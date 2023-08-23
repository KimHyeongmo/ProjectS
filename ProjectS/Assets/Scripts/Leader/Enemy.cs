using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    int previous_hp;

    public int speed;

    private void Start()
    {
        previous_hp = this.hp;
    }

    private void Update()
    {


        if(previous_hp > this.hp)
        {
            Debug.Log("Ouch!!");
        }

        if(this.hp<=0)
        {
            Debug.Log("Dead");
        }

        previous_hp = this.hp;

    }
}

