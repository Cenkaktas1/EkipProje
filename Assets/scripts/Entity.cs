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
    [SerializeField] protected bool IsAlive;

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
        Dash();
    }

    protected virtual void Hareket()
    {   
        yatayGirdi = Input.GetAxisRaw("Horizontal"); // -1, 0, veya 1 doner
        if (control && IsAlive)
            rb.linearVelocity = new Vector2(yatayGirdi * hareketHizi, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, 0);  

        if (yatayGirdi == -1 && IsAlive)
            transform.eulerAngles = new Vector2(0, 180);
        else if (yatayGirdi == 1 && IsAlive)
            transform.eulerAngles = new Vector2(0, 0);
    }
    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGround && control && IsAlive)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 10f);
        }
        if (Input.GetKey(KeyCode.LeftShift) && IsGround && IsAlive) 
            hareketHizi = 10f;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            hareketHizi = 6f;
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.C) && IsAlive && IsGround && control)
            animator.SetTrigger("Dash");
    }

    protected virtual void Attacking()
    {
        if(Input.GetMouseButtonDown(0) && IsGround && IsAlive)
                animator.SetTrigger("Attack");
    }
    // Entity.cs içinde (Player için çalışan kısım)

    public void TargetDedector()
    {
        // 1. Saldırı alanındaki TÜM objeleri bul (Sadece birini değil)
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRadius, Target);

        // 2. Bulunan her bir obje için işlem yap
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            // 3. Çarptığımız objenin üzerindeki "Enemy" scriptini al
            // (Bu sayede kopyasına vurduysak kopyasının scriptini, ana objeye vurduysak onun scriptini alırız)
            Enemy hitEnemy = enemyCollider.GetComponent<Enemy>();

            if (hitEnemy != null)
            {
                // 4. Sadece bulduğumuz O düşmana hasar ver
                hitEnemy.TakeDamage();
                hitEnemy.StartDamageAnimation(); // Eğer animasyonu da buradan tetikliyorsan
            }
        }
    }
    public virtual void StartDamageAnimation()
    {
        Debug.Log("StartDamageAnimation ÇAĞRILDI"); // 1. Bu log konsolda görünüyor mu?

        if (animator == null)
        {
            Debug.LogError("ANIMATOR DEĞİŞKENİ 'NULL'!"); // 2. Bu hata çıkıyor mu?
            return;
        }
        if (IsAlive) 
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
        animator.SetBool("Alive", IsAlive);
    }
    protected virtual void GroundCheck()
    {
        IsGround = Physics2D.Raycast(transform.position, Vector2.down, TouchingGround, GroundLayer);
    }
    protected void animationofPlayer()
    {   if (IsAlive)
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
    protected virtual void IsDeathControl() => IsAlive = CanBarı.value < 100;
}
