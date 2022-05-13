using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalArea : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    //�Q�[���N���A�t���O
    private bool gameClear;

    // Start is called before the first frame update
    void Start()
    {
        /*
        //�Q�[���}�l�[�W���[�̎擾
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    */
        }

    private void OnTriggerEnter(Collider other)
    {
        //�@!�@�� false�@��\���B�܂�A !gameClear�@�� gameClear �łȂ����
        if(!gameClear && other.CompareTag("Player"))
        {
            gameClear = true;
            gameManager.ClearGame();

        }
    }
}
