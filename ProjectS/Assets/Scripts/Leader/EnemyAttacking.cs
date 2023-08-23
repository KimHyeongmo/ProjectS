using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    Enemy enemy;

    private void Start()
    {
        enemy = this.gameObject.GetComponent<Enemy>();
    }


    private void OnTriggerEnter(Collider user)
    {
        Entity target = user.gameObject.GetComponent<Entity>();

        if(target != null)
        {
            target.hp -= enemy.damage;
            Debug.Log("Attack!!");
        }

    }
}
