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
        //球が発生してから１０秒後に消す
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameManager.GameOver)
        {
            Destroy(gameObject);
            return;
        }
        //衝突した相手がプレイヤーならダメージを与え、さらに力を加えて飛ばす
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<ControllerInputSystem>().TakeDamage(attackPower);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.VelocityChange);
        }
        //何らかのゲームオブジェクトに衝突したら消す
        Destroy(gameObject);
    }
    void Update()
    {
        //　カウントダウン中やゲームオーバー時は以降何もしない
        if(gameManager.IsCountDown || gameManager.GameOver)
        {
            return;
        }

    }
}
