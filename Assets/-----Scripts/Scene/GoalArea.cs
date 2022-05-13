using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalArea : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    //ゲームクリアフラグ
    private bool gameClear;

    // Start is called before the first frame update
    void Start()
    {
        /*
        //ゲームマネージャーの取得
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    */
        }

    private void OnTriggerEnter(Collider other)
    {
        //　!　は false　を表す。つまり、 !gameClear　は gameClear でない状態
        if(!gameClear && other.CompareTag("Player"))
        {
            gameClear = true;
            gameManager.ClearGame();

        }
    }
}
