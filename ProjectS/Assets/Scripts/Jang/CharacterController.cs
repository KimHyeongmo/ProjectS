using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

public class CharacterController : Entity
{
    Rigidbody rigid;
    private float horizontal;
    private int nroPulos = 1;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool iscanMove = true;
    private bool isFacingRight = true;  //the direction the player is looking
    private int facingDirection = 1;
    private Vector3 prevPosition; //variable to store the position of the previous frame


    [Header("Parametros Player")]
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;
    [SerializeField] public float wallSlidingSpeed;

    [Header("Parametros Colisores")]
    public Transform feetPos;
    public Transform wallCheck;
    public Vector3 groundcheckSize;
    public Vector3 wallcheckSize;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;

    [Header("Parametros Wall Jump")]
    public float wallJumpForce;
    public Vector3 wallJumpDirection;

    int Player_hp;

    private void Start()
    {
        Player_hp = this.hp;
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckSurroundings();
        CheckInput();
        CheckWallSliding();
        CheckJump();

        if (Player_hp > this.hp)
        {
            Debug.Log("Player got damaged");
            Ondamaged();
        }
        if (this.hp <= 0)
        {
            Debug.Log("Dead");
        }

        Player_hp = this.hp;


    }

    void CheckSurroundings()
    {
        //isGrounded
        Collider[] colliderg = Physics.OverlapBox(feetPos.position, groundcheckSize, Quaternion.identity ,whatIsGround);
        isGrounded = colliderg.Length > 0;
        //isWalled
        Collider[] colliderw = Physics.OverlapBox(wallCheck.position, wallcheckSize, Quaternion.identity, whatIsWall);
        isTouchingWall = colliderw.Length > 0;
    }
    void Ondamaged()
    {
        // 무적방식 1=> layer의 변경을 통한 무적 but entity를 통해 hp를 받으니 방법 변경 필요
        // + 맞았을때 어디가 맞았는지의 방향을 알아야함
        // Layer Change
        gameObject.layer = 10;

        // Dameged Reaction

        rigid.AddForce(new Vector3(1, 1) * 7, ForceMode.Impulse);

        //Layer Backed
        Invoke("OffDamaged", 1);
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
    }

    void CheckJump()
    {
        if (isGrounded && rigid.velocity.y <= 0)
        {
            nroPulos = 1;
        }
    }

    void CheckInput()
    {
        if (iscanMove)
        {
            MovePlayer();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpPlayer();
        }
    }

    private void CheckWallSliding()
    {
        if (isTouchingWall && !isGrounded && rigid.velocity.y < 0 && horizontal != 0)
        {
            isWallSliding = true;
            Invoke("",0.3f);
        }
        else
        {
            isWallSliding = false;
        }
    }

    void JumpPlayer()
    {
        // Pulo normal
        if (nroPulos > 0 && !isWallSliding && isGrounded)
        {
            nroPulos = 0;
            rigid.velocity = Vector3.zero;
            rigid.AddForce(Vector3.up * jumpForce);
            nroPulos = 0;
        }
        //wall Jump
        else if (isWallSliding)
        {

            // x = force * x * (-1 or 1 - Left or right)
            // y = force * y (always upwards)
            Vector3 force = new Vector3(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y);

            // Clear the velocity before assigning to prevent velocity accumulation
            rigid.velocity = Vector3.zero;

            // Apply the force for Wall Jump
            rigid.AddForce(force, ForceMode.Impulse);

            // Temporarily regain control of the character
            StartCoroutine("StopMove");
        }
    }

    IEnumerator StopMove()
    {
        // Remove control from the character
        iscanMove = false;
        // Flip the transform side
        transform.localScale = transform.localScale.x == 1 ? new Vector3(-1, 1,0) : Vector3.one;

        yield return new WaitForSeconds(.3f);

        // Normalize the transform side
        transform.localScale = Vector3.one;
        // Restore control to the character
        iscanMove = true;
    }


    private void MovePlayer()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (!(isWallSliding ))
        {
            rigid.velocity = new Vector3(horizontal * speed, rigid.velocity.y);
        }

        if (isWallSliding)
        {
            if (rigid.velocity.y < -wallSlidingSpeed)
            {
                rigid.velocity = new Vector3(rigid.velocity.x, -wallSlidingSpeed);
            }
        }

        if ((horizontal < 0 && isFacingRight) || (horizontal > 0 && !isFacingRight))
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
        facingDirection *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(feetPos.position, groundcheckSize * 2 );
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(wallCheck.position, wallcheckSize * 2);
    }
}

