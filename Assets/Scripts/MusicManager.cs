using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicManager : MonoBehaviour
{
    private static MenuMusicManager instance;
    
    void Awake()
    {
        // Verifica se já existe uma instância do MenuMusicManager
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
        // Verifica se estamos na cena de jogar para parar a música
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            Destroy(gameObject); // Destrói o GameObject quando entra na cena de jogo
        }
    }
}
