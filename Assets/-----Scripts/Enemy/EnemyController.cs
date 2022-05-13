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

    //�L�����N�^�[�𑀍�\���ǂ���
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
        //�Q�[���I�[�o�[���ɒ�~
        if (gameManager.GameOver)
        {
            navMeshAgent.isStopped = true;
            animator.SetFloat("Speed", 0f);
            canControl = false;
            return;
        }
        //�ړI�n�̍Đݒ�
        navMeshAgent.SetDestination(player.position);

        //�v���C���[�L�����N�^�[�ƓG�̋���
        var characterDistance = Vector2.Distance(new Vector2(player.position.x, player.position.z),
            new Vector2(transform.position.x, transform.position.z));
        //�~�܂��Ă���Ƃ�
        if (navMeshAgent.isStopped)
        {
            //�������J���ƍēx�ǂ������� �����͖���
            if (characterDistance > 2f)
            {
                navMeshAgent.isStopped = false;
            }
        }
        else
        {
            //�ړI�n�Ƃ̋������J���Ă���ꍇ
            if (characterDistance > 0.8f)
            {
                navMeshAgent.isStopped = false;
                animator.SetFloat("Speed", navMeshAgent.speed);
            }
            else
            {
                navMeshAgent.isStopped = true;
                animator.SetFloat("Speed", 0f);
                //�v���C���[�ɋ߂Â����̂Ń_���[�W��^����
                playerController.TakeDamage(attackPower);
            }
        }
    }
}
