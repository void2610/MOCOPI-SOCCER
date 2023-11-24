using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public enum GameState
    {
        StartMenu,
        ReadyToKick,
        Kicking,
        GoalPerformance,
        ShowPicture,
        GameOver
    }

    [SerializeField]
    private GameObject ballPrefab;

    private Vector3 ballSpawnPosition;

    public GameState gameState { private set; get; }
    private float stateStartTime = 99999;
    private float KICK_TIME_LIMIT = 3.0f;
    private float GOAL_PERFORMANCE_TIME_LIMIT = 3.0f;
    private float SHOW_PICTURE_TIME_LIMIT = 3.0f;


    private GameObject scoreText;
    private GameObject stateText;
    private GameObject ball;
    private GameObject goal;
    private GameObject performanceCamera;
    private GameObject performancePicture;
    private int score = 0;

    public void StartGame()
    {
        this.gameState = GameState.ReadyToKick;
        //1秒間待機
        Invoke("SetBall", 1.0f);
    }
    public void KickBallToGoal(float power, Vector3 offset)
    {
        if (ball == null) return;

        Vector3 direction = goal.transform.position - ball.transform.position;
        direction += new Vector3(0, 4, 0);
        direction += offset;

        ball.GetComponent<Rigidbody>().AddForce(direction.normalized * 10 * power);
    }

    public void SetBall()
    {
        if (ball != null)
        {
            Destroy(ball);
        }

        GameObject newBall = Instantiate(ballPrefab, ballSpawnPosition, Quaternion.identity);
        this.ball = newBall;
    }

    public void OnGoal(int num)
    {
        score += num;
        scoreText.GetComponent<UnityEngine.UI.Text>().text = "スコア " + score.ToString();

        gameState = GameState.GoalPerformance;
        MenuManager.instance.ChangeGameState(GameState.GoalPerformance);
        stateStartTime = Time.time;
    }

    void Start()
    {
        goal = GameObject.Find("GoalTrigger");
        scoreText = GameObject.Find("ScoreText");
        stateText = GameObject.Find("StateText");
        performancePicture = GameObject.Find("PerformancePicture");
        performanceCamera = GameObject.Find("PerformanceCamera");
        ballSpawnPosition = GameObject.Find("BallSpawnAncher").transform.position;
        gameState = GameState.StartMenu;
    }


    void Update()
    {
        switch (gameState)
        {
            case GameState.ReadyToKick:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (ball != null)
                    {
                        gameState = GameState.Kicking;
                        KickBallToGoal(1.0f, Vector3.zero);
                        stateStartTime = Time.time;
                    }
                }
                break;
            case GameState.Kicking:
                if (stateStartTime + KICK_TIME_LIMIT < Time.time)
                {
                    SetBall();
                    gameState = GameState.ReadyToKick;

                }
                break;
            case GameState.GoalPerformance:
                if (stateStartTime + GOAL_PERFORMANCE_TIME_LIMIT < Time.time)
                {
                    Texture2D screenshot = performanceCamera.GetComponent<PerformanceCamera>().Capture();
                    performancePicture.GetComponent<RawImage>().texture = screenshot;

                    gameState = GameState.ShowPicture;
                    MenuManager.instance.ChangeGameState(GameState.ShowPicture);
                    stateStartTime = Time.time;
                }
                break;
            case GameState.ShowPicture:
                if (stateStartTime + SHOW_PICTURE_TIME_LIMIT < Time.time)
                {
                    performancePicture.GetComponent<RawImage>().texture = null;
                    SetBall();
                    gameState = GameState.ReadyToKick;
                    MenuManager.instance.ChangeGameState(GameState.ReadyToKick);
                }
                break;
            default:
                break;
        }

    }
}
