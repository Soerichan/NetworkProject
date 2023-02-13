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
    }


    private void Update()
    {
        if (winPosition.bluewin)
        {
            animator.SetBool("BlueWin", true);
            animator.SetBool("YellowLose", true);
            animator.SetBool("BlueLose", false);
            animator.SetBool("YellowWin", false);

        }
        else
        {
            animator.SetBool("BlueWin", false);
            animator.SetBool("YellowLose", false);

            animator.SetBool("BlueLose", true);
            animator.SetBool("YellowWin", true);

        }
    }       


}
