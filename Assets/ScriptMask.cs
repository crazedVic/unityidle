using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptMask : MonoBehaviour
{
    public GameObject parent;
    public List<GameObject> blocks = new List<GameObject>();
    public GameObject brightPreFab;
    public GameObject darkPreFabStationaryBackground;

    int numBrightBlocks = 3;
    int numDarkBlocks = 3;


    // Start is called before the first frame update
    void Start()
    {
        GameObject Background = Instantiate(darkPreFabStationaryBackground, new Vector3(0, 0, 0), Quaternion.identity);
        Background.transform.SetParent(parent.transform, false);
        // create prefab pieces 
        // Instantiate at position (0, 0, 0) and zero rotation.
        for (int i=0; i< numBrightBlocks; i++)
        {
            
            GameObject temp_game_object = Instantiate(brightPreFab, new Vector3(0, 0, 0), Quaternion.identity);
            temp_game_object.transform.SetParent(parent.transform, false);
            temp_game_object.transform.localPosition = new Vector3(32.5f + (float)(130*i), 0, 0f);
            //temp_game_object.transform.localScale = Vector3.one * 1f; //  sets scale to 1, objects were not scaling properly for some reason
            blocks.Add(temp_game_object);
            /*
            GameObject temp_game_object1 = Instantiate(darkPreFab, new Vector3(0, 0, 0), Quaternion.identity);
            temp_game_object1.transform.SetParent(parent.transform, false);
            //temp_game_object1.transform.parent = parent.transform;
            temp_game_object.transform.localPosition = new Vector3(97.5f + (float)(130*i), 0, 0f);
            blocks.Add(temp_game_object1);
           */
        }
        


        //myNewSmoke.transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
