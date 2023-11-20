using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;

    private Vector3 ballSpawnPosition;
    void Start()
    {
        ballSpawnPosition = GameObject.Find("BallSpawnAncher").transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>() != null)
        {
            Destroy(other.gameObject);
            GameObject ball = Instantiate(ballPrefab, ballSpawnPosition, Quaternion.identity);
            GameManager.instance.AddScore(1);
            GameManager.instance.SetBall(ball);
        }
    }
}
