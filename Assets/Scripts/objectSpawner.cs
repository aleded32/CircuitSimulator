using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        public void addSwitchToWorld()
        {
            componentInWorld.Add(Instantiate(components[1], transform.position, Quaternion.identity));
        }

        public void addBulbToWorld()
        {
            componentInWorld.Add(Instantiate(components[2], transform.position, Quaternion.identity));
        }

        public void addResistorToWorld()
        {
            componentInWorld.Add(Instantiate(components[3], transform.position, Quaternion.identity));
        }

        public void addSpeakerToWorld()
        {
            componentInWorld.Add(Instantiate(components[5], transform.position, Quaternion.identity));
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
