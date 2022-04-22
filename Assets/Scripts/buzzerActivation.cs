using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CM;

public class buzzerActivation : MonoBehaviour
{
    CircuitManager cm;
    // Start is called before the first frame update
    void Start()
    {
        cm = FindObjectOfType<CircuitManager>();
    }

    // Update is called once per frame
    void Update()
    {
        buzzerFunction();
    }

    void buzzerFunction()
    {
        if (cm)
        {


            if (isBuzzerOn() && !gameObject.GetComponent<AudioSource>().isPlaying)
            {

                gameObject.GetComponent<AudioSource>().volume = cm.powerCalculation();
                gameObject.GetComponent<AudioSource>().Play();

            }
            else if(!isBuzzerOn() && gameObject.GetComponent<AudioSource>().isPlaying)
            {
                gameObject.GetComponent<AudioSource>().Stop();
            }
            
        }
    }

    bool isBuzzerOn()
    {
        if (cm.CircuitConnected)
        {

            return true;
        }
        else
            return false;
    }
}
