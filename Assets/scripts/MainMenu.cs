using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Panelller")]
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject LevelPanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject OptionsPanel;

    [Header("Arka Plan Sesi")]
    [SerializeField] private Slider SesAyarý;
    [SerializeField] private AudioSource ArkaPlanSesi;
    [SerializeField] private TextMeshProUGUI volume;
    static float ses = 0.5f;

    void Awake()
    {
        if (Entity.PlayerDeathCount > 0)
        {
            MainMenuPanel.SetActive(false);
            LevelPanel.SetActive(false);
            GameOverPanel.SetActive(true);
        }
        SesAyarý.value = ses;
        Enemy.totalkills = 0;
    }

    private void Update()
    {
        ArkaPlanSesi.volume = SesAyarý.value;
        volume.text = (SesAyarý.value * 100).ToString("0.0");

    }

    public void StartGame()
    {
        MainMenuPanel.SetActive(false);
        LevelPanel.SetActive(true);
    }

    public void TurnBack()
    {
        MainMenuPanel.SetActive(true);
        LevelPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        OptionsPanel.SetActive(false);
    }

    public void loadLevel(int levelIndex)
    {
        Enemy.totalkills = 0;
        ses = ArkaPlanSesi.volume;
        SceneManager.LoadScene(levelIndex);
    }

    public void TurnLevels()
    {
        MainMenuPanel.SetActive(false);
        LevelPanel.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(Entity.SceneIndex);
    }

    public void Options()
    {
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit(); // build edilen oyundan çýkýlmasýný saðlar.

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // editörde çalýþtýrýrken oyundan çýkmayý saðlar.
        #endif
    }
}
