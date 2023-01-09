using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public UnityEvent<float> onProgress;
    public UnityEvent OnCompleted;
    public Coroutine AnimationCoroutine;
    public IEnumerator coroutineCountingDown;
    public Image ProgressImage;
    public Button ProductivityClick;
    public Button ManualClick;
    public Button BuyManager;
    public Button SaveButton;
    public Button LoadButton;
    public Button ResetButton;
    public TextMeshProUGUI textCounter;
    public TextMeshProUGUI textCountDown;
    public TextMeshProUGUI textCost; // text box for cost of next producitvity increase 
    public TextMeshProUGUI managerButtonText;
    public TextMeshProUGUI textProduction; // text box for current productivity 

    public float timer = 0.0f;
    public float num_increase = 1.0f;
    public float cost = 1.0f;
    public float DefaultSpeed = 1f;
    public float autoSaveTimer = 0.0f;
    public float counter = 0.0f;
    public bool countDown = false;
    public bool managerPurchased = false;

    // Start is called before the first frame update
    void Start()
    {
        // ON START check for saved data, if it exists then cost=savedcost or whatever, LoadGame should load data if it exists
        LoadGame();

        // update GUI to reflect load game values
        updateCounterUI();
    }

    // Update is called once per frame
    void Update()
    {
        autoSaveTimer += Time.deltaTime;

        if (autoSaveTimer > 15)
        {
            SaveGame();
            autoSaveTimer = 0f;
        }

        // if manager is purchased then auto click
        if (managerPurchased && countDown == false)
        {
            // start count down via coroutine
            coroutineCountingDown = countingDown(num_increase);
            countDown = true;
            StartCoroutine(coroutineCountingDown);
            SetProgress(1f, 3f); // progress, speed
        }

        // change transparency of Buy Manager button based on if purchase can be made or not
        if (counter < 1000.0f)
        {
            managerButtonText.alpha = 0.37f;
        }
        else
        {
            managerButtonText.alpha = 1.0f;
        }
    }

    public void SetProgress(float Progress, float Speed)
    {
        if(Progress != ProgressImage.fillAmount)
        {
            if(AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(AnimateProgress(Progress, Speed));
        }
    }

    public IEnumerator AnimateProgress(float Progress, float Speed)
    {
        float time = 0;
        float initialProgress = ProgressImage.fillAmount;

        while (time < 1)
        {
            ProgressImage.fillAmount = Mathf.Lerp(initialProgress, Progress, time);
            time += Time.deltaTime * (1 / Speed);

            onProgress?.Invoke(ProgressImage.fillAmount);
            yield return null;
        }

        //yield return new WaitForSeconds(0.09f); // removing this reduces stuttering when manager is purchased

        // after progress bar has been animated then reset it
        ProgressImage.fillAmount = 0;
        updateCountDownUI(3.0f);

        // can prob get rid of OnCompleted, from the tutorial but I don't think it serves a purpose here
        onProgress?.Invoke(Progress);
        OnCompleted?.Invoke();
    }

    public void SaveButtonClicked()
    {
        SaveGame();
    }

    public void LoadButtonClicked()
    {
        LoadGame();
    }

    public void ResetButtonClicked()
    {
        ResetGame();
    }


    public void SaveGame()
    {
        PlayerPrefs.SetFloat("SavedNumIncrease", num_increase);
        PlayerPrefs.SetFloat("SavedCost", cost);
        PlayerPrefs.SetFloat("SavedCounter", counter);

        // save boolean as int 
        if (managerPurchased == true)
        {
            PlayerPrefs.SetInt("SavedManagerPurchased", 1);
        }
        else
        {
            PlayerPrefs.SetInt("SavedManagerPurchased", 0);
        }

        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedNumIncrease")) // have at least some saved data then load it
        {
            // retrieve floats
            num_increase = PlayerPrefs.GetFloat("SavedNumIncrease");
            cost = PlayerPrefs.GetFloat("SavedCost");
            counter = PlayerPrefs.GetFloat("SavedCounter");
            int intermediateManagerPurchased = PlayerPrefs.GetInt("SavedManagerPurchased");

            // retrieve boolean
            if (intermediateManagerPurchased == 1)
            {
                managerPurchased = true;
            }
            else
            {
                managerPurchased = false;
            }
            Debug.Log("Game data loaded!");

            // update UI with new count
            updateCounterUI();
        }
        else
            Debug.LogError("There is no save data!");
    }

    public void ResetGame()
    {
        // delete saved data
        PlayerPrefs.DeleteAll();

        // reset state of game to have default/ original val's
        num_increase = 1.0f;
        cost = 1.0f;
        counter = 0f;
        managerPurchased = false;

        // update counter UI and log reset
        updateCounterUI();
        Debug.Log("Data reset complete");
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
            SetProgress(1f, 3f); // progress, speed
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

            // Deactivates Buy Manager and Manual Click button
            BuyManager.gameObject.SetActive(false);
            ManualClick.gameObject.SetActive(false);
        }
    }

    public IEnumerator countingDown(float count_increase)
    {
        // count down for 1 seconds 
        float countDownLength = 3.0f; // CHANGE HERE TO INCREASE/ DECREASE COUNTDOWN
        float numIterations = 0.0f;

        // loop through count down using Wait 
        while(countDownLength - numIterations > 0)
        {
            numIterations += 1.0f;
            updateCountDownUI(countDownLength - numIterations);
            
            yield return new WaitForSeconds(1.0f);
        }

        //updateCountDownUI(0f); // removed or else the timer is not reset back to 00:03, it goes straight to 00;00
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
        } 
        else if(minutes>9.0f && seconds <= 9.0f)
        {
            textCountDown.text = (((int)minutes).ToString() + ":" + (0).ToString() + ((int)seconds).ToString());
        } 
        else if(minutes<=9.0f && seconds > 9.0f)
        {
            textCountDown.text = ((0).ToString() + ((int)minutes).ToString() + ":" + ((int)seconds).ToString());
        } 
        else if (minutes >9.0f && seconds > 9.0f)
        {
            textCountDown.text = (((int)minutes).ToString() + ":" + ((int)seconds).ToString());
        }

    }

    // updateCounterTextBox
    public void updateCounterUI()
    {
        //textCounter.text = (((int)counter).ToString("#,#")); // update GUI, round values 
        textCounter.text = $"{counter:n0}"; // ToString does not work when you need to print "0"
    }



}
