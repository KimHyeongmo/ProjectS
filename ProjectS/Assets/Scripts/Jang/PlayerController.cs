using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerController : Entity
{
    [SerializeField]
    public float MaxSpeed;
    [SerializeField]
    public float JumpForce;
    Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        
    }
    void FixedUpdate()
    {
        //Jumping
        JumpPlayer();
        //Moving
        MovePlayer();
    }

    void JumpPlayer() {
        
    }

    void MovePlayer() {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector3.right * h, ForceMode.Impulse);

        if (rigid.velocity.x > MaxSpeed) // Right Max Speed
            rigid.velocity = new Vector3(MaxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < MaxSpeed * (-1)) // Left Max Speed
            rigid.velocity = new Vector3(MaxSpeed * (-1), rigid.velocity.y);
    }

}
