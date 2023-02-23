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
    public int numPlayers;

    public List<Sprite> numImage = new List<Sprite>();
    public Image currentPlayersImage;

    public bool start;



    void Start()
    {
        numPlayers = 2;
    }

    private void FixedUpdate()
    {
        currentPlayersImage.sprite = numImage[numPlayers - 2];
    }

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
                idPlayering = 2;
                if (numPlayers < (idPlayering + 1))
                    idPlayering = 0;
                turnText.text = "Игрок " + (idPlayering + 1).ToString();
                scoreText.text = players[idPlayering].score.ToString();
                break;

            case 2:
                idPlayering = 3;
                if (numPlayers < (idPlayering + 1))
                    idPlayering = 0;
                turnText.text = "Игрок " + (idPlayering + 1).ToString();
                scoreText.text = players[idPlayering].score.ToString();
                break;

            case 3:
                idPlayering = 0;
                
                turnText.text = "Игрок " + (idPlayering + 1).ToString();
                scoreText.text = players[idPlayering].score.ToString();
                break;
        }

    }

    public void AddUser()
    {
        if (numPlayers < 4)
            numPlayers += 1;
    }

    public void RemoveUser()
    {
        if(numPlayers > 2)
            numPlayers -= 1;
    }

    public void startPlay()
    {
        start = true;
        for(int i = 0; i < players.Count; i++)
        {
            if (numPlayers > i)
                continue;
            else
            {
                players.RemoveAt(i);
                i--;
            }

        }
    }
}
