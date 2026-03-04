using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 15;

    [Header("Dönme Ayarlarý (Rotation)")]
    [Tooltip("Tiki açarsan kendi etrafýnda (3D para gibi), kapatýrsan direksiyon gibi döner.")]
    public bool yEksenindeDon = true;

    public float donmeHizi = 150f;

    public float paraDonmeHizi = 5f;

    [Header("Aþaðý Yukarý Hareket (Floating)")]
    public float dalgaFrekansi = 2f;
    public float dalgaGenligi = 0.25f; 

    private Vector3 baslangicPozisyonu;
    private Vector3 baslangicScale; // Objenin orijinal büyüklüðünü hafýzada tutmak için

    void Start()
    {
        // Objenin ilk konumunu kaydediyoruz ki referans alarak onun etrafýnda aþaðý yukarý gitsin
        baslangicPozisyonu = transform.position;

        baslangicScale = transform.localScale;
    }

    void Update()
    {
        // 1. DÖNME ÝÞLEMÝ
        if (yEksenindeDon)
        {
            float yeniGenislik = Mathf.Sin(Time.time * paraDonmeHizi) * baslangicScale.x;
            transform.localScale = new Vector3(yeniGenislik, baslangicScale.y, baslangicScale.z);
        }
        else
        {
            transform.Rotate(0f, 0f, donmeHizi * Time.deltaTime);
        }

        // 2. AÞAÐI YUKARI SÜZÜLME ÝÞLEMÝ
        float yeniY = baslangicPozisyonu.y + Mathf.Sin(Time.time * dalgaFrekansi) * dalgaGenligi;

        transform.position = new Vector3(transform.position.x, yeniY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Entity playerEntity = other.GetComponent<Entity>();

            if (playerEntity != null)
            {
                playerEntity.Heal(healAmount);

                // AudioSource.PlayClipAtPoint(toplamaSesi, transform.position);

                Destroy(gameObject);
            }
        }
    }
}