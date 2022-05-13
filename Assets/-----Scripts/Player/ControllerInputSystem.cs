using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInputSystem : MonoBehaviour
{
    //�v���C���[�̃X�e�[�^�X�f�[�^
    [SerializeField]
    private PlayerStatus playerStatus;
    //���C�t�Q�[�W�̍X�V�X�N���v�g
    [SerializeField]
    private LifeGaugeUpdate lifeGaugeUpdate;
    //Rigidbody�̃R���|�[�l���g
    private Rigidbody rigidBody;
    //�L�����N�^�[�̃R���C�_
    private CapsuleCollider myCollider;
    //���͒l
    private Vector3 input;
    //�ړ����x
    private Vector3 velocity;
    //�n�ʂɐG��Ă��邩�ǂ���
    [SerializeField]
    private bool isGrounded;
    //�ړ��̑���
    [SerializeField]
    private float moveSpeed = 2f;
    //�W�����v��
    [SerializeField]
    private float jumpPower = 6f;

    [SerializeField]
    private GameManager gameManager;
    //�L�����N�^�[����\��
    private bool canControl;

    //�A�j���[�^�[
    private Animator animator;
    //��i��ڂ̃W�����v��
    [SerializeField]
    private float doubleJumpPower = 5f;
    //�ŏ��̃W�����v�����Ă��邩�ǂ���
    [SerializeField]
    private bool isFirstJump;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.currentActionMap.FindAction("Move");
        jumpAction = playerInput.currentActionMap.FindAction("Jump");
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        myCollider = GetComponent<CapsuleCollider>();

        canControl = true;
    }

    //�n�ʂ̊m�F
    private void CheckGround()
    {
        if (isGrounded)
        {
            return;
        }
        //�A�j���[�V�����p�����[�^��RigidBody��Y��0.1�ȉ���Ground�܂���
        //Enemy���C���[�Ƌ��̃R���C�_���Ԃ������ꍇ�n�ʂɐG��Ă���
        if (animator.GetFloat("JumpPower") <= 0.1f &&
            Physics.CheckSphere(rigidBody.position, myCollider.radius - 0.1f, LayerMask.GetMask("Ground", "Enemy")))
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
    //�ړ��l�ƌ����̌v�Z
    private void Move()
    {
        //�ڐG���Ă���ꍇ
        if (isGrounded)
        {
            //�ړ����x�̒������O���傫����Ε����A�j���[�V����
            if (velocity.magnitude > 0f)
            {
                animator.SetFloat("Speed", velocity.magnitude);
            }
            else
            {
                animator.SetFloat("Speed", 0f);

            }
            //�ړ����x��������
            velocity = Vector3.zero;
        }
        //�ړ����͒l Horizontal���������@Vertical���O�����
        input = new Vector3(moveAction.ReadValue<Vector2>().x, 0f, moveAction.ReadValue<Vector2>().y);
        //
        //���x�̌v�Z
        velocity = new Vector3(input.normalized.x * moveSpeed, 0f, input.normalized.z * moveSpeed);
        //�W�����v����
        Jump();
    }

    //�M�Y���̕\��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //�ݒu���莞�͈͕̔\��
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.down * 0.2f);
    }
    //�_���[�W����
    public void TakeDamage(int damage)
    {
        //HP�����炷
        if (playerStatus.SetHp(playerStatus.GetHp() - damage) <= 0)
        {
            gameManager.EndGame();
        }
        //���C�t�Q�[�W�����炷
        lifeGaugeUpdate.UpdateLifeGauge();
    }
    //�W�����v����
    private void Jump()
    {
        //�ڒn���Ă���ꍇ
        if (isGrounded)
        {
            //�W�����v����
            if (jumpAction.triggered)
            {
                isGrounded = false;
                animator.SetBool("isGrounded", isGrounded);
                isFirstJump = true;
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpPower, rigidBody.velocity.z);
                animator.SetTrigger("Jump");
            }
        }
        else if (isFirstJump && jumpAction.triggered)
        {
            isFirstJump = false;
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, doubleJumpPower, rigidBody.velocity.z);

        }

        //�W�����v�͂��A�j���[�V�����p�����[�^�ɐݒ�
        animator.SetFloat("JumpPower", rigidBody.velocity.y);
    }
    //�Œ�̃t���[�����[�g�ŌĂяo����� project settig �ŕύX�\
    private void FixedUpdate()
    {
        //���͂����鎞�������s
        if (!Mathf.Approximately(input.x, 0f) || !Mathf.Approximately(input.z, 0f))
        {
            //���͕����Ɍ�����
            rigidBody.MoveRotation(Quaternion.LookRotation(input.normalized));
            /*
            //���͂�����Ƃ��������s
            if(input.magnitude > 0f)
            {
                //���͕����Ɍ�����
                rigidBody.MoveRotation(Quaternion.LookRotation(input.normalized));
            }
            */

            //���݂̈ʒu�ɂP�t���[���̑��x���𑫂��Ĉړ�
            if (velocity.magnitude > 0f)
            {
                rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(gameManager.IsCountDown)
        {
            return;
        }
        //����s�\�̎��͉������Ȃ�
        if (!canControl)
        {
            return;
        }
        //�Q�[���I�[�o�[�����Z�b�g����
        if (gameManager.GameOver)
        {
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            rigidBody.isKinematic = true;
            canControl = false;
            return;
        }

        //�n�ʂƂ̐ڐG�m�F
        CheckGround();
        //�ړ����x�̌v�Z
        Move();

    }
}

