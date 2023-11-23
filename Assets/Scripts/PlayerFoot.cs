using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
        // Triggerに触れたときに取得する値
        
    private int triggerValue = 0;
    
    //Vector3 velocity;
    Vector3 velocity;
    Vector3 direction;
    Vector3 previousPosition; 
    Vector3 previousVelocity;
    float forceMagnitude;
   
    void Update()
    {
       
        velocity = transform.position - previousPosition; // 前フレームからの移動量を取得
        direction = velocity.normalized; // 移動方向を取得
        direction.x = direction.x*10;
        direction.y = direction.y*10;
        direction.z = direction.z*10;
        previousPosition = transform.position;
         // 前フレームからの速度変化を計算
        Vector3 velocityChange = velocity - previousVelocity;

        // 経過時間（デルタタイム）を取得
        float deltaTime = Time.fixedDeltaTime;

        // 加速度の計算
        Vector3 acceleration = velocityChange / deltaTime;

     
        // 推定される力の計算（近似的な値）
        Vector3 force = 1 * acceleration;

        // ベクトルの大きさ（Magnitude）を取得し、それを float 型の変数に格納
        forceMagnitude = force.magnitude;

        // デバッグログに推定される力の大きさを表示
        

        // 現在の速度を前フレームの速度として保存
        previousVelocity = velocity;
        // velocityやdirectionを使った処理
            
    }
    
     void Start()
    {
        previousPosition = transform.position;
    }
    
    
    // Triggerに他のColliderが入ったときに呼ばれる
    private void OnTriggerEnter(Collider other)
    {



        // Ballスクリプトを持っている物に触れた際に実行
        Ball BallComponent = other.GetComponent<Ball>();
        if (BallComponent != null)
        {
            
            Debug.Log("kick" );

            // 別のスクリプトを呼び出す
            GameManager KickBall = FindObjectOfType<GameManager>();
            if (KickBall != null)
            {
              Debug.Log("Velocity: " + forceMagnitude);
              Debug.Log("direction: " + direction);
                KickBall.KickBallToGoal(forceMagnitude*100,direction);
            }
        }
  }
}