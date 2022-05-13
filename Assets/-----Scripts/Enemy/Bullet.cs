using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int attackPower = 1;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //�����������Ă���P�O�b��ɏ���
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameManager.GameOver)
        {
            Destroy(gameObject);
            return;
        }
        //�Փ˂������肪�v���C���[�Ȃ�_���[�W��^���A����ɗ͂������Ĕ�΂�
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<ControllerInputSystem>().TakeDamage(attackPower);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.VelocityChange);
        }
        //���炩�̃Q�[���I�u�W�F�N�g�ɏՓ˂��������
        Destroy(gameObject);
    }
    void Update()
    {
        //�@�J�E���g�_�E������Q�[���I�[�o�[���͈ȍ~�������Ȃ�
        if(gameManager.IsCountDown || gameManager.GameOver)
        {
            return;
        }

    }
}
