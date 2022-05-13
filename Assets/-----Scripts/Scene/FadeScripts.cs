using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScripts : MonoBehaviour
{
    //透明度　速
    public float speed = 0.01f;
    //アルファ値を操作するための変数
    float alfa;
    //RGBを操作するための変数
    float red, green, blue;

    // Start is called before the first frame update
    void Start()
    {
        //Panelの色を取得
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
