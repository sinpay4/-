using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotation : MonoBehaviour
{
    //�C�g����]�����邩�ǂ���
    [SerializeField]
    private bool rotatable;
    //��]�X�s�[�h
    [SerializeField]
    private float rotationSpeed = 2f;
    private Transform player;

    private Rigidbody rigidBody;
    //�C�g�������p�x
    private Quaternion viewingQuaternion;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rigidBody = GetComponent<Rigidbody>();
        viewingQuaternion = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if(rotatable)
        {
            //���X�ɖC�g���v���C���[�̕����Ɍ����邽�߂̏���
            viewingQuaternion = Quaternion.Slerp(rigidBody.rotation, Quaternion.LookRotation
                (player.position - rigidBody.position + Vector3.up * 0.8f), rotationSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        //�C�g�̍�������]������
        if (rotatable)
        {
            rigidBody.MoveRotation(viewingQuaternion);
        }
    }
}
