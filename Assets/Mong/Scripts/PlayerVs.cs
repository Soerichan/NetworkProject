using Mong;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerVs : MonoBehaviour
{
    public Animator animator;
    public WinPosition winPosition;
    private void Start()
    {
        animator = GetComponent<Animator>();
        winPosition = FindObjectOfType<WinPosition>();

        if (winPosition.bluewin)
        {
            animator.SetBool("BlueWin", true);
            


        }
        else
        {


            animator.SetBool("BlueWin", false);
            

        }
    }


    


}
