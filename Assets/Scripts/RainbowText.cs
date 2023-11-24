using UnityEngine;
using UnityEngine.UI;

public class RainbowText : MonoBehaviour
{
    public Text rainbowText;
    public float colorChangeSpeed = 1.0f;

    void Start()
    {
        rainbowText = this.GetComponent<Text>();
    }

    void Update()
    {
        // 時間によって色を変化させる
        float t = Time.time * colorChangeSpeed;

        // Mathf.Sinを使用して0から1までのサイクルを生成
        float lerpValue = Mathf.Sin(t) * 0.5f + 0.5f;

        // 色相を0から1に変化させ、HSVからRGBに変換
        Color rainbowColor = Color.HSVToRGB(lerpValue, 1.0f, 1.0f);

        // Textコンポーネントの色を設定
        rainbowText.color = rainbowColor;
    }
}
