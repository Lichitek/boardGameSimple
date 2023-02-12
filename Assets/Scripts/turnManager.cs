using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnManager : MonoBehaviour
{
    public List<playerMovement> players = new List<playerMovement>();
    public Text turnText;
    public Text scoreText;
    public int idPlayering;
    

    public void RunTurn(int numberBox)
    {
        switch(idPlayering)
        {
            case 0:                
                idPlayering = 1;
                turnText.text = "Игрок " + (idPlayering + 1).ToString();
                scoreText.text = players[idPlayering].score.ToString();
                break;

            case 1:
                idPlayering = 0;
                turnText.text = "Игрок " + (idPlayering + 1).ToString();
                scoreText.text = players[idPlayering].score.ToString();
                break;

            /*case 2:
                idPlayering = 3;
                turnText.text = "Игрок " + (idPlayering + 1).ToString();
                scoreText.text = players[idPlayering].score.ToString();
                break;

            case 3:
                idPlayering = 0;
                turnText.text = "Игрок " + (idPlayering + 1).ToString();
                scoreText.text = players[idPlayering].score.ToString();
                break;*/
        }

    }
}
