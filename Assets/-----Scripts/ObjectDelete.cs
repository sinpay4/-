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
        //プレイヤーが侵入したらゲームオブジェクト無効
        if(other.CompareTag("Player"))
        {
            //指定したゲームオブジェクトの効果処理を実行する
            greenObject.GetComponent<Effect>().AddEffect();
            Destroy(gameObject);

        }
    }
}
