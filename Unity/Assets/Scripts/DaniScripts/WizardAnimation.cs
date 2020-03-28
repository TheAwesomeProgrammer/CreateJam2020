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
    private bool fireMode = false;

    // Start is called before the first frame update
    void Start()
    {
        fireMode = false;

    }

    public void SetFireMode(bool fireMode)
    {
        if (fireMode)
        {
            WizardUseWandAnimation();
            PlayFireAnimation();
        }
        else
        {
            WizardUseWandAnimation();
            TurnOffFireAnimation();
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
