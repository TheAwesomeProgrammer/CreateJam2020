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

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Health lost");
            Health--;

            switch (Health)
            {
                case 5:
                    HearhtRight1.active = false;
                    break;

                case 4:
                    HearhtLeft1.active = false;
                    break;

            }
        }
        
    }
}
