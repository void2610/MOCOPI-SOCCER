using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    //シングルトン実装
    public static MenuManager instance;

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

    private GameObject titleProps;
    private GameObject goalPerformanceProps;
    public GameObject goalPerformanceTimer;

    public void ChangeGameState(GameManager.GameState gameState)
    {
        switch (gameState)
        {
            case GameManager.GameState.StartMenu:
                titleProps.SetActive(true);
                goalPerformanceProps.SetActive(false);
                break;
            case GameManager.GameState.ReadyToKick:
                titleProps.SetActive(false);
                goalPerformanceProps.SetActive(false);
                break;
            case GameManager.GameState.GoalPerformance:
                titleProps.SetActive(false);
                goalPerformanceProps.SetActive(true);
                break;
            case GameManager.GameState.ShowPicture:
                titleProps.SetActive(false);
                goalPerformanceProps.SetActive(false);
                break;
            default:
                break;
        }
    }
    void Start()
    {
        titleProps = GameObject.Find("TitleProps");
        titleProps.SetActive(true);

        goalPerformanceProps = GameObject.Find("GoalPerformanceProps");
        goalPerformanceProps.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState == GameManager.GameState.StartMenu && Input.GetKeyDown(KeyCode.Space))
        {
            titleProps.SetActive(false);
            GameManager.instance.StartGame();
        }
        goalPerformanceTimer.GetComponent<Text>().text = Mathf.Floor(GameManager.instance.stateStartTime + 3 - Time.time).ToString();
    }
}
