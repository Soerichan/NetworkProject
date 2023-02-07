using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundColorChange : MonoBehaviour
{
    public new Renderer renderer;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    
}
