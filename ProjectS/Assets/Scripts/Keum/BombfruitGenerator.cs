using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombfruitGenerator : MonoBehaviour
{
    public GameObject BombfruitPrefab;
    public float span;
    float delta = 0;
    void Start()
    {
        
    }

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            Instantiate(BombfruitPrefab, transform);
        }
    }
}
