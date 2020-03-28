﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombCounter : MonoBehaviour
{
    public int BombLeft;
    public string BombAmount;

    public Text BombCount;
    public Text BombX;
    public Image Bombimg;
    public GameObject BombAnimation;
    public Animator BombUsage;
    public Animator BombXAnimation;

    // Start is called before the first frame update
    void Start()
    {
        BombLeft = 5;
        BombAmount = "x";
        BombCount.text = BombLeft.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (BombLeft>0)
            {
                BombLeft--;
                BombCount.text = BombLeft.ToString();
                BombUsageAnimtionens();
            }
            else if (BombLeft <= 0)
            {
                BombLeft = 0;
                BombAmount = "x";
                BombCount.text = BombLeft.ToString();
                BombZeroAnimationens();
                BombIsZero();
            }

        }
           
        


           /* if(BombLeft == 0 && Input.GetKeyDown(KeyCode.E))
        {
            BombLeft = 0;
            BombAmount = "x";
            BombCount.text = BombLeft.ToString();
            BombZeroAnimationens();
            
        BombCount.text = BombLeft.ToString();
        }
        */
   }

    public void BombUsageAnimtionens()
    {
        BombUsage.Play("BombUsage");
    }

    public void BombZeroAnimationens()
    {
        BombUsage.Play("BombLeftZero");
    }

    public void BombIsZero()
    {
        BombXAnimation.Play("XOnZero");
    }
}

