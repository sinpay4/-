using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        // MainCamera(�������g)��player�Ƃ̑��΋��������߂�
        offset = transform.position - player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //�V�����g�����X�t�H�[���̒l��������
        transform.position = player.transform.position + offset;

    }
}
