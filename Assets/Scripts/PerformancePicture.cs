using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformancePicture : MonoBehaviour
{
    private RawImage rawImage;
    void Start()
    {
        rawImage = this.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rawImage.texture != null)
        {
            rawImage.color = Color.white;
        }
        else
        {
            rawImage.color = Color.clear;
        }
    }
}
