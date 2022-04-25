using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CM;

namespace spawner
{
    public class objectSpawner : MonoBehaviour
    {

        public GameObject[] components;
        public List<GameObject> componentInWorld;
        public List<GameObject> wiresInWorld;
        CircuitManager cm;
        public switchFunc sw;
        public Dropdown dd;

        // Start is called before the first frame update
        void Start()
        {
            componentInWorld = new List<GameObject>();
            wiresInWorld = new List<GameObject>();
        }

        // Update is called once per frame
        void Update()
        {
            cm = FindObjectOfType<CircuitManager>();
            
        }


        public void addBatteryToWorld()
        {
            if(!componentInWorld.Exists(x => x.tag == "battery"))
                componentInWorld.Add(Instantiate(components[0], transform.position, Quaternion.identity));
        }

        public void addComponents()
        {
            switch (dd.value)
            {
                case 0:
                    componentInWorld.Add(Instantiate(components[6], transform.position, Quaternion.identity));
                    break;
                case 1:
                    componentInWorld.Add(Instantiate(components[1], transform.position, Quaternion.identity));
                    break;
                case 2:
                    componentInWorld.Add(Instantiate(components[2], transform.position, Quaternion.identity));
                    break;
                case 3:
                    componentInWorld.Add(Instantiate(components[3], transform.position, Quaternion.identity));
                    break;
                case 4:
                    componentInWorld.Add(Instantiate(components[5], transform.position, Quaternion.identity));
                    break;

            };
        }

      

        public void clear()
        {

            foreach (GameObject c in componentInWorld)
                Destroy(c);

            foreach (GameObject w in wiresInWorld)
                Destroy(w);

            cm.componentsInCircuit.Clear();
            cm.connectionTable.Clear();
            componentInWorld.Clear();
            wiresInWorld.Clear();
            cm.CircuitConnected = false;
            sw.switchState = true;
            
           
        }
    }
}
