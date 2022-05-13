using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
	//�@�Q�[���}�l�[�W���[
	private GameManager gameManager;
	//�@�e�̃v���n�u
	[SerializeField]
	private GameObject bullet;
	//�@�e�𔭎˂���Ԋu�b��
	[SerializeField]
	private float fireInterval = 2f;
	//�@�e�ɉ������
	[SerializeField]
	private float power = 10f;
	//�@�o�ߎ���
	private float nowTime;

	// Start is called before the first frame update
	void Start()
    {
		//�@�Q�[���}�l�[�W���[�̎擾
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    // Update is called once per frame
    void Update()
    {
        //�Q�[���I�[�o�[���͈ȍ~�������Ȃ�
        if (gameManager.IsCountDown || gameManager.GameOver)
        {
			return;
        }
		//��莞�Ԃ��Ƃɒe�𔭎�
		nowTime += Time.deltaTime;
		if(nowTime >= fireInterval)
        {
			Fire();
			nowTime = 0f;
        }
    }

	void Fire()
    {
		//�e�v���ӂ��Ԃ��C���X�^���X��
		var ins = Instantiate(bullet, transform.position, transform.rotation);
		//�e��rigidbody�Ɏ��ʂ𖳎����ė͂�������
		ins.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.Force);


    }
}
