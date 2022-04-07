using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CM;
using UnityEngine.Experimental.Rendering.Universal;

public class bulbActivation : MonoBehaviour
{
    CircuitManager cm;
    public GameObject light2D;
    public GameObject lightSprite;
    // Start is called before the first frame update
    void Start()
    {
        light2D.GetComponent<Light2D>().intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cm = FindObjectOfType<CircuitManager>();

        if (cm)
        {
            
           
            if (isLightOn())
            {
                light2D.GetComponent<Light2D>().intensity = cm.powerCalculation();
                lightSprite.SetActive(true);
            }
            else
            {
                light2D.GetComponent<Light2D>().intensity = 0;
                lightSprite.SetActive(false);
            }
        }
        
            
        

    }

    bool isLightOn()
    {
        if (cm.CircuitConnected)
        {
            
            return true;
        }
        else
            return false;
    }
}
