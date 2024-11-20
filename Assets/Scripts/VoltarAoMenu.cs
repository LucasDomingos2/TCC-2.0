using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Função para voltar ao menu principal
    public void VoltarAoMenu()
    {
        SceneManager.LoadScene("Main menu");
    }
}
