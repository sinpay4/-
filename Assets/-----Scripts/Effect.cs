using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
	//�@�\������p�[�e�B�N��
	[SerializeField]
	private GameObject particle;
	private AudioSource audioSource;
	//�@���������ǂ���
	private bool erasing;

	// Start is called before the first frame update
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		//�@�������łȂ���Ή������Ȃ�
		if (!erasing)
		{
			return;
		}
		//�@�������Đ�����Ă��Ȃ���΃p�[�e�B�N����\�����ăQ�[���I�u�W�F�N�g���\���ɂ���
		if (!audioSource.isPlaying)
		{
			Instantiate(particle, transform.position, Quaternion.identity);
			gameObject.SetActive(false);
		}
	}
	public void AddEffect()
	{
		audioSource.Play();
		erasing = true;
	}
}
