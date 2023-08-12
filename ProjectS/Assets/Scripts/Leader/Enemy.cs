using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    private void Start()
    {
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("test");
    }
}

