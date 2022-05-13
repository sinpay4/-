using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseInputSystem : MonoBehaviour
{
    
    //　タイトルシーンへ移動するボタン
    [SerializeField]
    private GameObject goToTitleButton;

    //停止中に表示するUI
    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private PlayerInput playerInput;
    private InputAction pauseAction;
    // Start is called before the first frame update
    void Start()
    {
        pauseAction = playerInput.currentActionMap.FindAction("Pause");
        //スタート時にポーズUIを非表示にする
        pauseUI.SetActive(false);
        goToTitleButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズボタンを押したとき
        if (pauseAction.triggered)
        {
            if (Mathf.Approximately(Time.timeScale, 1f))
            {
                Time.timeScale = 0f;
                pauseUI.SetActive(true);
                goToTitleButton.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                pauseUI.SetActive(false);
                goToTitleButton.SetActive(false);
            }

        }
        
    }
    //　タイトルシーンへ移動
    public void GoToTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
