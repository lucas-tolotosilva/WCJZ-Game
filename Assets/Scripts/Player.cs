using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //Para acessar os Text 
using UnityEngine.SceneManagement; //Para add a função de restart

public class Player : MonoBehaviour
{
    [Header("Atributos")]
    public float speed;
    public float jumpForce;
    public int life;
    public int pineapple;

    [Header("Componentes")]
    //Acesso ao RigidBody2d
    public Rigidbody2D rig;
    //Acesso ao Animator
    public Animator anim;
    //Acesso ao SpriteRenderer
    public SpriteRenderer sprite;

    [Header("UI")]
    public TextMeshProUGUI pineappleText;
    public TextMeshProUGUI lifeText;
    public GameObject gameOver;


    // Variável Vector2 declara variável x e y
    private Vector2 direction;
    //verificar se está no chão ou não
    private bool isGrounded;
    //Para controlar se ele tomou dano, não ficar tomando dano logo em seguida
    private bool recovery;

    public static Player player;


    // Start is called before the first frame update
    void Start()
    {
        //Para sempre começar com a vida máxima
        lifeText.text = life.ToString();
        Time.timeScale = 1;

        //Para ao passar de cena não destruir o objeto
        DontDestroyOnLoad(gameObject);
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
        if(life <= 0)
        {
            gameOver.SetActive(true);
        }
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
                Time.timeScale = 1;
            }
            
        }
    }

    public void Hit()
    {
        
        if(recovery == false)
        {
            StartCoroutine(Flick());
        }
       
    }

    // Coroutine
    // Setar uma ação em um determinado tempo- por exemplo, pintar de verde depois de um segundo
    IEnumerator Flick()
    {
        //Faz piscar a cor do Player

        recovery = true;

        life--;
        lifeText.text = life.ToString();

        Death();
        //manipular o alpha da cor do SpriteRenderer do Objeto player
        sprite.color = new Color(1, 1, 1, 0);
        //Manipular tempo
        yield return new WaitForSeconds(0.2f);

        sprite.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.2f);

        sprite.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.2f);

        sprite.color = new Color(1, 1, 1, 1);

        recovery = false;
    }

    public void IncreaseScore()
    {
        pineapple++;
        pineappleText.text = pineapple.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Awake()
    {
        if (player == null)
        {
            player = this;
            DontDestroyOnLoad(this.gameObject); //mantém o objetivo ao passar de cena
        }
        else if (player != this)
        {
            Destroy(gameObject);
        }
    }

    //Resetar as configurações do Player
    public void ResetGamePresets()
    {
        sprite.color = new Color(1, 1, 1, 1);
        gameOver.setActive(false);
        life = 5;
        lifeText.text = life.ToString();
        Time.timeScale = 1;
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
