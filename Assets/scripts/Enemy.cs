using TMPro;
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
<<<<<<< HEAD
=======
        else
        {
            Debug.LogError("Sahne'de 'ScoreText' isminde bir obje bulunamadý!");
        }
>>>>>>> 9202e7a13a9c419df1006d1f3a9a62a88c1ac8dc
    }
    protected override void Update()
    {
        if (animator.transform == null)
        {
<<<<<<< HEAD
            // Player yok olmu� olabilir, bu y�zden transform'u kontrol ediyor
=======
            // Player yok olmuþ olabilir, bu yüzden transform'u kontrol ediyorsunuz...
>>>>>>> 9202e7a13a9c419df1006d1f3a9a62a88c1ac8dc
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
        // Eðer zaten öldüyse tekrar hasar almasýn
        if (!IsAlive) return;
        
        if(Health > 1) audioSource.PlayOneShot(damageSound, 0.5f);

        // Caný azalt
        Health -= 1;

        // Animasyonu tetikle (Hasar alma)
        animator.SetTrigger("TakeDamage");

        // Eðer can 0 veya altýna düþtüyse ÖLÜMÜ BAÞLAT
        if (Health <= 0)
        {
            Death();
        }
    }

    public override void Death()
    {
<<<<<<< HEAD
        audioSource.PlayOneShot(deathSound, 1f);
        totalkills++;
        if (uiObj != null)
        {
            score.text = "Score: " + totalkills;
        }
        // 1. Durum g�ncellemesi
        IsAlive = false; // Art�k ya�am�yor
=======
        totalkills++;
        score.text = "Score: " + totalkills;
        // 1. Durum güncellemesi
        IsAlive = false; // Artýk yaþamýyor
>>>>>>> 9202e7a13a9c419df1006d1f3a9a62a88c1ac8dc

        // 2. Ölüm Animasyonu
        animator.SetTrigger("Death");
        animator.SetBool("Alive", false);

        // 3. Fiziksel Etkileþimi Kes (Ýsteðe baðlý ama önerilir)
        // Düþman ölünce cesedine takýlmamak veya tekrar vurmamak için Collider'ý kapatabilirsin
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        if (rb != null)
        {
            rb.gravityScale = 0; // Yerçekimini sýfýrla (Havada asýlý kalsýn)
            rb.linearVelocity = Vector2.zero; // Eðer hareket halindeyse dursun

            // Alternatif olarak tamamen fiziði dondurmak için þunu da yapabilirsin:
            // rb.bodyType = RigidbodyType2D.Kinematic; 
        }

        // 4. YOK ETME (EN ÖNEMLÝ KISIM)
        // "gameObject" bu scriptin baðlý olduðu nesnedir.
        // 2.5f saniye bekler (animasyon bitsin diye), sonra kendini yok eder.
        Destroy(gameObject, 2.5f);
    }
    protected override void IsDeathControl() => IsAlive = Health > 0;
}
