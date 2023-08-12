using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{

    public GameObject body;

    Rigidbody enemy_rigidbody;

    Enemy enemy;

    private void Start()
    {
        enemy_rigidbody = body.GetComponent<Rigidbody>();
        enemy = body.GetComponent<Enemy>();
    }

    private void FixedUpdate()
    {
        enemy_rigidbody.velocity = new Vector3(enemy.speed, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Entity>()==null)
        {
            Debug.Log("hi");
            enemy.speed *= -1;
        }
    }
}
