using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Structure
{
    public GameObject Spike;
    public Entity AttackedPlayer;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AttackedPlayer = collision.gameObject.GetComponent<Entity>();
            AttackedPlayer.hp -= 1;
            Debug.Log("Spike : Attack!");
        }
    }

}