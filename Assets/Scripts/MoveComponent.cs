using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using spawner;
using CM;

public class MoveComponent : MonoBehaviour
{

    public Collider2D selectedTarget;
    float rotZ;
    bool wireCreated = false;
    bool deleteWire = false;
    objectSpawner os;
    CircuitManager cm;
    // Start is called before the first frame update
    private void Start()
    {
        os = FindObjectOfType<objectSpawner>();
    }

    // Update is called once per frame
    void Update()
    {

        cm = FindObjectOfType<CircuitManager>();

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.W))
        {
            selectObject();
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) && selectedTarget)
        {
          
            selectedTarget = null;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            deleteWire = true;
        }


        if (cm)
        {
            if (!Input.GetKey(KeyCode.W))
            {
                if (Input.GetMouseButton(1) && selectedTarget && !cm.componentsInCircuit.Contains(selectedTarget.transform.parent.gameObject))
                {

                    rotateObject();
                }
                else if (Input.GetMouseButton(0) && selectedTarget && !cm.componentsInCircuit.Contains(selectedTarget.transform.parent.gameObject))
                {
                    selectedTarget.transform.parent.position = new Vector3(mousePos().x, mousePos().y, 0);
                }
            }
        }

        WireAdd();

        
    }

    Vector3 mousePos()
    {
        
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void selectObject()
    {
        Collider2D target = Physics2D.OverlapPoint(mousePos());

        if (target) 
        {
            selectedTarget = target;
        }

        
    }

    void rotateObject()
    {
        rotZ += Input.GetAxis("Vertical") * 0.2f * Time.deltaTime;
        selectedTarget.transform.parent.Rotate(0, 0, rotZ);
    }

    void WireAdd()
    {
        GameObject TempWire = null;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (selectedTarget && Input.GetKeyDown(KeyCode.W))
        {
            wireCreated = true;
            deleteWire = false;
        }

        if (wireCreated)
        {
            
            os.wiresInWorld.Add(Instantiate(os.components[4], selectedTarget.transform.position, Quaternion.identity));
            wireCreated = false;
           

        }

        if (selectedTarget)
        {
            
            TempWire = os.wiresInWorld.Find(x => x.transform.position == selectedTarget.transform.position);

            
            
        }

        if (TempWire)
        {
            if (selectedTarget.gameObject.tag == "negative")
            {
                TempWire.GetComponent<LineRenderer>().startColor = Color.black;
                TempWire.GetComponent<LineRenderer>().endColor = Color.black;

            }
            else
            {
                TempWire.GetComponent<LineRenderer>().startColor = Color.red;
                TempWire.GetComponent<LineRenderer>().endColor = Color.red;
            }
            TempWire.GetComponent<LineRenderer>().SetPosition(0, new Vector3(selectedTarget.transform.position.x, selectedTarget.transform.position.y, 0));


            if (cm)
            {
                if (hit.collider && selectedTarget.tag == hit.collider.tag)
                {

                    if (hit.collider != selectedTarget)
                    {
                        TempWire.GetComponent<LineRenderer>().SetPosition(1, new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, 0));


                        cm.addConnection(hit.collider.transform.parent, selectedTarget.transform.parent);

                        selectedTarget = null;
                        TempWire = null;
                    }
                }
                else if (!hit.collider)
                {
                    Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 10, Color.blue);
                    TempWire.GetComponent<LineRenderer>().SetPosition(1, new Vector3(mousePos().x, mousePos().y, 0));
                    if (deleteWire)
                    {


                        cm.removeConnection(selectedTarget.transform.parent);

                        os.wiresInWorld.Remove(TempWire);
                        Destroy(TempWire);

                    }

                }
            }
        }



    }

    
}
