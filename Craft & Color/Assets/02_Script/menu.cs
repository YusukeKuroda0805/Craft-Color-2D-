using UnityEngine;
using System.Collections;
using UnityEngine.UI; // UIコンポーネントの使用

public class menu : MonoBehaviour
{
    Button start;
    Button quit;

    void Start()
    {
        // ボタンコンポーネントの取得
        start = GameObject.Find("/Canvas/Button1").GetComponent<Button>();
        quit = GameObject.Find("/Canvas/Button2").GetComponent<Button>();

        // 最初に選択状態にしたいボタンの設定
        start.Select();
    }
}
