using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 3;
    public float speed;
    public float jumpForce;

    public GameObject bow;
    public Transform firePoint;

    private bool isJumping;
    private bool doubleJump;
    private bool isFire;
    private bool canFire = true;

    private Rigidbody2D rig;
    private Animator anim;

    public float movement;
    public bool isMobile;

    public bool touchJump;
    public bool touchFire;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameController.instance.UpdateLives(health);
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        BowFire();
    }


    void FixedUpdate()
    {
        Move();
    }

    //metodo para mover o player e tambem habilitar animações de mover
    void Move()
    {
        if (!isMobile)
        {
            //se não pressionar nada valor 0. Pressionar direito valor maximo 1. Esquerda valor maximo -1
            movement = Input.GetAxis("Horizontal");
        }

        
        //adiciona velocidade ao corpo do personagem no eixo x e y
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        //andando pra direita
        if(movement > 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("Transition", 1);
            }
            
            transform.eulerAngles = new Vector3(0,0,0);
        }

        //andando pra esquerda
        if(movement < 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("Transition", 1);
            }
            
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //metodo para permitir a animação iddle, se o player estiver parado, e não estiver pulando, e não estiver atirando,
        if(movement == 0 && !isJumping && !isFire)
        {
            anim.SetInteger("Transition", 0);
        }
    }

    //metodo para fazer o player pular duas vezes e tambem habilitar animações de pular
    void Jump()
    {
        if (Input.GetButtonDown("Jump") || touchJump)
        {
            //checando se a variavel isJumping é false
            if (!isJumping)
            {
                anim.SetInteger("Transition", 2);
                rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            
                doubleJump = true;
                isJumping = true;
            }
            else
            {
                //checando se a variavel doublejump é true
                if (doubleJump)
                {
                    anim.SetInteger("Transition", 2);
                    rig.AddForce(new Vector2(0, jumpForce * 1), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }

            touchJump = false;
        }
    }

    //metodo para fazer o player atirar 
    void BowFire()
    {
        StartCoroutine("Fire");
    }

    //corotina para fazer o player atirar, voltar a idle e poder atirar de novo/tambem habilitar animações de atirar
    IEnumerator Fire()
    {
        if (Input.GetKeyDown(KeyCode.E) || touchFire && canFire == true)
        {
            canFire = false;
            touchFire = false;
            isFire = true;
            
            anim.SetInteger("Transition", 3);
            GameObject Bow = Instantiate(bow, firePoint.position, firePoint.rotation);

            if (transform.rotation.y == 0)
            {
                Bow.GetComponent<Bow>().isRight = true;
            }

            if (transform.rotation.y == 180)
            {
                Bow.GetComponent<Bow>().isRight = false;
            }

            yield return new WaitForSeconds(0.2f);
            isFire = false;
            anim.SetInteger("Transition", 0);
            canFire = true;
            Debug.Log("atirei");
            
        }
    }

    //metodo para fazer o player tomar dano/tambem habilita animação de hit/chama game over
    public void Damage(int dmg)
    {
        health -= dmg;
        GameController.instance.UpdateLives(health);
        anim.SetTrigger("hit");

        
        if (transform.rotation.y == 0)
        {
            transform.position += new Vector3(-0.5f,0,0);
        }

        if (transform.rotation.y == 180)
        {
            transform.position += new Vector3(0.5f, 0, 0);
        }

        if (health <= 0)
        {
            //chamar game over
            GameController.instance.GameOver();
        }
    }

    //metodo para aumentar a vida do player
    public void IncreaseLife(int value)
    {
        health += value;
        GameController.instance.UpdateLives(health);
    }


    //metódo para  controlar o player de pular somente do chão/ permitir o player de dar mais de dois pulos 
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 8)
        {
            isJumping = false;
        }

        if (coll.gameObject.layer == 9)
        {
            GameController.instance.GameOver();
        }
    }



}
