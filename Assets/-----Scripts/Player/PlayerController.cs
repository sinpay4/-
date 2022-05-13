using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //プレイヤーのステータスデータ
    [SerializeField]
    private PlayerStatus playerStatus;
    //ライフゲージの更新スクリプト
    [SerializeField]
    private LifeGaugeUpdate lifeGaugeUpdate;
    //Rigidbodyのコンポーネント
    private Rigidbody rigidBody;
    //キャラクターのコライダ
    private CapsuleCollider myCollider;
    //入力値
    private Vector3 input;
    //移動速度
    private Vector3 velocity;
    //地面に触れているかどうか
    [SerializeField]
    private bool isGrounded;
    //移動の速さ
    [SerializeField]
    private float moveSpeed = 2f;
    //ジャンプ力
    [SerializeField]
    private float jumpPower = 6f;

    [SerializeField]
    private GameManager gameManager;
    //キャラクター操作可能か
    private bool canControl;

    //アニメーター
    private Animator animator;
    //二段回目のジャンプ力
    [SerializeField]
    private float doubleJumpPower = 5f;
    //最初のジャンプをしているかどうか
    [SerializeField]
    private bool isFirstJump;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        myCollider = GetComponent<CapsuleCollider>();

        canControl = true;
    }

    //地面の確認
    private void CheckGround()
    {
        if (isGrounded)
        {
            return;
        }
        //アニメーションパラメータのRigidBodyのYが0.1以下でGroundまたは
        //Enemyレイヤーと球のコライダがぶつかった場合地面に触れている
        if(animator.GetFloat("JumpPower") <= 0.1f && 
            Physics.CheckSphere(rigidBody.position, myCollider.radius - 0.1f, LayerMask.GetMask("Ground","Enemy")))
        {
            isGrounded = true;
            velocity.y = 0f;
        }
        else
        {
            isGrounded = false;
        }
        animator.SetBool("isGrounded", isGrounded);
}
    //移動値と向きの計算
    private void Move()
    {
        //接触している場合
        if(isGrounded)
        {
            //移動速度の長さが０より大きければ歩くアニメーション
            if (velocity.magnitude > 0f)
            {
                animator.SetFloat("Speed", velocity.magnitude);
            } else
            {
                animator.SetFloat("Speed", 0f);

            }
            //移動速度を初期化
            velocity = Vector3.zero;
        }
        //移動入力値 Horizontalが横方向　Verticalが前後方向
        input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        //
        //速度の計算
        velocity = new Vector3(input.normalized.x * moveSpeed, 0f, input.normalized.z * moveSpeed);
        //ジャンプ処理
        Jump();
    }

    //ギズモの表示
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //設置判定時の範囲表示
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.down * 0.2f);
    }
    //ダメージ処理
    public void TakeDamage(int damage)
    {
        //HPを減らす
        if(playerStatus.SetHp(playerStatus.GetHp() - damage) <=0)
        {
            gameManager.EndGame();
        }
        //ライフゲージを減らす
        lifeGaugeUpdate.UpdateLifeGauge();
    }
    //ジャンプ処理
    private void Jump()
    {
        //接地している場合
        if (isGrounded)
        {
            //ジャンプ所理
            if (Input.GetButtonDown("Jump"))
            {
                isGrounded = false;
                animator.SetBool("isGrounded", isGrounded);
                isFirstJump = true;
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpPower, rigidBody.velocity.z);
                animator.SetTrigger("Jump");
            }
        }
        else if (isFirstJump && Input.GetButtonDown("Jump"))
        {
            isFirstJump = false;
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, doubleJumpPower , rigidBody.velocity.z);

        }

        //ジャンプ力をアニメーションパラメータに設定
        animator.SetFloat("JumpPower", rigidBody.velocity.y);
    }
    //固定のフレームレートで呼び出される project settig で変更可能
    private void FixedUpdate()
    {
        //入力がある時だけ実行
        if (!Mathf.Approximately(input.x,0f) || !Mathf.Approximately(input.z, 0f))
        {
            //入力方向に向ける
            rigidBody.MoveRotation(Quaternion.LookRotation(input.normalized));
            /*
            //入力があるときだけ実行
            if(input.magnitude > 0f)
            {
                //入力方向に向ける
                rigidBody.MoveRotation(Quaternion.LookRotation(input.normalized));
            }
            */

            //現在の位置に１フレームの速度分を足して移動
            if(velocity.magnitude > 0f)
            {
                rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //操作不可能の時は何もしない
        if(!canControl)
        {
            return;
        }
        //ゲームオーバー時リセットする
        if(gameManager.GameOver)
        {
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            rigidBody.isKinematic = true;
            canControl = false;
            return;
        }

        //地面との接触確認
        CheckGround();
        //移動速度の計算
        Move();

    }
}
