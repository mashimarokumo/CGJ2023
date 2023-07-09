using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    private Transform tran;

    void Start()
    {
        tran = GameObject.Find("Character").transform;
    }

    
    void Update()
    {
        gameObject.transform.position = tran.position;
    }
}
