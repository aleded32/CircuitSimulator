using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CM;

public class switchFunc : MonoBehaviour
{

    CM.CircuitManager cm;
    public bool switchState;
    bool isPressed;
    public Sprite[] switchSprites;

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
        switchState = true;
    }

    // Update is called once per frame
    void Update()
    {
        cm = FindObjectOfType<CircuitManager>();

        if (cm)
        { 
            pressSwitch();
           
        }
    }

    void pressSwitch()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (cm.componentsInCircuit.Exists(x => x.tag == "battery") && cm.componentsInCircuit.Exists(x => x.tag == "switch"))
        {
            if (hit.collider && Input.GetMouseButtonDown(0) && !isPressed)
            {
                if (hit.collider.transform.parent.tag == "switch")
                {
                    switchState = (switchState == true) ? false : true;

                    if (switchState)
                    {
                       hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = switchSprites[0];
                    }
                    else
                        hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = switchSprites[1];

                    isPressed = true;
                }
            }
            else
                isPressed = false;
        }
    }

    

}
