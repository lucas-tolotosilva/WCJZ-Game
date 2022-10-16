using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pineapple : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Add 1 abacaxi no invent�rio
            collision.GetComponent<Player>().IncreaseScore();
            Destroy(gameObject);
        }
    }
}
