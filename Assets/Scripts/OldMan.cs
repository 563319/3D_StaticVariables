using UnityEngine;

public class OldMan : MonoBehaviour
{
    public CharacterController controller;
    Animator anim;
    private Rigidbody rb;
    public float speed = 8;
    public int jumpSpeed = 15;
    public GameObject ball;
    public Transform ballSpawnPoint;
    public Transform cam;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    bool isOnSlope = false;
    bool isJumping = false;


    bool isGrounded = false;
    
    
    /*
    bool facingLeft = false;
    bool facingRight = false;
    bool facingUp = false;
    bool facingDown = false;
    */

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        OldManUpdate();
        

    }

    private void ThrowBall()
    {
        GameObject clone;
        clone = Instantiate(ball, ballSpawnPoint.position, transform.rotation);
        Rigidbody rb = clone.GetComponent<Rigidbody>();


        rb.linearVelocity = transform.forward * 10;
    }

    public void ThrowEnded()
    {
        anim.SetBool("isThrowing", false);
    }




    public void OldManUpdate()
    {
        //moving and camera stuff 
        
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0, v).normalized;
        float yvel = rb.linearVelocity.y;
        //Vector3 movement;




        if (h > 0 || h < 0 || v > 0 || v < 0)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isThrowing", false);

        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
        }
        if (yvel > 0 && isJumping == true)
        {
            
            anim.SetBool("isJumping", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isThrowing", false);

        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            isJumping = true;

            //yvel = jumpSpeed;
            //rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpSpeed, rb.linearVelocity.z);
            rb.linearVelocity = new Vector3(0, jumpSpeed, 0);
            //rb.AddForce(0, yvel, 0);
            

        }
        else
        {
            isJumping = false;
        }

        if (Input.GetKey("f"))
        {
            print("f detected");

            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isThrowing", true);
            if (anim.GetBool("isThrowing"))
            {
                print("is throwing = true");
            }
        }
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            rb.linearVelocity = new Vector3( moveDir.x*speed, yvel, moveDir.z*speed);

            //controller.Move(moveDir.normalized * speed * Time.deltaTime);
            //rb.linearVelocity = movement * speed; // (moveDir.normalized * speed);

        }
        if (isOnSlope == true)
        {
            rb.AddForce(0, -7, 0);
        }
        
        

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "slope")
        {
            isOnSlope = true;
        }
        else
        {
            isOnSlope = false;
        }

        if (collision.gameObject.layer == 6)
        {
            print("debug layer ground detected");
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


    }
    

}
/*
void Update()
    {

       

        float yvel = rb.linearVelocity.y;

        //float h = Input.GetAxisRaw("Horizontal");// * speed;
        float v = Input.GetAxisRaw("Vertical");// * speed;
        
        if (Input.GetKey("d"))
        {
            rb.transform.Rotate(0.0f, 1.0f, 0.0f, Space.Self);
        }
        if (Input.GetKey("a"))
        {
            rb.transform.Rotate(0.0f, -1.0f, 0.0f, Space.Self);
        }
       
        if (v > 0 || v < 0 )
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isThrowing", false);

        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
        }
        if (yvel > 0)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isThrowing", false);

        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            yvel = jumpSpeed;
            
        }
        
        if (Input.GetKeyDown("f"))
        {
            print("f detected");
            
            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isThrowing", true);
            if (anim.GetBool("isThrowing"))
            {
                print("is throwing = true");
            }
        }
        else 
        {
            //anim.SetBool("isThrowing", false);
            //anim.SetBool("isThrowing", falswe);
        }


        direction = new Vector3(0, yvel) + (v * transform.forward);
        rb.linearVelocity = direction * speed;
    }
*/