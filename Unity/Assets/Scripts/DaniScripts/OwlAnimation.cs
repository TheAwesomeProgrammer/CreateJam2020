using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlAnimation : MonoBehaviour
{

    public Animator CannonAnimator;
    public Animator CannonWheelAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            CannonShootAnimation();
        }
    }

    public void CannonShootAnimation()
    {
        CannonAnimator.Play("Test");
        CannonWheelAnimator.Play("Wheelfeedback");
    }

    public void CannonIdleAnimation()
    {

    }
}
