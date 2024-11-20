using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public List<Button> answerButtons;
    public PlayerController playerController;
    public Button updateQuestionsButton; // Botão para atualizar perguntas
    public TextMeshProUGUI feedbackText; // Texto para feedback (opcional)

    private List<QuizQuestion> questions;
    private int currentQuestionIndex = 0;

    void Start()
    {
        // Carregar perguntas ao iniciar
        StartCoroutine(LoadQuestionsFromAPI());

        // Configurar botão de atualização
        if (updateQuestionsButton != null)
        {
            updateQuestionsButton.onClick.AddListener(() => StartCoroutine(UpdateQuestions()));
        }
    }

    IEnumerator LoadQuestionsFromAPI()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/api/questions");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Erro ao carregar as perguntas: " + request.error);
            ShowFeedback("Erro ao carregar perguntas.", Color.red);
        }
        else
        {
            string json = request.downloadHandler.text;
            QuizQuestion[] loadedQuestions = JsonHelper.FromJson<QuizQuestion>(json);

            if (loadedQuestions != null && loadedQuestions.Length > 0)
            {
                questions = new List<QuizQuestion>(loadedQuestions);
                DisplayNextQuestion();
                ShowFeedback("Perguntas carregadas com sucesso!", Color.green);
            }
            else
            {
                Debug.LogError("Nenhuma pergunta foi carregada da API.");
                ShowFeedback("Nenhuma pergunta disponível.", Color.red);
            }
        }
    }

    IEnumerator UpdateQuestions()
    {
        UnityWebRequest request = UnityWebRequest.PostWwwForm("http://localhost:3000/api/update-questions", "");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Erro ao atualizar as perguntas: " + request.error);
            ShowFeedback("Erro ao atualizar perguntas.", Color.red);
        }
        else
        {
            Debug.Log("Perguntas atualizadas com sucesso!");
            ShowFeedback("Perguntas atualizadas com sucesso!", Color.green);

            // Após atualizar, recarregar as perguntas
            StartCoroutine(LoadQuestionsFromAPI());
        }
    }

    void DisplayNextQuestion()
    {
        if (questions == null || questions.Count == 0)
        {
            Debug.LogError("Nenhuma pergunta carregada.");
            return;
        }

        if (currentQuestionIndex < questions.Count)
        {
            QuizQuestion question = questions[currentQuestionIndex];

            if (questionText != null)
            {
                questionText.text = question.question;
                Debug.Log("Texto da pergunta atualizado.");
            }
            else
            {
                Debug.LogError("questionText não está atribuído no Inspector.");
                return;
            }

            for (int i = 0; i < answerButtons.Count; i++)
            {
                if (i < question.answers.Count)
                {
                    if (answerButtons[i] != null)
                    {
                        TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                        if (buttonText != null)
                        {
                            buttonText.text = question.answers[i];
                            answerButtons[i].gameObject.SetActive(true);

                            bool isCorrect = question.answers[i] == question.correctAnswer;
                            answerButtons[i].onClick.RemoveAllListeners();  // Remove listeners antigos para evitar sobreposição
                            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(isCorrect));
                        }
                        else
                        {
                            Debug.LogError("TextMeshProUGUI não encontrado no botão de resposta " + i);
                        }
                    }
                    else
                    {
                        Debug.LogError("Botão de resposta " + i + " não está atribuído no Inspector.");
                    }
                }
                else
                {
                    answerButtons[i].gameObject.SetActive(false);
                }
            }

            Debug.Log("Botões de resposta atualizados.");
        }
        else
        {
            Debug.Log("Todas as perguntas foram respondidas.");
        }
    }

    void OnAnswerSelected(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("Resposta correta!");
            playerController.MoveToAttackPosition();
        }
        else
        {
            Debug.Log("Resposta incorreta!");
        }

        currentQuestionIndex++;
        DisplayNextQuestion();
    }

    // Método para exibir uma mensagem de feedback temporária
    void ShowFeedback(string message, Color color)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackText.color = color;
            StartCoroutine(ClearFeedbackAfterDelay(3f)); // Limpa após 3 segundos
        }
    }

    IEnumerator ClearFeedbackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (feedbackText != null)
        {
            feedbackText.text = "";
        }
    }
}
