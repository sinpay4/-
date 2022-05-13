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
        // MainCamera(自分自身)とplayerとの相対距離を求める
        offset = transform.position - player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //新しいトランスフォームの値を代入する
        transform.position = player.transform.position + offset;

    }
}
