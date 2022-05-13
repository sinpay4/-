using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    //�@�Q�[���I�[�o�[�pCanvas
    [SerializeField]
    private GameObject gameOverCanvas;
    //�@�^�C�g���V�[���ֈړ�����{�^��
    private GameObject goToTitleButton;
    //��~���ɕ\������UI
    [SerializeField]
    private GameObject pauseUI;
    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.SetActive(false);
        //�X�^�[�g���Ƀ|�[�YUI���\���ɂ���
        pauseUI.SetActive(false);

        goToTitleButton = gameOverCanvas.transform.Find("GoToTitleButton").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //�|�[�Y�{�^�����������Ƃ�
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
    //�@�^�C�g���V�[���ֈړ�
    public void GoToTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    }
