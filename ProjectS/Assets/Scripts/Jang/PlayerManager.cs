using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public Collider[] AttackHitBoxes;
    
    void Start()
    {
       
    }

    private float curTime;
    public float coolTime = 0.5f;
    public Transform pos;
    public Vector3 BoxSize;


    // Update is called once per frame
    void Update()
    {
      

        if (curTime <= 0)
        {   
            //공격
            //'Z'버튼을 공격버튼으로
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Entity target;
                Collider[] cols = Physics.OverlapBox(pos.position, BoxSize);

                foreach (Collider c in cols)
                {
                    target = c.gameObject.GetComponent<Entity>();
                    if (target != null)
                    {
                        target.hp -= 1;
                        Debug.Log(c.name);
                        Debug.Log("damage");
                    }
                }
                curTime = coolTime;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position,BoxSize*2);  
    }
}
