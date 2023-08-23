using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_tempt : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
