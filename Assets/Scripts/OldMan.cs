using UnityEngine;

public class OldMan : MonoBehaviour
{
    public CharacterController controller;
    Animator anim;
    private Rigidbody rb;
    public float speed = 10;
    public int jumpSpeed = 10;
    public GameObject ball;
    public GameObject flashLight;
    bool flashLightBool = true;
    public GameObject flashLightCover;
    
    public Transform ballSpawnPoint;
    public Transform cam;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    bool isOnSlope = false;
    bool isJumping = false;

    bool isRayColliding = false;
    bool isRaySlope = false;
    bool isGrounded = false;
    //float gravity = -55f;
    
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        OldManUpdate();

        //print("isJumping = " + isJumping);
        //print("isOnSlope = " + isOnSlope);
        //print("isGrounded = " + isGrounded);
        print(rb.linearVelocity.y);
    }
    
    void FixedUpdate()
    {
        if (isRaySlope == true)
        {
            print("adding force on slope");
            rb.AddForce(0f, -40f, 0f);
        }
        
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
        if (Input.GetKeyDown("l"))
        {
            if (flashLightBool == true)
            {
                flashLightBool = false;
                flashLight.SetActive(false);
                flashLightCover.SetActive(true);
            }
            else
            {
                flashLightBool = true;
                flashLight.SetActive(true);
                flashLightCover.SetActive(false);

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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            isJumping = true;
            rb.linearVelocity = new Vector3(0, jumpSpeed, 0);
            

        }
        if (isJumping == true && rb.linearVelocity.y < 0)
        {
            isJumping = false;
        }




        Raycast();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "slope")
        {
            isOnSlope = true;
        }
        
        if (collision.gameObject.layer == 6)
        {
            
            isGrounded = true;
        }
       


    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "slope")
        {
            isOnSlope = true;
        }

        if (collision.gameObject.layer == 6)
        {

            isGrounded = true;
        }



    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "slope")
        {
            isOnSlope = false;
        }
        

        if (collision.gameObject.layer == 6)
        {

            isGrounded = false;
        }
        

    }
    
    

    private void Raycast()
    {
        Vector3 offset = new Vector3(0, -1.74f, 0);
        var ray = new Ray(transform.position + offset , Vector3.down);
        
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 5f))
        {
            //isGrounded = true;
            isRayColliding = true;
            if (hitInfo.collider.gameObject.tag == "slope" && rb.linearVelocity.y < 0)
            {
                isRaySlope = true;
            }
            else
            {
                isRaySlope = false;
            }
            
            if (hitInfo.collider.gameObject.layer == 6)
            {
               // isGrounded = true;
            }
            else
            {
               // isGrounded = false;
            }

        }
        else
        {
            //isGrounded = false;
            isRayColliding = false;
        }
            Debug.DrawRay(transform.position + offset, Vector3.down, Color.red);
    }



}
