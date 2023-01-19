using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class easyQuestions : MonoBehaviour
{
    public List<Text> scoreText = new List<Text>();
    public Text questionText;
    public Text answerText;
    public GameObject answer;
    public GameObject field;
    //public List<playerMovement> players = new List<playerMovement>();
    public turnManager playerTurn;

    public Animation anim;

    int number;
    string[] questionsEasy;
    string easyFilePath, easyFileName;
    string[] questionsHard;
    string hardFilePath, hardFileName;

    void Start()
    {
        easyFileName = "questions.txt";
        easyFilePath = Application.dataPath + "/" + easyFileName;

        hardFileName = "questionsHard.txt";
        hardFilePath = Application.dataPath + "/" + hardFileName;
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

        int questionNumber = Random.Range(0, 3);
        switch (playerTurn.players[playerTurn.idPlayering].typeQuiz)
        {
            case "easy":
                questionsEasy = File.ReadAllLines(easyFilePath);
                questionText.text = questionsEasy[questionNumber].Substring(0, questionsEasy[questionNumber].IndexOf("="));
                answerText.text = questionsEasy[questionNumber].Substring(questionsEasy[questionNumber].IndexOf("=") + 1);
                break;
            case "hard":
                questionsHard = File.ReadAllLines(hardFilePath);
                questionText.text = questionsHard[questionNumber].Substring(0, questionsHard[questionNumber].IndexOf("="));
                answerText.text = questionsHard[questionNumber].Substring(questionsHard[questionNumber].IndexOf("=") + 1);
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
