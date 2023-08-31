using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombfruit : Enemy
{
    public GameObject BombTrap;
    public Entity AttackedPlayer;


    void FixedUpdate()
    {
        transform.Translate(0, -0.1f, 0); //일정한 속도로 낙하한다.
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") //플레이어를 Player 오브젝트로 명명한다고 가정
        {
            AttackedPlayer = collision.gameObject.GetComponent<Entity>();
            AttackedPlayer.hp -= 1;
            Debug.Log("Bomb : Attack!");
        }
        Destroy(gameObject);//일단은 낙하하는 폭탄은 어떤 물체에 닿을 경우 알아서 사라지게 한다.
    }
}