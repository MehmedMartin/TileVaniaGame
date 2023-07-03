using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


//TODO find a way to reset boosttime after re entering the boost object 

//

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private bool isFacingRight = true;
    Animator myAnimator;
    [SerializeField] private int BoostTime;
    [SerializeField] private float speedBoost;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpingPower;
    [SerializeField] private float inAirHorizontalSpeedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        //To get players horizontal axis every frame
        horizontal = Input.GetAxisRaw("Horizontal");
        //Checks if Jump Button clicked and if the Player is grounded then lets character jump
        if(Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        //Character Run animation starts if User hold downs  D or A also if isGrounded == true
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            if (isGrounded() == true)
            {
                myAnimator.SetBool("isRunning", true);
            }
            //If Character is not grounded running animation is set to false.
            if(isGrounded() == false)
            {
                Delay();
            }
        }
        //Flips the direction of character
        Flip();
        

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            Delay();    
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if (isGrounded() == false)
        {
            rb.velocity = new Vector2((horizontal * speed) * inAirHorizontalSpeedMultiplier, rb.velocity.y);
        }
    }
    // To make character stop running if he is not grounded
    public void Delay()
    {
        myAnimator.SetBool("isRunning", false);
    }
    //To make character stop Boosting after SpeedBoost time expired
    private  void StopBoosting()
    {
      
        speed = speed / speedBoost;
    }
    
 
    //To give character Speed Boost also Stops speed bost after enough time
    private  void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "SpeedBoost")&& speed <10f)
        {
            speed = speed * speedBoost;
            StartCoroutine(myWaitCoroutine());
        }
    }
    // Coroutine for creating new wait Coroutine
    IEnumerator myWaitCoroutine()
    {
        yield return new WaitForSeconds(BoostTime);
        StopBoosting();
    }
    //checks if the target is grounded
    private bool isGrounded() 
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }    
    //Changes character left to right.
    private void Flip()
    {
        if(isFacingRight && horizontal < 0f ||!isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            
        }  
    }
}
