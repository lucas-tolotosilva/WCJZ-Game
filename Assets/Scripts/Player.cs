using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public int life;
    //Acesso ao RigidBody2d
    public Rigidbody2D rig;
    //Acesso ao Animator
    public Animator anim;
    //Acesso ao SpriteRenderer
    public SpriteRenderer sprite;

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
        Jump();

        PlayAnim();
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
            anim.SetInteger("transition", 2);
            //Próprio do Rigidbody2D - Adiciona Força
            rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
       
    }
    
    // Die
    void Death()
    {

    }

    //Animations
    void PlayAnim()
    {
        if(direction.x > 0)
        {
            // Para limitar quando o player executar a função de pular não executar essas animações 
            if (isGrounded)
            {
                //acessa a transição feita na unity - nesse caso a 1 que é o RUN
                //SetInteger pois o tipo do animator foi INT
                anim.SetInteger("transition", 1);
            }
            // Transform não precisa ser referenciado como Rigidbody, pois já vem setado
            transform.eulerAngles = new Vector3(0, 0);
        }

        if(direction.x < 0)
        {
            if (isGrounded)
            {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector2(0, 180);
        }

        if (direction.x == 0)
        {
            if (isGrounded)
            {
                anim.SetInteger("transition", 1);
            }
            
        }
    }

    public void Hit()
    {
        StartCoroutine(Flick());
        life--;
    }

    // Coroutine
    // Setar uma ação em um determinado tempo- por exemplo, pintar de verde depois de um segundo
    IEnumerator Flick()
    {
        //Faz piscar a cor do Player

        //manipular o alpha da cor do SpriteRenderer do Objeto player
        sprite.color = new Color(1, 1, 1, 0);
        //Manipular tempo
        yield return new WaitForSeconds(0.2f);

        sprite.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.2f);

        sprite.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.2f);

        sprite.color = new Color(1, 1, 1, 1);
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
