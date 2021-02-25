using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    static int assignments=0;
    static int totalenemies=0;
    static float totaltime=0.0f;
    
    static float timestart=0;
    static int enemystart=0;
    static int assignmentstart=0;

    public int displayassignments=0;
    public int displayenemies=0;
    public int displaytime=0;

    //Start() variables
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;

    private AudioSource footsteps;

    //FSM
    private enum State { idle, running, jumping, falling, hurt }
    private State state = State.idle;

    //Inspector variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int cherries = 0;
    [SerializeField] private Text cherryText;
    [SerializeField] private float hurtForce = 10f;

    //HealthBar
    public int maxHealth = 100; //69
    public int currentHealth;   //69

    public HealthBar healthBar; //69

    [SerializeField]public int damageamt=20;

    void Start()
    {
        timestart=totaltime;
        enemystart=totalenemies;
        assignmentstart=assignments;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        currentHealth = maxHealth;  //69
        healthBar.SetMaxHealth(maxHealth);   //69
        footsteps=GetComponent<AudioSource>();

    }
    void Update()
    {
        totaltime += Time.deltaTime;
        if (state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state); //sets animation based on Enumerator state

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //totaltime=timestart;
            //totalenemies=enemystart;
            //assignments=assignmentstart;
        }
    }

    public void EndResult(){

        displayassignments = assignments;
        displaytime = (int)totaltime % 60;
        displayenemies = totalenemies;
    }
    private void OnTriggerEnter2D(Collider2D collision) //Trigger for Collectables
    {
        if (collision.tag == "Collectable")
        {
            Destroy(collision.gameObject); //Cherry destroy
            cherries += 1;
            cherryText.text = cherries.ToString(); //Converting number to string
            assignments++;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.JumpedOn();
                Jump2();
                totalenemies++;
            }
            else
            {
                state = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is to my right therefore should be damaged and move left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                    TakeDamage(damageamt);

                }
                else
                {
                    //Enemy is to my left therefore i Should be damaged and move right
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                    TakeDamage(damageamt);
                }
            }

        }


         if (other.gameObject.tag == "Boss")
        {
            Boss boss = other.gameObject.GetComponent<Boss>();
            if (state == State.falling)
            {
                boss.JumpedOn();
                Jump2();
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is to my right therefore should be damaged and move left
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);

                }
                else
                {
                    //Enemy is to my left therefore i Should be damaged and move right
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
            }
            else
            {
                state = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is to my right therefore should be damaged and move left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                    TakeDamage(damageamt);

                }
                else
                {
                    //Enemy is to my left therefore i Should be damaged and move right
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                    TakeDamage(damageamt);
                }
            }

        }

        if (other.gameObject.tag == "Life"){
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void hurtAnimation(){
        state=State.hurt;
        rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
    }


    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        //Moving left
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        //Moving right
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        //Jumping
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    public void Jump()
    {
        if(coll.IsTouchingLayers(ground)){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;  
        } 
    }

    public void Jump2()
    {
        
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;  
    }
    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }

        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            //Moving
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void Footstep(){
        footsteps.Play();
    }
}

