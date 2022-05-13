using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    //　ゲームオーバー用Canvas
    [SerializeField]
    private GameObject gameOverCanvas;
    //　タイトルシーンへ移動するボタン
    private GameObject goToTitleButton;
    //停止中に表示するUI
    [SerializeField]
    private GameObject pauseUI;
    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.SetActive(false);
        //スタート時にポーズUIを非表示にする
        pauseUI.SetActive(false);

        goToTitleButton = gameOverCanvas.transform.Find("GoToTitleButton").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズボタンを押したとき
        if(Input.GetButtonDown("Pause"))
        {
            if (Mathf.Approximately(Time.timeScale, 1f))
            {
                Time.timeScale = 0f;
                pauseUI.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                pauseUI.SetActive(false);
            }

            gameOverCanvas.SetActive(true);

            
        }

    }
    //　タイトルシーンへ移動
    public void GoToTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    }
