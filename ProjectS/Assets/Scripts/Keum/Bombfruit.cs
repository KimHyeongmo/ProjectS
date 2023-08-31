using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombfruit : Enemy
{
    public GameObject BombTrap;
    public Entity AttackedPlayer;


    void FixedUpdate()
    {
        transform.Translate(0, -0.1f, 0); //������ �ӵ��� �����Ѵ�.
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") //�÷��̾ Player ������Ʈ�� ����Ѵٰ� ����
        {
            AttackedPlayer = collision.gameObject.GetComponent<Entity>();
            AttackedPlayer.hp -= 1;
            Debug.Log("Bomb : Attack!");
        }
        Destroy(gameObject);//�ϴ��� �����ϴ� ��ź�� � ��ü�� ���� ��� �˾Ƽ� ������� �Ѵ�.
    }
}