using System.Collections.Generic;

[System.Serializable]
public class QuizQuestion
{
    public int id;
    public string question;
    public List<string> answers; // A lista de respostas
    public string correctAnswer;
}
