using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScripts : MonoBehaviour
{
    //�����x�@��
    public float speed = 0.01f;
    //�A���t�@�l�𑀍삷�邽�߂̕ϐ�
    float alfa;
    //RGB�𑀍삷�邽�߂̕ϐ�
    float red, green, blue;

    // Start is called before the first frame update
    void Start()
    {
        //Panel�̐F���擾
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa += speed;
    }
}
