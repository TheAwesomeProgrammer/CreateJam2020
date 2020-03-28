using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAnimation : MonoBehaviour
{

    public Animator OwlBody;
    public Animator WizardHead;
    public Animator OwlHead;
    public Animator Fire;
    public Animator WizardArm;
    public bool fireMode = false;

    // Start is called before the first frame update
    void Start()
    {
        fireMode = false;

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.O))
        {
            WizardUseWandAnimation();
        }
        //TOGGLE SMOKE/FIRE 
        if (Input.GetKeyDown(KeyCode.I))
        {
            fireMode = !fireMode;
            if (fireMode == true)
            {
                WizardUseWandAnimation();
                TurnOffFireAnimation();
            }
            else if(fireMode == false)
            {
                WizardUseWandAnimation();
                PlayFireAnimation();
            }
        }
        

    }

    public void WizardUseWandAnimation()
    {
        WizardArm.Play("WizardUseWand");
    }
    public void PlayFireAnimation()
    {
        Fire.Play("FireAnimation");
    }

    public void TurnOffFireAnimation()
    {
        Fire.Play("NoFire");
    }
}
