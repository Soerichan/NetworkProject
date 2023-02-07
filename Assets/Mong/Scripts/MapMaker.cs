using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    public GameObject m_cube;

    // Start is called before the first frame update
    void Start()
    {
        MapMake();    
    }

    public void MapMake()
    {
        for (int i = 0; i < 30; i++)
        { 
            for (int j= 0; j < 30; j++) 
            { 
                
                GameObject cube = Instantiate(m_cube, transform);
                cube.transform.localPosition = new Vector3(i, -1, j);
            }
        }
    
    }
}
