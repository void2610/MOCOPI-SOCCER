using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
        // Triggerに触れたときに取得する値
        
    private int triggerValue = 0;
    
    //Vector3 velocity;
    public Vector3 velocity = Vector3.zero;
    public Vector3 direction = Vector3.zero;
    public Vector3 previousPosition = Vector3.zero;
    public Vector3 previousVelocity = Vector3.zero;
    public float forceMagnitude = 0;
       
     void Start()
    {
        previousPosition = transform.position;
    }
    
    void FixedUpdate()
    {
       
        velocity = transform.position - previousPosition; // 前フレームからの移動量を取得
        direction = velocity.normalized; // 移動方向を取得

         // 前フレームからの速度変化を計算
        Vector3 velocityChange = velocity - previousVelocity;

        // ベクトルの大きさ（Magnitude）を取得し、それを float 型の変数に格納
        forceMagnitude = velocityChange.magnitude;

        // デバッグログに推定される力の大きさを表示
        

        // 現在の速度を前フレームの速度として保存
        previousVelocity = velocity;

        direction *= 50;
        previousPosition = transform.position;   
    }

    
    // Triggerに他のColliderが入ったときに呼ばれる
    private void OnTriggerEnter(Collider other)
    {



        // Ballスクリプトを持っている物に触れた際に実行
        if(other.GetComponent<Ball>())
        {
            // 別のスクリプトを呼び出す
            GameManager.instance.KickBallToGoal(forceMagnitude*300,direction);
        }
  }
}