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

    enum GameState
    {
        StartMenu,
        ReadyToKick,
        Kicking,
        GameOver
    }

    [SerializeField]
    private GameObject ballPrefab;

    private Vector3 ballSpawnPosition;

    private GameState gameState = GameState.StartMenu;
    private float kickStartTime;
    private float KICK_TIME_LIMIT = 3.0f;


    private GameObject scoreText;
    private GameObject ball;
    private GameObject goal;
    private int score = 0;

    public void StartGame()
    {
        this.gameState = GameState.ReadyToKick;
        //1秒間待機
        Invoke("SetBall", 1.0f);
    }
    private void KickBallToGoal(float power, Vector3 offset)
    {
        if (ball == null) return;

        Vector3 direction = goal.transform.position - ball.transform.position;
        direction += new Vector3(0, 4, 0);
        direction += offset;

        ball.GetComponent<Rigidbody>().AddForce(direction.normalized * 1000 * power);
    }

    public void SetBall()
    {
        if (ball != null)
        {
            Destroy(ball);
        }

        GameObject newBall = Instantiate(ballPrefab, ballSpawnPosition, Quaternion.identity);
        this.ball = newBall;
        gameState = GameState.ReadyToKick;
    }

    public void AddScore(int num)
    {
        score += num;
        scoreText.GetComponent<UnityEngine.UI.Text>().text = "Score: " + score.ToString();
    }

    void Start()
    {
        goal = GameObject.Find("GoalTrigger");
        scoreText = GameObject.Find("ScoreText");
        ballSpawnPosition = GameObject.Find("BallSpawnAncher").transform.position;
    }


    void Update()
    {
        switch (gameState)
        {
            case GameState.ReadyToKick:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("aa");
                    gameState = GameState.Kicking;
                    KickBallToGoal(1.0f, Vector3.zero);
                    kickStartTime = Time.time;
                }
                break;
            case GameState.Kicking:
                if (kickStartTime + KICK_TIME_LIMIT < Time.time)
                {
                    SetBall();
                    gameState = GameState.ReadyToKick;

                }
                break;
            default:
                break;
        }
    }
}
