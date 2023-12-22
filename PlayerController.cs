using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    
    public Animator animator;
    public Camera cam;

    float horizontal;
    float jump;
    public float speed = 2.0f;
    public float jumpForce = 5.0f;
    bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        isJumping = false;

    }

    // Update is called once per frame
    void Update()
    {      
        horizontal = Input.GetAxis("Horizontal");
        jump = Input.GetAxis("Jump");

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (LevelManager.instance.Mode == "Light")
        {
            
            animator.SetBool("Light", true);
            cam.backgroundColor = Color.white;
        }
        else if (LevelManager.instance.Mode == "Dark")
        {
            animator.SetBool("Light", false);
            cam.backgroundColor = Color.black;
        }

        if (Input.GetKeyDown(KeyCode.I) && isJumping == false)
        {
            Stop();
            LevelManager.instance.ChangeMode();

        }
    }

    void FixedUpdate()
    {
        if(horizontal != 0)
        {
            rigidbody2d.AddForce(new Vector2(horizontal * speed, 0f), ForceMode2D.Impulse);
        }

        if (rigidbody2d.bodyType != RigidbodyType2D.Static)
        {
            if(horizontal < 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if(horizontal > 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }

        if(!isJumping && jump != 0)
        {
            rigidbody2d.AddForce(new Vector2(0f, jump * jumpForce), ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
            animator.SetBool("Fall", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Death")
        {
            rigidbody2d.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            rigidbody2d.bodyType = RigidbodyType2D.Static;
            animator.SetBool("Fall", false);
            animator.SetBool("IsJumping", false);
            animator.SetBool("Dying", true);
            animator.SetBool("Light", false);
        }
    }

    void Death()
    {
            animator.SetBool("Dying", false);
            Destroy(gameObject);
            LevelManager.instance.Respawn();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            isJumping = true;
            if (animator.GetBool("IsJumping") == false)
            {
                animator.SetBool("Fall", true);
            }
        }
    }

    void Stop()
    {
      rigidbody2d.bodyType = RigidbodyType2D.Static;  
    }

    void StartMoving()
    {
      rigidbody2d.bodyType = RigidbodyType2D.Dynamic;  
    }
}