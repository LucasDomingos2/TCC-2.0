using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicManager : MonoBehaviour
{
    private static GameMusicManager instance;
    
    void Awake()
    {
        // Verifica se já existe uma instância do GameMusicManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Não destrua ao carregar uma nova cena
        }
        else
        {
            Destroy(gameObject); // Destroi o novo GameObject duplicado
        }
    }

    void Update()
    {
        // Verifica se voltamos ao menu para parar a música de jogo
        if (SceneManager.GetActiveScene().name != "GameScene")
        {
            Destroy(gameObject); // Destrói o GameObject quando sai da cena de jogo
        }
    }
}
