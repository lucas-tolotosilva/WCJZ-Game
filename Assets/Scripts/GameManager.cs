using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform point;

    // Start is called before the first frame update
    void Start()
    {
        Player player = FindObjectOfType<Player>();

        //Encontra na cena algum objeto que tanha um script Player
        player.transform.position = point.position;
        player.ResetGamePresets();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
