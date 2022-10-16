using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Lidar com carregamento de Cenas

public class FinalPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Aqui chama método que passa de fase
            //SceneManager.LoadScene("Scene2");
        }
    }
}
