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

    private GameObject ball;
    private GameObject goal;

    private void KickBallToGoal()
    {
        Vector3 direction = goal.transform.position - ball.transform.position;
        direction = new Vector3(direction.x, direction.y + 4, direction.z);

        ball.GetComponent<Rigidbody>().AddForce(direction.normalized * 1000);
    }
    void Start()
    {
        ball = GameObject.Find("Ball");
        goal = GameObject.Find("GoalTrigger");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            KickBallToGoal();
        }
    }
}
