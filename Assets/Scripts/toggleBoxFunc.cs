using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CM;
using UnityEngine.EventSystems;

public class toggleBoxFunc : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    MoveComponent mc;

    [SerializeField]
    CircuitManager cm;

    public GameObject[] toggleboxes;

    [HideInInspector]
    public bool isMouseOver;

    [HideInInspector]
    public bool isToggleActive;

    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mc.selectedTarget != null)
        {
            toggleBoxPosition();
            changeToggleBoxText();
        }

        
        activeToggleBox();

        foreach (GameObject togglebox in toggleboxes)
        {
            if (togglebox.activeInHierarchy)
            {
                isToggleActive = true;
            }
            
        }

    }

    void toggleBoxPosition()
    {
        
        Vector2 component = mc.selectedTarget.transform.position;
        foreach (GameObject toggleBox in toggleboxes)
        {
            toggleBox.transform.position = ScreenToRectTransform(new Vector2(component.x - 4, component.y));
        }
        
    }


    void activeToggleBox()
    {
        
        if (mc.selectedTarget != null)
        {
            string compName = mc.selectedTarget.transform.parent.tag;

            if (compName == "speaker" && compName == "bulb")
            {
                toggleboxes[2].SetActive(true);
                toggleboxes[1].SetActive(false);
                toggleboxes[0].SetActive(false);
            }
            else if (compName == "ldr")
            {
                toggleboxes[1].SetActive(true);
                toggleboxes[0].SetActive(false);
                toggleboxes[2].SetActive(false);
            }
            else if (compName == "resistor")
            {
                toggleboxes[0].SetActive(true);
                toggleboxes[1].SetActive(false);
                toggleboxes[2].SetActive(false);
            }
            else
            {
                toggleboxes[0].SetActive(false);
                toggleboxes[1].SetActive(false);
                toggleboxes[2].SetActive(false);
            }
           
        }
        else if (mc.selectedTarget == null &&  !isMouseOver)
        {
            toggleboxes[0].SetActive(false);
            toggleboxes[1].SetActive(false);
            toggleboxes[2].SetActive(false);
            
        }

        
    }

    
    

    void changeToggleBoxText()
    {

        float voltage = cm.voltage;
        float amp = cm.amp;
      
        string compName = mc.selectedTarget.transform.parent.tag;


            

        if (compName == "speaker" && compName == "bulb")
        {
            toggleboxes[2].transform.GetChild(0).GetComponent<Text>().text = compName;
            toggleboxes[2].transform.GetChild(1).GetComponent<Text>().text = "Voltage:  " + voltage;
            toggleboxes[2].transform.GetChild(3).GetComponent<Text>().text = "Amps  " + amp;

        }
        else if (compName == "ldr")
        {
            toggleboxes[1].transform.GetChild(0).GetComponent<Text>().text = compName;
        }
        else if(compName == "resistor")
        {
            toggleboxes[0].transform.GetChild(0).GetComponent<Text>().text = compName;
            int ohm;
            Slider sl = toggleboxes[0].transform.GetChild(4).GetComponent<Slider>();

            ohm = (int)sl.value;
            toggleboxes[0].transform.GetChild(5).GetComponent<Text>().text = sl.value.ToString();


            mc.selectedTarget.transform.parent.GetComponent<componentValue>().ohm = ohm;
        }

        

    }

    Vector3 ScreenToRectTransform(Vector3 selectedTargetPos)
    {
        Vector3 screenpos = Camera.main.WorldToScreenPoint(selectedTargetPos);
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenpos, canvas.worldCamera, out movePos);
        return canvas.transform.TransformPoint(movePos);

    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
       
    }
}
