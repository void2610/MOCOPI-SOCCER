using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnAncher : MonoBehaviour
{
    [SerializeField]
    private GameObject subAncher;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(subAncher.transform.position.x, this.gameObject.transform.position.y, subAncher.transform.position.z);
        this.gameObject.transform.position = pos;
    }
}
