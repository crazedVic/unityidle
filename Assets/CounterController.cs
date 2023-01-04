using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CounterController : MonoBehaviour
{
    private float counter = 1.0f;
    public TextMeshProUGUI text2; // TextCounter ....... starts at 1

    // singleton???



    // Start is called before the first frame update
    void Start()
    {
        // empty??
    }

    // Update is called once per frame
    void Update()
    {
        // emppty?
    }

    // updateCounterTextBox
    public void updateCounterUI()
    { 
        text2.text = (((int)counter).ToString()); // update GUI, round values 
    }

    // getters and setters
    public void setCounter(float newVal)
    {
        // critical section????????
        counter = newVal;
    }

    public float getCounter()
    {
        return counter;
    }
}
