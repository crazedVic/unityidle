using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour {

    public Button Button1;
    public TextMeshProUGUI text2; // TextCounter ....... starts at 1
    public TextMeshProUGUI textCost;
    public TextMeshProUGUI textProduction;
    public float timer = 0.0f;
    public float counter = 1.0f;
    public float num_increase = 1.0f;
    public float cost = 1.0f;


    // Start is called before the first frame update, use for initilization 
    void Start()
    {
        Button btn = Button1.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
        
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log(Time.deltaTime); //approx 0.003 so 333fps???

        // update counter based on current producivity, i.e., higher productivity means more update of text2 per sec up to a limit of every 100ms

        if (1.0/num_increase <= 0.1)
        {
            // limit reached now update every 100ms or 0.1s
            if (timer > 0.1)
            {
                // update counter 
                text2.text = ((int)(counter + (0.1 * num_increase))).ToString();
                counter = (int)(counter + (0.1*num_increase));
                timer = 0;
            }

        }else
        {
            // limit not reached update according to number for productivity 
            if (timer> 1.0/num_increase)
            {
                // update counter 
                // update counter 
                text2.text = ((int)(counter + 1.0)).ToString(); // losing number after decima place 
                counter = (int)(counter + 1.0);
                timer = 0;
            }
        } 
    }

    public void TaskOnClick() {
        // change rate at which textfield increments if enough credits are available 
        // also change textfield overlayed on button to show the cost of next increment change 
        //Debug.Log ("You have clicked the button!");
        //text2.text = "Some new line of text.";

        // only perfom button action if we have enough "points"
        if (counter >= cost)
        {
            // increase producity, subtract cost from "points" and increase next cost amount 
            num_increase = num_increase * 2;
            counter -= cost;
            cost = cost * 2.32f;

            // update GUI, round values  
            text2.text = (( (int) counter).ToString());
            textCost.text = "(COST: " + ((int)cost).ToString() + " NUMBER)";
            textProduction.text = ( (int) num_increase).ToString() + " number per second";
        }
    }
}
