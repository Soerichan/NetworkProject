using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolerTest : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;

    public PoolManager poolManager;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            poolManager.Get(prefabs[0]);

        }
        if (Input.GetKey(KeyCode.S))
        {
            poolManager.Get(prefabs[1]);

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            poolManager.Get(prefabs[2]);

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            poolManager.Get(prefabs[3]);

        }
    }
}
