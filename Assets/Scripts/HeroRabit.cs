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
    public AudioClip deathSound = null;
    public AudioClip goSound = null;
    public AudioClip jumpSound = null;

    Rigidbody2D myBody = null;
    Transform heroParent = null;
    Vector3 targetScale;
    Color32 targetColor;
    AudioSource deathSource = null;
    AudioSource goSource = null;
    AudioSource jumpSource = null;

    bool isGrounded = true;
    bool JumpActive = false;
    bool isInvulnerable = false;
    bool canChangeScale = true;

    float JumpTime = 0f;
    int currentHealth = 1;


    public static HeroRabit lastRabit = null;
    void Awake()
    {
        lastRabit = this;
    }

    void Start () {
        LevelController.current.setStartPosition(transform.position);
        this.myBody = this.GetComponent<Rigidbody2D>();
        this.heroParent = this.transform.parent;
        this.targetScale = SMALL_SIZE;
        this.targetColor = WITHE_COLOR;
        this.deathSource = gameObject.AddComponent<AudioSource>();
        this.deathSource.clip = deathSound;
        this.goSource = gameObject.AddComponent<AudioSource>();
        this.goSource.spatialBlend = 1;
        this.goSource.clip = goSound;
        this.jumpSource = gameObject.AddComponent<AudioSource>();
        this.jumpSource.clip = jumpSound;
       
    }

    public void makeSmall()
    {
        currentHealth = 1;
        this.targetScale = SMALL_SIZE;
    }

    Vector3 vel = Vector3.one;
    // Update is called once per frame
    void FixedUpdate()
    {
        
        run();
        jump();
        StartCoroutine(die());
        if (canChangeScale)
            this.transform.localScale = Vector3.SmoothDamp(this.transform.localScale, this.targetScale, ref vel, 0.5f);
        StartCoroutine(invulnerable());
    }

   

    private void run()
    {
        //[-1, 1]
        if (myBody)
        {
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
                if (SoundManager.Instance.isSoundOn() && !goSource.isPlaying && isGrounded)
                {
                    goSource.Play();
                }
                else if (!isGrounded)
                {
                    goSource.Stop();
                }
                animator.SetBool("run", true);

            }
            else
            {
                animator.SetBool("run", false);
                if (SoundManager.Instance.isSoundOn())
                {
                    goSource.Stop();
                }
            }
        }
        
    }

    private IEnumerator die()
    {

        if (currentHealth <= 0)
        {
            if (SoundManager.Instance.isSoundOn())
            {
                deathSource.Play();
            }

            Animator animator = GetComponent<Animator>();
            animator.SetBool("dead", true);
            
            this.GetComponent<BoxCollider2D>().isTrigger = true;
            if (myBody != null) Destroy(myBody);


            currentHealth = 1;
            yield return new WaitForSeconds(3.0f);

            animator.SetBool("dead", false);

            LevelController.current.onRabitDeath(this);
            this.GetComponent<BoxCollider2D>().isTrigger = false;
            myBody = this.gameObject.AddComponent<Rigidbody2D>();
            myBody.freezeRotation = true;
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
        if (myBody)
        {
            Vector3 from = transform.position + Vector3.up * 0.5f;
            Vector3 to = transform.position + Vector3.down * 0.5f;
            int layer_id = 1 << LayerMask.NameToLayer("Graund");
            RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
            Animator animator = GetComponent<Animator>();


            if (hit)
            {
                if(!isGrounded && (SoundManager.Instance.isSoundOn())) jumpSource.Play();
                isGrounded = true;
                //Перевіряємо чи ми опинились на платформі
                if (hit.transform != null
                && hit.transform.GetComponent<MovingPlatform>() != null)
                {
                    //Приліпаємо до платформи
                    this.transform.parent = hit.transform;
                    
                    canChangeScale = false;
                }
                else canChangeScale = true;

            }
            else
            {
                if (isGrounded && SoundManager.Instance.isSoundOn()) 
                    jumpSource.Play();
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
