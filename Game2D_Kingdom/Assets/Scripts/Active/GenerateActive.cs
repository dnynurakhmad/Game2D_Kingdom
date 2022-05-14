using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GenerateActive : MonoBehaviour
{
    public GameObject[] Creeps;
    public float DelayTime;
        
    public Action<Transform, string> GenerateAction { get; set; }

    public bool IsReadySpawn
    {
        set
        {
            if (value)
            {
                if (spawnTime > 0)
                {
                    spawnTime -= Time.deltaTime;
                }

                if (spawnTime < 0)
                {
                    spawnTime = 1f;
                    GenerateAction?.Invoke(transform, transform.tag);
                }
            }
        }
    }

    public int Stock
    {
        get
        {
            return stock;
        }
        set
        {
            stock = value;
            Creeps = new GameObject[stock];
        }
    }

    private int stock;
    private float castTime;
    private float spawnTime;

    private void Start()
    {
        castTime = DelayTime;
    }

    private void Update()
    {
        if (castTime > 0)
        {
            castTime -= Time.deltaTime;
        }

        if (castTime < 0)
        {
            castTime = 0;
            spawnTime = 1f;
        }

        IsReadySpawn = transform.childCount < Stock;
    }
}
