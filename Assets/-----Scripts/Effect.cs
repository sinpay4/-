using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
	//　表示するパーティクル
	[SerializeField]
	private GameObject particle;
	private AudioSource audioSource;
	//　消去中かどうか
	private bool erasing;

	// Start is called before the first frame update
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		//　消去中でなければ何もしない
		if (!erasing)
		{
			return;
		}
		//　音声が再生されていなければパーティクルを表示してゲームオブジェクトを非表示にする
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
