using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    public route currenrRoute;
    public int steps;
    public bool startQuiz = false;
    public string typeQuiz;
    public int score;
    public int numberBox;

    public Vector3 moveAt;
    Vector3 transit;

    public List<Sprite> imgNumber = new List<Sprite>();
    public Image dice;
    public turnManager turnMan;
    int routePosition;
    bool isMoving;
    int backSteps;

    public Image endGame;

    void Start()
    {
        score = 0;
    }


    void FixedUpdate()
    {
        if (turnMan.numPlayers < (numberBox + 1) && turnMan.start)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void RollDice()
    {
        if (!isMoving && numberBox == turnMan.idPlayering)
        {
            startQuiz = false;
            steps = Random.Range(1, 7);
            backSteps = steps;
            if (routePosition + steps< currenrRoute.childTileList.Count)
            {
                StartCoroutine(swapDice(steps));
                
                StartCoroutine(Move());                
            }
            else
            {
                Debug.Log("Rolled num is 2 high");
            }
        }
        
    }

    IEnumerator swapDice(int randNum)
    {
        for (int i=0; i < 10; i++)
        {
            dice.sprite = imgNumber[Random.Range(0, 6)];

            yield return new WaitForSeconds(0.05f);
            if(i == 9)
            {
                dice.sprite = imgNumber[randNum-1];
                StopCoroutine("swapDice");
            }
        }
    }

    IEnumerator Move()
    {
        if(isMoving)
        {
            yield break;
        }
        isMoving = true;
        while (steps > 0)
        {
            Vector3 nextPos = new Vector3(currenrRoute.childTileList[routePosition + 1].position.x, 0.5f, currenrRoute.childTileList[routePosition + 1].position.z);

            while(MoveToNextTile(nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
            steps--;
            routePosition++;
        }

        isMoving = false;
        startQuiz = true;

    }
    bool MoveToNextTile(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 8f * Time.deltaTime));
    }

    public void Return()
    {
        if (!isMoving)
        {
            if (routePosition - steps < currenrRoute.childTileList.Count)
            {
                StartCoroutine(MoveBack());
            }
            else
            {
                Debug.Log("LoL");
            }
        }
    }

    IEnumerator MoveBack()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;
        while (backSteps > 0)
        {
            Vector3 nextPos = currenrRoute.childTileList[routePosition - 1].position;

            while (MoveToBackTile(nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
            backSteps--;
            routePosition--;
        }
        isMoving = false;
    }
    bool MoveToBackTile(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 16f * Time.deltaTime));
    }

    void endOfGame()
    {
        Time.timeScale = 0f;
        endGame.gameObject.SetActive(true);
        //scores[0].text = turnMan.players[0].score.ToString();
        //scores[1].text = turnMan.players[1].score.ToString();

    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "finishTile":
                endOfGame();
                break;
            case "easyTile":
                typeQuiz = "easy";
                break;
            case "hardTile":
                typeQuiz = "hard";
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transit = this.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, moveAt, 16f * Time.deltaTime);


        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            transform.position = Vector3.MoveTowards(transform.position, transit, 16f * Time.deltaTime);


        }
    }

}
