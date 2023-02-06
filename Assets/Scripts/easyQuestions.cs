using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class easyQuestions : MonoBehaviour
{
    public List<Text> scoreText = new List<Text>();
    public Text questionText;
    public Text answerText;
    public GameObject answer;
    public GameObject field;
    //public List<playerMovement> players = new List<playerMovement>();
    public turnManager playerTurn;

    public questions questions;

    public Animation anim;

    int number;


    void Start()
    {

    }

    
    void Update()
    {
        if (playerTurn.players[playerTurn.idPlayering].startQuiz)
        {
            StartCoroutine(Question());
        }        
    }

    IEnumerator Question()
    {
        number = playerTurn.players[playerTurn.idPlayering].score;

        int questionNumber = Random.Range(0, questions.questionsEasy.Length);
        switch (playerTurn.players[playerTurn.idPlayering].typeQuiz)
        {
            case "easy":
                //questionsEasy = File.ReadAllLines(questions.easyFNameAdmin);
                questionText.text = questions.questionsEasy[questionNumber].Substring(0, questions.questionsEasy[questionNumber].IndexOf("="));
                answerText.text = questions.questionsEasy[questionNumber].Substring(questions.questionsEasy[questionNumber].IndexOf("=") + 1);
                break;
            case "hard":
                //questionsHard = File.ReadAllLines(questions.hardFNameAdmin);
                questionText.text = questions.questionsHard[questionNumber].Substring(0, questions.questionsHard[questionNumber].IndexOf("="));
                answerText.text = questions.questionsHard[questionNumber].Substring(questions.questionsHard[questionNumber].IndexOf("=") + 1);
                break;
        }

        answer.SetActive(false);
        field.SetActive(true);
        anim.Play();
        playerTurn.players[playerTurn.idPlayering].startQuiz = false;
        //Time.timeScale = 0f;
        yield return null;

    }
    public void CloseTab()
    {        
        Time.timeScale = 1f;
        playerTurn.players[playerTurn.idPlayering].startQuiz = false;
        field.SetActive(false);
        if(playerTurn.players[playerTurn.idPlayering].score == number)
        {
            playerTurn.players[playerTurn.idPlayering].Return();
            
        }

        playerTurn.RunTurn(playerTurn.players[playerTurn.idPlayering].numberBox);

    }
    public void AddScore()
    {
        switch (playerTurn.players[playerTurn.idPlayering].typeQuiz)
        {
            case "easy":
                playerTurn.players[playerTurn.idPlayering].score += 1;
                scoreText[playerTurn.idPlayering].text = playerTurn.players[playerTurn.idPlayering].score.ToString();
                CloseTab();
                break;
            case "hard":
                playerTurn.players[playerTurn.idPlayering].score += 3;
                scoreText[playerTurn.idPlayering].text = playerTurn.players[playerTurn.idPlayering].score.ToString();
                CloseTab();
                break;
        }
    }
}
