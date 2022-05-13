using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseInputSystem : MonoBehaviour
{
    
    //�@�^�C�g���V�[���ֈړ�����{�^��
    [SerializeField]
    private GameObject goToTitleButton;

    //��~���ɕ\������UI
    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private PlayerInput playerInput;
    private InputAction pauseAction;
    // Start is called before the first frame update
    void Start()
    {
        pauseAction = playerInput.currentActionMap.FindAction("Pause");
        //�X�^�[�g���Ƀ|�[�YUI���\���ɂ���
        pauseUI.SetActive(false);
        goToTitleButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�|�[�Y�{�^�����������Ƃ�
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
    //�@�^�C�g���V�[���ֈړ�
    public void GoToTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
