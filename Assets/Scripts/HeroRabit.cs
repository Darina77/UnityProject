using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {


    private  Vector3 BIG_SIZE = new Vector3(3, 3, 0);
    private Vector3 SMALL_SIZE = new Vector3(2, 2, 0);
    private Color32 RED_COLOR = new Color32(248, 120, 120, 143);
    private Color32 WITHE_COLOR = new Color32(255, 255, 255, 255);

    public float speed = 1.0f;
    public float MaxJumpTime = 2.0f;
    public float JumpSpeed = 2.0f;
    public int healthLimit = 2;
    public float invulnerableTime = 4.0f;

    Rigidbody2D myBody = null;
    Transform heroParent = null;
    Vector3 targetScale;
    Color32 targetColor; 

    bool isGrounded = false;
    bool JumpActive = false;
    public bool isInvulnerable = false;

    float JumpTime = 0f;
    public int currentHealth = 1;
    
  
    void Start () {
        LevelController.current.setStartPosition(transform.position);
        this.myBody = this.GetComponent<Rigidbody2D>();
        this.heroParent = this.transform.parent;
        this.targetScale = SMALL_SIZE;
        this.targetColor = WITHE_COLOR;
    }


    Vector3 vel = Vector3.one;
    // Update is called once per frame
    void FixedUpdate()
    {
        
        run();
        jump();
        StartCoroutine(die());
        this.transform.localScale = Vector3.SmoothDamp(this.transform.localScale, this.targetScale, ref vel, 0.5f);
        StartCoroutine(invulnerable());
    }

    private void run()
    {
        //[-1, 1]
        float value = Input.GetAxis("Horizontal");
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Animator animator = GetComponent<Animator>();

        if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }
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

    private IEnumerator die()
    {
        if (currentHealth <= 0)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("dead", true);
            currentHealth = 1;
            yield return new WaitForSeconds(2.0f);

            animator.SetBool("dead", false);
            LevelController.current.onRabitDeath(this);
            
        }
    }

    private bool red = false;
    private IEnumerator invulnerable()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (isInvulnerable)
        {
            if (red)
            {
                this.targetColor = RED_COLOR;
                yield return new WaitForSeconds(0.3f);
                red = false;
            }
            else
            {
                this.targetColor = WITHE_COLOR;
                yield return new WaitForSeconds(0.3f);
                red = true;
            }
            sr.color = this.targetColor;
            yield return new WaitForSeconds(invulnerableTime);
            isInvulnerable = false;
        }
        else
        {
            this.targetColor = WITHE_COLOR;
            sr.color = targetColor;
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
            this.targetScale = BIG_SIZE;
        }
    }

    public void removeOneHealth()
    {
        if (!isInvulnerable)
        { 

            if (currentHealth > 1)
            {
                currentHealth--;
                this.targetScale = SMALL_SIZE;
                this.isInvulnerable = true;
            }
            else
            {
                currentHealth--;
                this.targetScale = SMALL_SIZE;
            }
        }   
    }

    
}
