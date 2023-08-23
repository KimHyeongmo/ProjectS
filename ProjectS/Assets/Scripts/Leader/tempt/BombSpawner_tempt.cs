using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner_tempt : MonoBehaviour
{

    public Queue<GameObject> Bomb_Pool;

    public float spawntime = 5f;

    public float currenttime = 0;


    // Update is called once per frame
    void Update()
    {
        if(currenttime>spawntime)
        {
            if(Bomb_Pool.Count > 0)
            {
                GameObject Tempt;
                Tempt = Bomb_Pool.Dequeue();
                if (Tempt.activeSelf == false)
                {
                    Tempt.transform.position = transform.position;
                    Tempt.SetActive(true);
                }
                Bomb_Pool.Enqueue(Tempt);
            }

            currenttime = 0;
        }
        currenttime += Time.deltaTime;
    }
}
