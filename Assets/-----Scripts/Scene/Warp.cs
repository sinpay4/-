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
        //移動先の座標を取得する
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
                //Playerを数秒後にワープさせる
                obj.transform.position = vector3;
            }


    }
    }
    void OnTriggerExit(Collider other)
    {
        //移動可能にする。
        moveStatus = true;
    }
}
