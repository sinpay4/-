using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int attackPower = 2;
    private PlayerController playerController;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform player;

    //キャラクターを操作可能かどうか
    private bool canControl;
    // Start is called before the first frame update
    void Start()
    {
        canControl = true;
        player = GameObject.Find("Player").transform;
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.IsCountDown)
        {
            return;
        }

            if (!canControl)
        {
            return;
        }
        //ゲームオーバー時に停止
        if (gameManager.GameOver)
        {
            navMeshAgent.isStopped = true;
            animator.SetFloat("Speed", 0f);
            canControl = false;
            return;
        }
        //目的地の再設定
        navMeshAgent.SetDestination(player.position);

        //プレイヤーキャラクターと敵の距離
        var characterDistance = Vector2.Distance(new Vector2(player.position.x, player.position.z),
            new Vector2(transform.position.x, transform.position.z));
        //止まっているとき
        if (navMeshAgent.isStopped)
        {
            //距離が開くと再度追いかける 高さは無視
            if (characterDistance > 2f)
            {
                navMeshAgent.isStopped = false;
            }
        }
        else
        {
            //目的地との距離が開いている場合
            if (characterDistance > 0.8f)
            {
                navMeshAgent.isStopped = false;
                animator.SetFloat("Speed", navMeshAgent.speed);
            }
            else
            {
                navMeshAgent.isStopped = true;
                animator.SetFloat("Speed", 0f);
                //プレイヤーに近づいたのでダメージを与える
                playerController.TakeDamage(attackPower);
            }
        }
    }
}
