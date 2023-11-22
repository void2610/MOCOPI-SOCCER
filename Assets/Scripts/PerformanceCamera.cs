using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PerformanceCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector3 offset;

    private Camera captureCamera;
    private string savePath = "Assets/Resources/Screenshots/";

    public Texture2D Capture()
    {
        // カメラのレンダーテクスチャを作成
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        captureCamera.targetTexture = renderTexture;

        // テクスチャをアクティブにしてレンダリング
        captureCamera.Render();
        RenderTexture.active = renderTexture;

        // テクスチャから2Dテクスチャを作成
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // レンダーテクスチャとアクティブなテクスチャをリセット
        captureCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        // 画像を保存
        byte[] bytes = screenshot.EncodeToPNG();
        string fileName = "Screenshot_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        string filePath = Path.Combine(savePath, fileName);
        File.WriteAllBytes(filePath, bytes);

        Debug.Log("Screenshot saved to: " + filePath);

        return screenshot;
    }

    void Start()
    {
        captureCamera = this.GetComponent<Camera>();
    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
