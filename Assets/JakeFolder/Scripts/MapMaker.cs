using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{

    [SerializeField]
    private MapCube m_mapCube;

    private void Start()
    {
        MapMake();
    }
    public void MapMake()
    {

        for (int i = 0; i < 50; i++)
        {

            for (int j = 0; j < 50; j++)
            {
                MapCube mapCube = Instantiate(m_mapCube, transform);
                mapCube.transform.localPosition = new Vector3(i, 0f, j);
            }
        }
    }
    
}
