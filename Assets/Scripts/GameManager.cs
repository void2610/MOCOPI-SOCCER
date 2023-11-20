using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //シングルトン実装
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);

        }
    }


    private GameObject scoreText;
    private GameObject ball;
    private GameObject goal;

    private int score = 0;

    private void KickBallToGoal()
    {
        Vector3 direction = goal.transform.position - ball.transform.position;
        direction = new Vector3(direction.x, direction.y + 4, direction.z);

        ball.GetComponent<Rigidbody>().AddForce(direction.normalized * 1000);
    }

    public void SetBall(GameObject ball)
    {
        this.ball = ball;
    }

    public void AddScore(int num)
    {
        score += num;
        scoreText.GetComponent<UnityEngine.UI.Text>().text = "Score: " + score.ToString();
    }

    void Start()
    {
        ball = GameObject.Find("Ball");
        goal = GameObject.Find("GoalTrigger");
        scoreText = GameObject.Find("ScoreText");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            KickBallToGoal();
        }
    }
}
