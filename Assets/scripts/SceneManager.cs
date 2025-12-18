using UnityEngine;
using UnityEngine.SceneManagement; // Sahne deðiþtirmek için bu kütüphane þart!

public class LevelManager : MonoBehaviour
{
    // Bu fonksiyon diðer sahneye geçiþi saðlar
    public void SonrakiBolumeGec()
    {
        // Þu anki sahnenin numarasýný al (Örn: 0)
        int aktifSahneNo = SceneManager.GetActiveScene().buildIndex;

        // Bir sonraki sahneyi yükle (Örn: 0 + 1 = 1)
        SceneManager.LoadScene(aktifSahneNo + 1);
    }
}