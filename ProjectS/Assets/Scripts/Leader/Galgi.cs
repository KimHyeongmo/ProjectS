using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galgi : MonoBehaviour
{
    
    Enemy Enemy_inform;
    Rigidbody Enemy_rigid;

    bool target_on = false;
    float original_speed;

    [Range(0,Mathf.Infinity)]
    public float target_direction;

    // Start is called before the first frame update
    void Start()
    {
        Enemy_inform = GetComponent<Enemy>();
        Enemy_rigid = GetComponent<Rigidbody>();
        original_speed = Enemy_inform.speed;

        StartCoroutine(Rush_managing());
    }

    // Update is called once per frame
    void Update()
    {
        Rush();
        
    }

    void Rush()
    {
        float direction = transform.localEulerAngles.y != 0 ? -1 : 1;

        Debug.DrawRay(transform.position, Vector3.right * target_direction * direction, Color.red);
        RaycastHit rayHit;
        if(Physics.Raycast(transform.position, Vector3.right * direction , out rayHit, target_direction))
        {
            if (rayHit.transform.CompareTag("Player"))
            {
                target_on = true;
            }
            else
                target_on = false;
        }
        else
        {
            target_on = false;
        }
    }

    IEnumerator Rush_managing()
    {

        float speed_abs;

        do
        {
            yield return null;
            speed_abs = Mathf.Abs(Enemy_inform.speed);
            if (target_on)
            {

                if (speed_abs < 10)
                {
                    Enemy_inform.speed *= 1.125f;
                }

            }
            else
            {
                if (speed_abs > original_speed)
                {
                    Enemy_inform.speed /= 1.125f;
                }
            }

            if(speed_abs < original_speed)
            {
                Enemy_inform.speed = Enemy_inform.speed > 0 ? original_speed : original_speed * -1;
            }
            else if(speed_abs > 10)
            {
                Enemy_inform.speed = Enemy_inform.speed > 0 ? 10 : -10;
            }
        }
        while (true);
    }

}
