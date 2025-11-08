using UnityEngine;

public class Enemy : Entity
{
    [Header("Enemy Attack")]
    public float AttackGizmos;
    public bool playerCheck;
    public LayerMask Player;

    [Header("Enemy Movement")]
    private Transform FollowPlayer;
    private GameObject FindPlayer;
    private Vector2 Yon;

    [Header("Enemy Healt")]
    public int Health = 3;
    protected override void Awake()
    {
        // Referans en bata alyoruz
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        FindPlayer = GameObject.FindWithTag("Player");
        FollowPlayer = FindPlayer.GetComponent<Transform>();
    }
    protected override void Update()
    {
        if (animator.transform == null)
        {
            // Player yok olmuþ olabilir, bu yüzden transform'u kontrol ediyorsunuz...
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
        if (control && IsDeath) { 
            rb.linearVelocity = new Vector2(Yon.x * 2f, rb.linearVelocity.y);
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
        if (transform.rotation.y == 0)
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(AttackGizmos, 0, 0));
        else
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(-AttackGizmos, 0, 0));
    }
    private void PlayerCheck()
    {   
        if (transform.rotation.y == 0)
            playerCheck = Physics2D.Raycast(transform.position, Vector2.right, AttackGizmos, Player);
        else
            playerCheck = Physics2D.Raycast(transform.position, Vector2.right, -AttackGizmos, Player);
    }
    protected override void Attacking()
    {   if (!IsDeath)
            return;
        if (playerCheck && IsDeath)
            animator.SetTrigger("Attack");
    }
    public override void TakeDamage()
    {
        Debug.LogWarning("Take Damage çalýþtý");
        if (IsDeath)
        {
            Health -= 1;
            Debug.LogWarning("Düþman hasar aldý");
        }
    }
    public override void StartDamageAnimation()
    {   
        if(IsDeath)
            animator.SetTrigger("TakeDamage");
    }
    public override void Death()
    {
        animator.SetTrigger("Death");
        animator.SetBool("Alive", IsDeath);
    }
    protected override void IsDeathControl() => IsDeath = Health > 0;
}
