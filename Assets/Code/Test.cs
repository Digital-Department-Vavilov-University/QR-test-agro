using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Trailer
{
    [SerializeField] private string name;
    [SerializeField] private List <Question> questions;
    public string Name => name;
    public List<Question> Questions => questions;
}

[System.Serializable]
public class Question
{
    [SerializeField] private string item;
    [SerializeField] [Range(1, 5)] private ushort weight;

    public string Item => item;
    public ushort Weight => weight;
}

public class Test : MonoBehaviour
{
    [SerializeField] List<Trailer> trailers = new List<Trailer>();
    private ushort valueScore = 0;

    public ushort ValueScore => valueScore;

    private Trailer currentTrailer;
    private Question currentQuestion;

    [SerializeField] private Text status;
    [SerializeField] private Text labelTrailer;
    [SerializeField] private Text labelObject;

    [SerializeField] public GameObject Score;


    private int maxScore;
    public int MaxScore => maxScore;
    private void Start()
    {
        NextQuestion();
    }

    public void CheckAnswer(string answer)
    {
        if (answer == (currentTrailer.Name + " " + currentQuestion.Item) )
        {
            status.text = "Правильный ответ";
            valueScore += currentQuestion.Weight;
        }
        else
        {
            status.text = "ответ не верный"; 
        }
        NextQuestion();
    }

    private short valueObject = 0;
    public void NextQuestion()
    {
        valueObject++;
        if (valueObject > 4)
        {
            trailers.Remove(currentTrailer);
            if (trailers.Count > 0) currentTrailer = trailers[Random.Range(0, trailers.Count - 1)];
            else { Score.SetActive(true); gameObject.SetActive(false); }
            valueObject = 0;
        }
        if (currentTrailer == null) currentTrailer = trailers[Random.Range(0, trailers.Count - 1)];
        if (currentTrailer.Questions.Count <= 0)
        {
            trailers.Remove(currentTrailer);
            if (trailers.Count > 0) currentTrailer = trailers[Random.Range(0, trailers.Count - 1)];
            else { Score.SetActive(true); gameObject.SetActive(false); }
        }
        labelTrailer.text = "Прицеп - " + currentTrailer.Name;

        currentQuestion = currentTrailer.Questions[Random.Range(0, currentTrailer.Questions.Count - 1)];
        currentTrailer.Questions.Remove(currentQuestion);
        labelObject.text = "Объект - " + currentQuestion.Item;
        maxScore += currentQuestion.Weight;
    }
   
}
