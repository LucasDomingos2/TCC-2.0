using System.Collections.Generic;

public class Question
{
    public int Id { get; set; }  // Identificador único da pergunta
    public string Text { get; set; }  // Texto da pergunta
    public List<string> Answers { get; set; }  // Lista de respostas

    public Question(int id, string text, List<string> answers)
    {
        Id = id;
        Text = text;
        Answers = answers;
    }
}
