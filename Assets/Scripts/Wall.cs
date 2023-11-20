using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    Vector3 targetPosition1 = new Vector3(21, 3, -8);
    Vector3 targetPosition2 = new Vector3(21, 3, 8);
    void Start()
    {

    }

    void Update()
    {
        transform.position = Vector3.Lerp(targetPosition1, targetPosition2, Mathf.PingPong(Time.time, 1.0f));
    }
}
