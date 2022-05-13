using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //何らかのコライダが侵入してきたら実行
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameManager.EndGame();
        }
    }
}
