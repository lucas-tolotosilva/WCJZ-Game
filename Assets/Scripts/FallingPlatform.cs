using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    //Tempo que demora para a plataforma cair
    public float fallingTime;

    public BoxCollider2D boxCollider;
    public TargetJoint2D joint;

    void Falling()
    {
        //Desabilitar o collider e o joint target da plataforma
        boxCollider.enabled = false;    
        joint.enabled = false;
    }

    public void OnCollisorEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //Chamar um método depois de um determinado tempo - invoke
            Invoke("Falling", fallingTime);

            Destroy(gameObject, 2);
        }
    }
}
