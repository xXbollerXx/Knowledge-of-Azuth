using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 2; 
    [SerializeField] float JumpPower = 500f;
     Rigidbody2D RG;
     bool isGrounded = false;
     bool Jump = false;
     bool JumpDown = false;
     float HMovement;
    [SerializeField] SpriteRenderer playerSprite;

    [SerializeField] Transform FloorCheckLoc;
     [SerializeField] float FloorCheckRadius = 0.05f;
    [SerializeField] LayerMask FloorLayer;
    [SerializeField] bool ShowFloorCheck = false;
    private PlatformEffector2D FlippedPlatform;
    bool canCoyoteJump = false;
    float CoyoteDelay = 0.1f;
    float CoyoteDelayFlag;

    // Start is called before the first frame update
    void Start()
    {
        if (!RG)
        {
            RG = GetComponent<Rigidbody2D>();
        }

        if (!playerSprite)
        {
            playerSprite = GetComponent<SpriteRenderer>();

        }

    }

    // Update is called once per frame
    void Update()
    {
        HMovement = Input.GetAxisRaw("Horizontal") * Speed;

        playerSprite.flipX = HMovement < 0; // flip sprite in direction of movement

        if (Input.GetButtonDown("Jump"))
        {
            Jump = true;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            Jump = false;
        }

        JumpDown = Input.GetAxisRaw("Vertical") < 0;
    }

    private void FixedUpdate()
    {
        FloorCheck();
        TryToJumpDown();
        Move(HMovement, Jump);
        //FlipPlayer();

    }

    private void Move(float HValue, bool JumpFlag)
    {
        //try to jump 
        //note - might need to add coyote jump 
        if (isGrounded && JumpFlag && RG.velocity.y <= 1) // check velocity so u can't keep jumping when jumping up onto a platform
        {
            RG.AddForce(new Vector2(0f, JumpPower));
            isGrounded = false;
            canCoyoteJump = false;

            if (FlippedPlatform )
            {
                FlippedPlatform.rotationalOffset = 0;
                FlippedPlatform = null;
            }
        }
        else if (JumpFlag && canCoyoteJump && CoyoteDelayFlag >= Time.time) // COYOTE JUMP
        {
            RG.AddForce(new Vector2(0f, JumpPower));
            isGrounded = false;
            canCoyoteJump = false;

            if (FlippedPlatform)
            {
                FlippedPlatform.rotationalOffset = 0;
                FlippedPlatform = null;
            }
        }

        float Temp = HValue * 100 * Speed * Time.fixedDeltaTime;
        RG.velocity = new Vector2(Temp, RG.velocity.y);


        if(HMovement == 0)
        {
            GetComponent<Animator>().SetBool("IsMoving", false);
        }
        else if(HMovement < 0 || HMovement > 0)
        {
            GetComponent<Animator>().SetBool("IsMoving", true);
        }
    }

    void TryToJumpDown()
    {
        // note -- might need a timer if there's ramps 

        if (!JumpDown) return;
        
        Collider2D[] Colliders = Physics2D.OverlapCircleAll(FloorCheckLoc.position, FloorCheckRadius, FloorLayer);
        if (Colliders.Length > 0)
        {
            foreach (Collider2D collider in Colliders)
            {
                if (collider.CompareTag("Platform")) // if the floor is a platform
                {
                    FlippedPlatform = collider.GetComponent<PlatformEffector2D>();
                    FlippedPlatform.rotationalOffset = 180;
                    return;
                }
            }
        }
    }

    private void FloorCheck()// for jumping 
    {
        bool wasGrounded = isGrounded;

        Collider2D[] Colliders = Physics2D.OverlapCircleAll(FloorCheckLoc.position, FloorCheckRadius, FloorLayer);
        isGrounded = Colliders.Length > 0;
        
        if(wasGrounded && !isGrounded)// just fell off or jumped
        {
            canCoyoteJump = true;
            CoyoteDelayFlag = Time.time + CoyoteDelay;
        }
    }

    private void OnDrawGizmos()
    {
        if (ShowFloorCheck)
        {
            Gizmos.DrawSphere(FloorCheckLoc.position, FloorCheckRadius);
        }
    }

    
}
