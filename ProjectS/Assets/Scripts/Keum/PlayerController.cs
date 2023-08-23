using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    void Start()
    {
        Entity Player = GetComponent<Entity>();
    }

   
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)) 
        {
            transform.Translate(-0.01f, 0, 0);
        }
        if(Input.GetKey(KeyCode.RightArrow)) 
        {
            transform.Translate(0.01f, 0, 0);
        }
    }
}
