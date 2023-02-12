using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class questionManager : MonoBehaviour
{
    public Text questionText;
    public Text answerText;
    public GameObject answer;
    public GameObject field;
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
                questionText.text = questions.questionsEasy[questionNumber].Substring(0, questions.questionsEasy[questionNumber].IndexOf("="));
                answerText.text = questions.questionsEasy[questionNumber].Substring(questions.questionsEasy[questionNumber].IndexOf("=") + 1);
                break;
            case "hard":
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
                CloseTab();
                break;
            case "hard":
                playerTurn.players[playerTurn.idPlayering].score += 3;
                CloseTab();
                break;
        }
    }
}
