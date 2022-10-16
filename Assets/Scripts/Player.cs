using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public int life;
    public Rigidbody2D rig;

    // Variável Vector2 declara variável x e y
    private Vector2 direction;
    //verificar se está no chão ou não
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
    }

    //usado para a física 
    void FixedUpdate() 
    {
        Movement();        
    }

    // Walk
    void Movement()
    {
        rig.velocity = new Vector2(direction.x * speed, rig.velocity.y);
    }

    // Jump
    void Jump()
    {   
        if(Input.GetButtonDown("Jump") && isGrounded == true)
        {
            //Próprio do Rigidbody2D - Adiciona Força
            rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
       
    }
    
    // Die
    void Death()
    {

    }

    // Lido toda vez que o objeto Player encontrar em outro objeto na cena - nesse caso quando encostar no chão
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.layer == 6)
        {
            //Verifica se está encostando no chão
            isGrounded = true;
        }
    }
}
