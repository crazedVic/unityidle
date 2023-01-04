using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using ManagerController;

public class ManualClickButtonController : MonoBehaviour
{
    public Button ManualClick;
    private bool startCountDown = false;
    public CounterController counterObj;
    public ButtonController btnObj;

    // Start is called before the first frame update
    void Start()
    {
        ManualClick = GetComponent<Button>();
        ManualClick.onClick.AddListener(() => TaskOnClicked());
    }

    void Destroy()
    {
        ManualClick.onClick.RemoveListener(TaskOnClicked); // destroy listener to prevent memory leak if ButtonController obj is destroyed, an active listener will create mem leak?
    }

    // Update is called once per frame
    void Update()
    {
        // empty????
    }

    public void TaskOnClicked()
    {
        // Manual click button is pressed
        // 1.   Check that a manager is not purchased, if so then disable button function 
        // 2.   Update counter var in Counter class, critical section??
        // 3.   initialize startCountDown to true 

        //if(!manager.getManagerPurchased() && startCountDown==false)
        /*if(ManagerController.getManagerPurchased() == false)
        {
            // button works because no manager is purchased AND we are not already counting down
            // if that is the case then we can 
            // 1. Increment counter
            // 2. start count down
            //counterObj.setCounter(counterObj.getCounter() + btnObj.num_increase);
            startCountDown = true;

            //CounterController.updateCounterUI();
            //counterObj.updateCounterUI();
        }*/
        Debug.Log(counterObj.getCounter());
        counterObj.setCounter(counterObj.getCounter() + btnObj.num_increase);
        counterObj.updateCounterUI();


    }



    // getters and setters
    public void setStartCountDown(bool newVal)
    {
        startCountDown = newVal;
    }

    public bool getStartCountDown()
    {
        return startCountDown;
    }

}
