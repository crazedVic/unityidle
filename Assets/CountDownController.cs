using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownController : MonoBehaviour
{

    // variables??
    private static float countDown = 0;
    //public ManualClickButtonController clickBtnObj;


    // Start is called before the first frame update
    void Start()
    {
        //clickBtnObj = GetComponentInParent<ManualClickButtonController>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if startCountDown is initialized to be true, if so then
        // 1.   Start countdown (i.e., update GUI)
        // 2.   When countdown is finished then set startCountdown in   ManualClickButtonController class to be false
    }


    // getters and setters

    public void setCountDown(float newVal)
    {
        countDown = newVal;
    }

    public float getCountDown()
    {
        return countDown;
    }
}
