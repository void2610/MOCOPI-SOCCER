using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private GameObject titleProps;
    void Start()
    {
        titleProps = GameObject.Find("TitleProps");
        titleProps.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            titleProps.SetActive(false);
            GameManager.instance.StartGame();
        }
    }
}
