using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;
    public Button audioToggleButton;

    private bool isAudioOn = true;
    public AudioSource backgroundMusic;

    private void Start()
    {
        // Adicionar listeners aos botões
        playButton.onClick.AddListener(PlayGame);
        settingsButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(QuitGame);
        audioToggleButton.onClick.AddListener(ToggleAudio);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Função para alternar o áudio ligado/desligado
    public void ToggleAudio()
    {
        isAudioOn = !isAudioOn;

        if (isAudioOn)
        {
            backgroundMusic.mute = false;
        }
        else
        {
            backgroundMusic.mute = true;
        }
    }
}
