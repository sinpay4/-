using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsCountDown { get; set; }
    private TextMeshProUGUI countDownText;
    [SerializeField]
    private GameManagementData gameManagementData;
    [SerializeField]
    private PlayerStatus playerStatus;
    //ゲームオーバーの判定
    public bool GameOver { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        //　ゲーム関連初期化
        GameOver = false;
        IsCountDown = true;
        if (SceneManager.GetActiveScene().name != "Title")
        {
            countDownText = GameObject.Find("MapNameText").GetComponent<TextMeshProUGUI>();
            StartCoroutine(CountDown());
        }
        else
        {
            //　タイトルシーンの時はステージ番号を1にする
            gameManagementData.StageNum = 1;
        }
    }
    //　カウントダウン表示
    IEnumerator CountDown()
    {
        //　ステージ名の表示
        countDownText.text = SceneManager.GetActiveScene().name;
        //　ここから1秒経過毎に数字を更新
        yield return new WaitForSeconds(1f);
        countDownText.text = "Redy...";
        yield return new WaitForSeconds(1f);
        countDownText.text = "Start!";
        IsCountDown = false;
        yield return new WaitForSeconds(0.5f);
        countDownText.gameObject.SetActive(false);
    }
        // Update is called once per frame
        void Update()
    {
        
    }
    //ゲームクリア時に実行
    public void ClearGame()
    {
        GameOver = true;
    }

    //ゲームクリアできなかった時に実行
    public void EndGame()
    {
        GameOver = true;
    }
    public void StartGame()
    {
        playerStatus.Reset();
        SceneManager.LoadScene("Stage1");
    }
    //　次のシーンへ移動
    public void GoToNextScene()
    {
        if (gameManagementData.StageNum < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene("Stage" + gameManagementData.StageNum);
        }
        else
        {
            SceneManager.LoadScene("Title");
        }
    }
    //　タイトルシーンへ移動
    public void GoToTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
