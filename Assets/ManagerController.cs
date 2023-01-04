using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerController : MonoBehaviour
{

    private bool managerPurchased = false;

    // Start is called before the first frame update
    void Start()
    {
        // empty?
    }

    // Update is called once per frame
    void Update()
    {
        if (managerPurchased)
        {
            // increase counter based on production rate, don't forget to re-read production rate since it increases 
            // use delta time? to increase counter at correct rate
        }    
    }


    // getters and setters
    public void setManagerPurchased(bool newVal)
    {
        managerPurchased = newVal;
    }

    public bool getManagerPurchased()
    {
        return managerPurchased;
    }
    
}
