using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDelete : MonoBehaviour
{
    [SerializeField]
    private GameObject greenObject;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        //�v���C���[���N��������Q�[���I�u�W�F�N�g����
        if(other.CompareTag("Player"))
        {
            //�w�肵���Q�[���I�u�W�F�N�g�̌��ʏ��������s����
            greenObject.GetComponent<Effect>().AddEffect();
            Destroy(gameObject);

        }
    }
}
