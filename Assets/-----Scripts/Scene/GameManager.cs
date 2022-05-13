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
    //�Q�[���I�[�o�[�̔���
    public bool GameOver { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        //�@�Q�[���֘A������
        GameOver = false;
        IsCountDown = true;
        if (SceneManager.GetActiveScene().name != "Title")
        {
            countDownText = GameObject.Find("MapNameText").GetComponent<TextMeshProUGUI>();
            StartCoroutine(CountDown());
        }
        else
        {
            //�@�^�C�g���V�[���̎��̓X�e�[�W�ԍ���1�ɂ���
            gameManagementData.StageNum = 1;
        }
    }
    //�@�J�E���g�_�E���\��
    IEnumerator CountDown()
    {
        //�@�X�e�[�W���̕\��
        countDownText.text = SceneManager.GetActiveScene().name;
        //�@��������1�b�o�ߖ��ɐ������X�V
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
    //�Q�[���N���A���Ɏ��s
    public void ClearGame()
    {
        GameOver = true;
    }

    //�Q�[���N���A�ł��Ȃ��������Ɏ��s
    public void EndGame()
    {
        GameOver = true;
    }
    public void StartGame()
    {
        playerStatus.Reset();
        SceneManager.LoadScene("Stage1");
    }
    //�@���̃V�[���ֈړ�
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
    //�@�^�C�g���V�[���ֈړ�
    public void GoToTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
