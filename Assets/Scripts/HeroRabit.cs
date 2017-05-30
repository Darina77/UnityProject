using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {

    public float speed = 1;
    public float MaxJumpTime = 2.0f;
    public float JumpSpeed = 2.0f;
    public int healthLimit = 2;


    Rigidbody2D myBody = null;
    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    Transform heroParent = null;
    bool is_dead = false;
    public int currentHealth = 1;
    float time_to_wait = 0.0f;
    float red_state_time = 4.0f;
    public bool red_state = false;
   // public bool is_red = false;
    public bool is_big = false;
    public bool make_big = false;
    // Use this for initialization
    void Start () {
        myBody = this.GetComponent<Rigidbody2D>();
        LevelController.current.setStartPosition(transform.position);
        this.heroParent = this.transform.parent;
    }
	
  

    // Update is called once per frame
    void FixedUpdate()
    {
        Animator animator = GetComponent<Animator>();
        if (this.is_dead)
        {
            animator.SetBool("dead", true);
            time_to_wait -= Time.deltaTime;
            if (time_to_wait <= 0)
            {
               
                is_dead = false;
                animator.SetBool("dead", false);
                LevelController.current.onRabitDeath(this);
            }
            else return;
        }


        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (red_state)
        {
            red_state_time -= Time.deltaTime;
            sr.color = Color.red;
            if (red_state_time <= 0) red_state = false;
        }
        else sr.color = Color.white;

        if (!is_big && make_big)
        {
            this.transform.localScale =  new Vector3(3, 3, 0);
            is_big = true;
        }
        else if (is_big && !make_big)
        {

            this.transform.localScale = new Vector3(2, 2, 0);
            is_big = false;
        }

        //[-1, 1]
        float value = Input.GetAxis("Horizontal");
        
       
        if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }

        jump();

        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
        }

        
        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }


    
    }

    private void jump()
    {
        Vector3 from = transform.position + Vector3.up * 0.5f;
        Vector3 to = transform.position + Vector3.down * 0.5f;
        int layer_id = 1 << LayerMask.NameToLayer("Graund");
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        Animator animator = GetComponent<Animator>();


        if (hit)
        {
            isGrounded = true;
            //Перевіряємо чи ми опинились на платформі
            if (hit.transform != null
            && hit.transform.GetComponent<MovingPlatform>() != null)
            {
                //Приліпаємо до платформи
                this.transform.parent = hit.transform;
            }
        }
        else
        {
            isGrounded = false;
            this.transform.parent = this.heroParent;
        }
        //Намалювати лінію (для розробника)
        Debug.DrawLine(from, to, Color.red);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }
        if (this.JumpActive)
        {
            //Якщо кнопку ще тримають
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }
        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }
    }

    public void addOneHealth()
    {
        if (currentHealth < healthLimit)
        {
            currentHealth++;
        }
        if(currentHealth == healthLimit)
        {
            make_big = true;
        }
    }

    public void removeOneHealth()
    {
        if (!red_state)
        {
            if (currentHealth > 1)
            {
                currentHealth--;
                make_big = false;
                red_state = true;
                red_state_time = 4.0f;
            }
            else
            {
                currentHealth--;
                time_to_wait = 1.0f;
                is_dead = true;
            }
        }
    }

    
}
