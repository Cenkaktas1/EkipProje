using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Player")]
    private Entity playerScript;
    private bool hareketControl = true;
    private GameObject player;

    private Rigidbody2D rb;
    private BoxCollider2D myCollider;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<Entity>();
        }

        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Hareket();
    }

    void Hareket()
    {   if (hareketControl)
            transform.Translate(Vector2.right * 10f * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript.StartDamageAnimation();
            playerScript.TakeDamage();
            Destroy(gameObject);
        }

        else if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            hareketControl = false;
            myCollider.enabled = false;

            transform.Translate(Vector2.right * 0f * Time.deltaTime);
            Destroy(gameObject, 1f);
        }

        else
            Destroy(gameObject, 3f);
    }
}
