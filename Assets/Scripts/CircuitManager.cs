using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CM
{

    public class CircuitManager : MonoBehaviour
    {

        public List<char> connectionTable;
        public List<GameObject> componentsInCircuit;
        List<GameObject> resistorsInCircuit;

        spawner.objectSpawner os;

        switchFunc sw;

        public bool CircuitConnected = false;


        // Start is called before the first frame update
        void Start()
        {
            connectionTable = new List<char>();
            os = FindObjectOfType<spawner.objectSpawner>();
            componentsInCircuit = new List<GameObject>();
            resistorsInCircuit = new List<GameObject>();
            
            
        }

        private void Update()
        {
            if(componentsInCircuit.Count != 0)
                checkConnection();

            

        }

        public void addConnection(Transform parent, Transform parent2)
        {
            if (!componentsInCircuit.Exists(x => x.tag == parent.gameObject.tag))
            { 

                connectionTable.Add('c');
                componentsInCircuit.Add(parent.gameObject);
            }
            else
            {
                connectionTable.Add('c');
                componentsInCircuit.Add(parent2.gameObject);
            }

        }

        public void removeConnection(Transform parent)
        {
            connectionTable.Remove('c');
            componentsInCircuit.Remove(parent.gameObject);
        }

        void checkConnection()
        {
            sw = FindObjectOfType<switchFunc>();

            if (componentsInCircuit.Count == connectionTable.Count && componentsInCircuit.Exists(x => x.tag == "battery") && sw.switchState)
            {
                CircuitConnected = true;
            }
            else if (componentsInCircuit.Count != connectionTable.Count || !sw.switchState)
            {
                CircuitConnected = false;
            }
        }


        public float powerCalculation()
        {
            GameObject battery = componentsInCircuit.Find(x=> x.tag == "battery");
            resistorsInCircuit = componentsInCircuit.FindAll(x => x.tag == "resistor" || x.tag == "ldr");

            float voltage;
            int ohm = 0;

            if (battery)
                voltage = battery.GetComponent<componentValue>().voltage;
            else
                voltage = 0;

            Debug.Log("voltage " + voltage);


            if (resistorsInCircuit.Count <= 0)
                ohm = 50;
            else
            {
                for (int i = 0; i < resistorsInCircuit.Count; i++)
                {
                    ohm += resistorsInCircuit[i].GetComponent<componentValue>().ohm;
                }
            }

            float amp = voltage / ohm;

            Debug.Log(ohm);
            

            float watt = (amp * voltage);

            Debug.Log(watt);

            return watt;

        }

        
    }
}
