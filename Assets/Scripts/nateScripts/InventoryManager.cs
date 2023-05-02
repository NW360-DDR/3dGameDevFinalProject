using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // This right here is our Inventory. 
    public Dictionary<string, int> inven = new Dictionary<string, int>();

    private void Start()
    {
        // Start up our inventory with some stuff.
        inven.Add("Strawberry", 3);
        inven.Add("Banana", 2);
        inven.Add("Pork Chop", 1);
        inven.Add("FourthFood", 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food")) 
        { 
            string thingName = other.name;
            if (inven.ContainsKey(thingName))
            {
                inven[thingName]++;
            }
            else
            {
                inven.Add(thingName, 1);
            }
        }
    }

    bool TryGrab(string request)
    {
        if (inven.ContainsKey(request) && inven[request] > 0)
        {
            return true;
        }
        else 
        { 
            return false; 
        }

    }

    string GrabItem(string request)
    {
        if (inven.ContainsKey(request) && inven[request] > 0)
        {
            inven[request]--;
            if (inven[request] <= 0)
            {
                inven.Remove(request);
            }
            return request;
        }
        else // This should never happen, but just in case... Among Sus
        {
            Debug.Log("Error: Item not in inventory!");
            return "";
        }
        
    }

    void UnGrabItem(string thing)
    {
        inven[thing]++;
    }
}

