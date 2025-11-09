using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    //HAREKET
    [SerializeField] protected float hareketHizi = 6f;
    private float yatayGirdi;       
    protected Rigidbody2D rb;
    protected bool control = true;
    protected float EnemyX = 1;

    //ANİMASYON
    [SerializeField] protected Animator animator;
    [SerializeField] private Animator animator2;

    //ZIPLAMAKONTROL
    [SerializeField] protected float TouchingGround;
    [SerializeField] private bool IsGround;
    [SerializeField] private LayerMask GroundLayer;

    [Header("Attack")]
    [SerializeField] protected float AttackRadius;
    [SerializeField] protected Transform AttackPoint;
    [SerializeField] protected LayerMask Target;
    public Collider2D EnemiesCollider;

    [Header("CanBarı")]
    [SerializeField] protected Slider CanBarı;
    [SerializeField] protected TextMeshProUGUI text;

    [Header("Death")]
    [SerializeField] protected bool IsDeath;

    protected virtual void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        CanBarı.value = 0;
        animator.SetBool("Alive", true);
    }

    protected virtual void Update()
    {
        if (animator.transform == null)
        {
            // Player yok olmuş olabilir, bu yüzden transform'u kontrol ediyoruz
            return;
        }
        GroundCheck();
        Hareket();
        jump();
        animationofPlayer();
        Attacking();
        IsDeathControl();
    }

    protected virtual void Hareket()
    {   
        yatayGirdi = Input.GetAxisRaw("Horizontal"); // -1, 0, veya 1 doner
        if (control && IsDeath)
            rb.linearVelocity = new Vector2(yatayGirdi * hareketHizi, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, 0);  

        if (yatayGirdi == -1 && IsDeath)
            transform.eulerAngles = new Vector2(0, 180);
        else if (yatayGirdi == 1 && IsDeath)
            transform.eulerAngles = new Vector2(0, 0);
    }
    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGround && control && IsDeath)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 10f);
        }
        if (Input.GetKey(KeyCode.LeftShift) && IsGround && IsDeath) 
            hareketHizi = 10f;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            hareketHizi = 6f;
    }
    protected virtual void Attacking()
    {
        if(Input.GetMouseButtonDown(0) && IsGround && IsDeath)
                animator.SetTrigger("Attack");
    }
    public void TargetDedector()
    {
        EnemiesCollider = Physics2D.OverlapCircle(AttackPoint.position, AttackRadius, Target);

        
    }
    public virtual void StartDamageAnimation()
    {
        Debug.Log("StartDamageAnimation ÇAĞRILDI"); // 1. Bu log konsolda görünüyor mu?

        if (animator == null)
        {
            Debug.LogError("ANIMATOR DEĞİŞKENİ 'NULL'!"); // 2. Bu hata çıkıyor mu?
            return;
        }
        if (IsDeath) 
        { 
            animator.SetTrigger("TakeDamage");
            Debug.Log("TakeDamage trigger'ı ateşlendi."); // 3. Bu log görünüyor mu?
        }
    }

    public virtual void TakeDamage()
    {
        Debug.Log("PlayerTakeDamage ÇAĞRILDI"); // 1. Bu log konsolda görünüyor mu?

        if (CanBarı == null)
        {
            Debug.LogError("CAN BARI (SLIDER) DEĞİŞKENİ 'NULL'!"); // 2. Bu hata çıkıyor mu?
            return;
        }
        if (text == null)
        {
            Debug.LogError("TEXT (TMP) DEĞİŞKENİ 'NULL'!"); // 3. Bu hata çıkıyor mu?
            return;
        }

        CanBarı.value += 15;
        text.text = $"{100 - CanBarı.value}";
        Debug.Log("Can barı güncellendi. Yeni Değer: " + CanBarı.value); // 4. Bu log görünüyor mu?
    }
    public virtual void Death()
    {
        animator2.SetTrigger("Death");
        //Destroy(gameObject);
        animator.SetBool("Alive", IsDeath);
    }
    protected virtual void GroundCheck()
    {
        IsGround = Physics2D.Raycast(transform.position, Vector2.down, TouchingGround, GroundLayer);
    }
    protected void animationofPlayer()
    {   if (IsDeath)
        {
            animator.SetFloat("xVelocity", yatayGirdi);
            animator.SetFloat("yVelocity", rb.linearVelocity.y);
            animator.SetBool("IsGround", IsGround);
        }
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -TouchingGround, 0));
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRadius);
    }
    public void AttackControlTrue(bool hareket)
    {
        control = hareket;
    }
    protected virtual void IsDeathControl() => IsDeath = CanBarı.value < 100;
}
