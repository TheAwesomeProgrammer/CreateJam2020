using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIScript : MonoBehaviour
{
    public int Health;

    public GameObject HearhtLeft1;
    public GameObject HearhtRight1;
    public GameObject HearhtLeft2;
    public GameObject HearhtRight2;
    public GameObject HearhtLeft3;
    public GameObject HearhtRight3;

    public Image Hearth;
    // Start is called before the first frame update
    void Start()
    {
        Health = 6; 
    }



    public void SetHealth(int health)
    {
        Health = health;
        switch (Health)
        {
            case 5:
                HearhtRight3.active = false;
                break;

            case 4:
                HearhtLeft3.active = false;
                break;
            case 3:
                HearhtRight2.active = false;
                break;

            case 2:
                HearhtLeft2.active = false;
                break;
                
            case 1:
                HearhtRight1.active = false;
                break;
            case 0:
                HearhtLeft1.active = false;
                break;
        }
    }
}
