using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePicture : MonoBehaviour
{
    [SerializeField]
    private GameObject performanceCamera;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Texture2D screenshot = performanceCamera.GetComponent<PerformanceCamera>().Capture();
        }
    }
}
