using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Panelller")]
    public GameObject MainMenuPanel;
    public GameObject LevelPanel;

    public void StartGame()
    {
        MainMenuPanel.SetActive(false);
        LevelPanel.SetActive(true);
    }
    public void TurnBack()
    {
        MainMenuPanel.SetActive(true);
        LevelPanel.SetActive(false);
    }
    public void loadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
