using TMPro;
using UnityEngine;


public class Enemy : Entity
{
    [Header("Enemy Attack")]
    public float AttackGizmos;
    public bool playerCheck;
    public LayerMask Player;

    [SerializeField] private Transform attackPoint; 
   
    [Header("Enemy Movement")]
    private Transform FollowPlayer;
    private GameObject FindPlayer;
    private Vector2 Yon;

    [Header("Enemy Healt")]
    public int Health = 3;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] public static int totalkills = 0;
    [SerializeField] private GameObject uiObj;
    protected override void Awake()
    {
        // Referans en bata alyoruz
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        FindPlayer = GameObject.FindWithTag("Player");
        FollowPlayer = FindPlayer.GetComponent<Transform>();
        IsAlive = false;

        uiObj = GameObject.Find("Score");
        if (uiObj != null)
        {
            score = uiObj.GetComponent<TextMeshProUGUI>();
        }
    }
    protected override void Update()
    {
        if (animator.transform == null)
        {
            return;
        }
        Hareket();
        GroundCheck();
        Attacking();
        PlayerCheck();
        IsDeathControl();
    }
    protected override void Hareket()
    {
        Yon = FollowPlayer.position - transform.position;
        Yon.Normalize();
        if (control && IsAlive) { 
            rb.linearVelocity = new Vector2(Yon.x * 2f, rb.linearVelocity.y);
            animator.SetFloat("Blend", Yon.x);
            if (Yon.x < 0)
                transform.eulerAngles = new Vector3(0, 180, 0);
            else if(Yon.x > 0)
                transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        

    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Vector3 baslangic = (attackPoint != null) ? attackPoint.position : transform.position;

        if (transform.rotation.y == 0)
            Gizmos.DrawLine(baslangic, baslangic + new Vector3(AttackGizmos, 0, 0));
        else
            Gizmos.DrawLine(baslangic, baslangic + new Vector3(-AttackGizmos, 0, 0));
    }
    private void PlayerCheck()
    {
        Vector3 baslangic = (attackPoint != null) ? attackPoint.position : transform.position;

        if (transform.rotation.y == 0)
            playerCheck = Physics2D.Raycast(baslangic, Vector2.right, AttackGizmos, Player);
        else
            playerCheck = Physics2D.Raycast(baslangic, Vector2.right, -AttackGizmos, Player);
    }
    protected override void Attacking()
    {   if (!IsAlive)
            return;
        if (playerCheck && IsAlive)
        {
            //audioSource.PlayOneShot(attackSound, 0.5f);
            animator.SetTrigger("Attack");
        }
    }

    public override void TakeDamage()
    {
        if (!IsAlive) return;
        
        if(Health > 1) audioSource.PlayOneShot(damageSound, 0.5f);

        Health -= 1;

        // Animasyonu tetikle (Hasar alma)
        animator.SetTrigger("TakeDamage");

        if (Health <= 0)
        {
            Death();
        }
    }

    public override void Death()
    {
        audioSource.PlayOneShot(deathSound, 1f);
        totalkills++;
        if (uiObj != null)
        {
            score.text = "Score: " + totalkills;
        }
        IsAlive = false;

        animator.SetTrigger("Death");
        animator.SetBool("Alive", false);

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;

        }

        Destroy(gameObject, 2.5f);
    }
    protected override void IsDeathControl() => IsAlive = Health > 0;
}
