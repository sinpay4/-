using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    //タイマー表示テキスト
    [SerializeField]
    private TextMeshProUGUI timerText;
    //経過秒数
    private float seconds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.IsCountDown || gameManager.GameOver)
        {
            return;
        }
        //時間計測
        TakeTime();
 
    }

    void TakeTime()
    {
        //1秒増やす
        seconds += Time.deltaTime;
        //TimeSpanクラスを使って時間秒数を取得するための準備
        var timeSpan = new TimeSpan(0, 0, (int)seconds);
        //数値を更新
        timerText.SetText("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        //時間がかかりすぎるとゲームオーバー
        if(seconds >= 60 * 60 * 60)
        {
            gameManager.EndGame();
        }

    }
    //時間経過を返す
    public int GetSeconds()
    {
        return (int)seconds;
    }
}

