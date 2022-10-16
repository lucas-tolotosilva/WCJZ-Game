using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{  
    //ontrigger usado pois o Objeto Spike tem trigger habilitado
    //não podendo ser usado o mesmo collision do Player por ter o trigger
    //necessário um dos objetos ter o bodycollider - nesse caso o Player tem
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Chamamos um método que dá hit/dano no player
            collision.GetComponent<Player>().Hit();
        }
    }
}
