using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public Button ManualClick;
    public Button BuyManager;
    public bool countDown = false;
    public float counter = 0.0f;
    public TextMeshProUGUI textCounter;
    public TextMeshProUGUI textCountDown; 
    public bool managerPurchased = false;
    public IEnumerator coroutineCountingDown; 

    public Button ProductivityClick;
    public TextMeshProUGUI textCost; // text box for cost of next producitvity increase 
    public TextMeshProUGUI textProduction; // text box for current productivity 
    public float timer = 0.0f;
    public float num_increase = 1.0f;
    public float cost = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // empty?
    }

    // Update is called once per frame
    void Update()
    {
        if (managerPurchased && countDown == false)
        {
            // update count automatically. manual click button should automatically be disabled. 

            
            // start count down via coroutine
            coroutineCountingDown = countingDown(num_increase);
            countDown = true;
            StartCoroutine(coroutineCountingDown);

        }
    }





    /*
     * change rate at which textfield increments if enough credits are available 
     *  also change textfield overlayed on button to show the cost of next increment change
     */
    public void ProductivityButtonClicked()
    {
        // only perfom button action if we have enough "points"
        if (counter >= cost)
        {
            // increase producity, subtract cost from "points" and increase next cost amount 
            num_increase = num_increase * 2;
            counter -= cost;
            cost = cost * 2.32f;

            // update GUI, round values  
            textCounter.text = (((int)counter).ToString());
            textCost.text = "(COST: " + ((int)cost).ToString() + " NUMBER)";
            textProduction.text = ((int)num_increase).ToString() + " number per second";
        }

    }

    public void ManualButtonClicked()
    {
        if (managerPurchased == false && countDown == false)
        {
            // start count down via coroutine
            coroutineCountingDown = countingDown(num_increase);
            countDown = true;
            StartCoroutine(coroutineCountingDown);
        }

        // else do nothing. If manager is purchased they will increase count themselves
    }

    public void BuyManagerClicked()
    {
        if(managerPurchased == false && counter>=1000.0f)
        {
            managerPurchased = true;
            counter -= 1000.0f;
            updateCounterUI();
        }
    }

    public IEnumerator countingDown(float count_increase)
    {
        // count down for 1 seconds 
        float countDownLength = 1.0f; // CHANGE HERE TO INCREASE/ DECREASE COUNTDOWN
        float numIterations = 0.0f;

        // loop through count down using Wait 
        while(countDownLength - numIterations > 0)
        {
            updateCountDownUI(countDownLength - numIterations);
            numIterations += 1.0f;
            yield return new WaitForSeconds(1.0f);
        }

        updateCountDownUI(0f);
        countDown = false;
        StopCoroutine(coroutineCountingDown);
        counter += count_increase;
        updateCounterUI();
    }

    /*
     * Method takes input in seconds. Displays it in textCountDown in format    min:sec
     */
    public void updateCountDownUI(float seconds)
    {
        float minutes = (float) (seconds / 60);
        seconds = seconds % 60;

        if(minutes<=9.0f && seconds <=9.0f)
        {
            textCountDown.text = ((0).ToString() + ((int)minutes).ToString() + ":" + (0).ToString() + ((int)seconds).ToString());

        } else if(minutes>9.0f && seconds <= 9.0f)
        {
            textCountDown.text = (((int)minutes).ToString() + ":" + (0).ToString() + ((int)seconds).ToString());

        } else if(minutes<=9.0f && seconds > 9.0f)
        {
            textCountDown.text = ((0).ToString() + ((int)minutes).ToString() + ":" + ((int)seconds).ToString());

        } else if (minutes >9.0f && seconds > 9.0f)
        {
            textCountDown.text = (((int)minutes).ToString() + ":" + ((int)seconds).ToString());
        }

    }

    // updateCounterTextBox
    public void updateCounterUI()
    {
            textCounter.text = (((int)counter).ToString("#,#")); // update GUI, round values 
    }



}
