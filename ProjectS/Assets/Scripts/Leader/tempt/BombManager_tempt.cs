using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager_tempt : MonoBehaviour
{

    public GameObject[] BombSpawnerEntity;

    Queue<GameObject> BombPool;
    public GameObject Bomb;


    [SerializeField]
    int BombNumber;

    // Start is called before the first frame update
    void Start()
    {
        BombPool = new Queue<GameObject>();

        GameObject tempt;

        for (int i = 0; i < BombNumber; i++)
        {
            tempt = Instantiate<GameObject>(Bomb);
            tempt.SetActive(false);
            BombPool.Enqueue(tempt);

        }

        foreach (GameObject Spawner in BombSpawnerEntity)
        {
            Spawner.GetComponent<BombSpawner_tempt>().Bomb_Pool = BombPool;
        }
    }
    
}
