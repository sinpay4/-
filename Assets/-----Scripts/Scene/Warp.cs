using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;
    public Warp transObj;
    private Vector3 vector3;

    public bool moveStatus;

    private void Start()
    {
        //�ړ���̍��W���擾����
        vector3 = transObj.transform.position;
        moveStatus = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("warp");
            if (moveStatus)
            {
                transObj.moveStatus = false;
                //Player�𐔕b��Ƀ��[�v������
                obj.transform.position = vector3;
            }


    }
    }
    void OnTriggerExit(Collider other)
    {
        //�ړ��\�ɂ���B
        moveStatus = true;
    }
}
