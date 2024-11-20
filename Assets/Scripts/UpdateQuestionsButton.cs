using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class QuestionUpdater : MonoBehaviour
{
    public Button updateQuestionsButton; // Referência ao botão

    private string updateQuestionsUrl = "http://localhost:3000/api/update-questions";

    void Start()
    {
        updateQuestionsButton.onClick.AddListener(UpdateQuestions);
    }

    public void UpdateQuestions()
    {
        StartCoroutine(SendUpdateQuestionsRequest());
    }

    private IEnumerator SendUpdateQuestionsRequest()
    {
        UnityWebRequest request = UnityWebRequest.PostWwwForm(updateQuestionsUrl, "");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Erro ao atualizar perguntas: " + request.error);
        }
        else
        {
            Debug.Log("Perguntas atualizadas com sucesso!");
        }
    }
}
