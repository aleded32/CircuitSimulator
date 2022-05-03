using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class componentValue : MonoBehaviour
{

    public float Amp;
    public float voltage;

    [HideInInspector]
    public int ohm;

    [Range(0.0f, 1.0f)]
    public float lightInput = 0;

    float maxLight = 1;
    float minLight = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ldrActive();
    }

    void ldrActive()
    {
        if(gameObject.tag == "ldr")
        {
            if (lightInput < 0.1)
                lightInput = minLight;
            else if (lightInput > 1)
                lightInput = maxLight;

            if (lightInput <= 0.1)
                    ohm = 50;
            else
                ohm = (int)(lightInput * 440.0f);






        }
    }
}
