using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public Joystick joystick;
    public float joystickHorizontalSenstivity;
    public float joystickVerticalSensitivity;
    public float horiontalForce;
    public float verticalForce;
    bool isGrounded;
    private Rigidbody2D m_rigidbody2D;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;
    public Transform spawnPoint;
    public bool isJumping;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _move(); 
    }
     
    void _move()
    {
        if (isGrounded)
        {
            if (joystick.Horizontal > joystickHorizontalSenstivity)
            {
                //Move Right
                m_rigidbody2D.AddForce(Vector2.right * horiontalForce * Time.deltaTime);
                m_spriteRenderer.flipX = false;
                m_animator.SetInteger("AnimState", 1);
            }
            else if (joystick.Horizontal < -joystickHorizontalSenstivity)
            {
                //Move Left
                m_rigidbody2D.AddForce(Vector2.left * horiontalForce * Time.deltaTime);
                m_spriteRenderer.flipX = true;
                m_animator.SetInteger("AnimState", 1);

            }
            
            else if(!isJumping)
            {
                //Idle
                m_animator.SetInteger("AnimState", 0);

            }
        }
        if (joystick.Vertical > joystickVerticalSensitivity)
        {
            m_rigidbody2D.AddForce(Vector2.up * verticalForce * Time.deltaTime);
            m_animator.SetInteger("AnimState", 2);
            isJumping = true;
        }
        else 
        {
            isJumping = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //respawn
        if (other.gameObject.CompareTag("DeathPlane"))
        {
            transform.position = spawnPoint.position;
        }
       
    }
}
