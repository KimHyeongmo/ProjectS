using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{

    public GameObject body;

    public Transform groundcheck;
    public Vector3 BoxSize;

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

        Collider[] cols = Physics.OverlapBox(groundcheck.position, BoxSize);
        if (cols.Length < 1 || cols.Length > 1)
        {
            float direction = transform.localEulerAngles.y != 0 ? 0 : 180;
            transform.rotation = Quaternion.Euler(0, direction, 0);
            //enemy.transform.Rotate(new Vector3(0, direction, 0));
            enemy.speed *= -1;
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(groundcheck.position, BoxSize * 2);
    }
}
