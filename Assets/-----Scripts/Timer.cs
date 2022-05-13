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
    //�^�C�}�[�\���e�L�X�g
    [SerializeField]
    private TextMeshProUGUI timerText;
    //�o�ߕb��
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
        //���Ԍv��
        TakeTime();
 
    }

    void TakeTime()
    {
        //1�b���₷
        seconds += Time.deltaTime;
        //TimeSpan�N���X���g���Ď��ԕb�����擾���邽�߂̏���
        var timeSpan = new TimeSpan(0, 0, (int)seconds);
        //���l���X�V
        timerText.SetText("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        //���Ԃ������肷����ƃQ�[���I�[�o�[
        if(seconds >= 60 * 60 * 60)
        {
            gameManager.EndGame();
        }

    }
    //���Ԍo�߂�Ԃ�
    public int GetSeconds()
    {
        return (int)seconds;
    }
}

