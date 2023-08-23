using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

public class CharacterController : Entity
{
    [Header("movingSetting")]
    [SerializeField]
    public float MaxSpeed;
    [SerializeField]
    public float JumpForce;
    [Header("overlapSetting")]
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Vector3 boxSize;
    [SerializeField]
    private LayerMask groundLayer;
    [Header("wallActSetting")]
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private LayerMask wallLayer;
    
    Rigidbody rigid;

    private float horizontal;
    //the direction the player is looking
    private bool isFacingRight = true;
    //variable to store the position of the previous frame
    private Vector3 prevPosition;

    private bool isFancingRight = true;
    private float wallSlidingSpeed;
    private bool isWallSliding;
    /* int Player_hp;

     private void Start()
     {
         Player_hp = this.hp;
     }*/

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigid.velocity = new Vector3(rigid.velocity.x, JumpForce);
        }

        if (Input.GetButtonUp("Jump") && rigid.velocity.y > 0f)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y * 0.5f);
        }

        WallSlide();

        /*
        if(Player_hp > this.hp)
        {
            Debug.Log("Player got damaged");
            Ondamaged();
        }
        if(this. hp <= 0)
        {
            Debug.Log("Dead");
        }

        Player_hp = this.hp;
        */


    }
    /*void Ondamaged()
    {
    /
    // 무적방식 1=> layer의 변경을 통한 무적 but entity를 통해 hp를 받으니 방법 변경 필요
    // + 맞았을때 어디가 맞았는지의 방향을 알아야함
        // Layer Change
        gameObject.layer = 10;
        
        // Dameged Reaction
        
        rigid.AddForce(new Vector3(1, 1)*7, ForceMode.Impulse);
    
        //Layer Backed
        Invoke("OffDamaged",1);
    }

     void OffDamaged()
    {
        gameObject.layer = 10; 이거는 attacted 변경으로 수정할 생각입니다.
    }
     
     */

    void FixedUpdate()
    { 
        //Moving
        MovePlayer();

        IsWalled();
    }


   
    

    void MovePlayer() {
        // horizontal moving
        rigid.AddForce(Vector3.right * horizontal, ForceMode.Impulse);

        if (rigid.velocity.x > MaxSpeed)
            rigid.velocity = new Vector3(MaxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < MaxSpeed * (-1))
            rigid.velocity = new Vector3(MaxSpeed*(-1), rigid.velocity.y);


        if ((horizontal > 0 && !isFacingRight) || (horizontal < 0 && isFacingRight))
        {
            // Flip character if direction changes
            FlipCharacter();
        }

        // save the current position
        prevPosition = transform.position;

    }

    void FlipCharacter()
    {
        // reverse direction
        isFacingRight = !isFacingRight; 
        Vector3 theScale = transform.localScale;
        // invert the x scale value
        theScale.x *= -1; 
        transform.localScale = theScale;
    }

    private bool IsGrounded()
    {
        Collider[] colliders = Physics.OverlapBox(groundCheck.position,boxSize, Quaternion.identity ,groundLayer);
        return colliders.Length > 0;
    }

    private bool IsWalled()
    {
        if(rigid.velocity.y < 0f)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1 ,0));
            RaycastHit rayHit;
            if (Physics.Raycast(rigid.position, Vector3.down, out rayHit, 1, wallLayer))
            {
                return true;
            }
        }
        return false;
    }

    private void WallSlide()
    {
        if(IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rigid.velocity = new Vector3(rigid.velocity.x, -wallSlidingSpeed);
        }
        else
        {
            isWallSliding=false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, boxSize * 2);
    }
}
