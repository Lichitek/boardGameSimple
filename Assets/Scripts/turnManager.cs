using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnManager : MonoBehaviour
{
    public List<playerMovement> players = new List<playerMovement>();
    public List<Text> turnText = new List<Text>();
    public List<Text> scoreText = new List<Text>();
    public int idPlayering;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RunTurn(int numberBox)
    {
        switch(idPlayering)
        {
            case 0:
                turnText[idPlayering].gameObject.SetActive(false);
                scoreText[idPlayering].gameObject.SetActive(false);
                idPlayering = 1;
                turnText[idPlayering].gameObject.SetActive(true);
                scoreText[idPlayering].gameObject.SetActive(true);
                break;
            case 1:
                turnText[idPlayering].gameObject.SetActive(false);
                scoreText[idPlayering].gameObject.SetActive(false);
                idPlayering = 0;
                turnText[idPlayering].gameObject.SetActive(true);
                scoreText[idPlayering].gameObject.SetActive(true);
                break;
        }

    }
}
