using UnityEngine;
using System.Collections;

public class Archer : MonoBehaviour
{
    [Header("Yon Belirle")]
    [SerializeField] private Transform player;
    private Vector2 Yon;
    private float angleZ;
    private Quaternion rota;

    [Header("Ates Etme")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Animasyon")]
    [SerializeField] private Animator archerAnimator;

    [Header("Can")]
    public int Health = 5;

    void Awake()
    {
        archerAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        YonBelirle();
    }

    void YonBelirle()
    {
        Yon = player.position - transform.position;
        Yon.Normalize();
        
        if (Yon.x < 0)
            transform.eulerAngles = new Vector2(0, 180);
        else if (Yon.x > 0)
            transform.eulerAngles = new Vector2(0, 0);

        angleZ = Mathf.Atan2(Yon.y, Yon.x) * Mathf.Rad2Deg;
        rota = Quaternion.Euler(0, 0, angleZ);
    }

    public void Attack() => Instantiate(arrowPrefab, firePoint.position, rota);


    public void StartDamageAnimation() => archerAnimator.SetTrigger("TakeDamage");
    public void TakeDamage()
    {
        Health--;
        if (Health <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        archerAnimator.SetTrigger("Death");
        archerAnimator.SetBool("Alive", false);
        Destroy(gameObject, 1.5f);
    }
}
