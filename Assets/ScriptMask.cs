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

    // Start is called before the first frame update
    void Start()
    {
        // create prefab pieces 
        GameObject Background = Instantiate(darkPreFabStationaryBackground, new Vector3(0, 0, 0), Quaternion.identity);
        Background.transform.SetParent(parent.transform, false);
        
        for (int i=0; i< numBrightBlocks; i++)
        {
            // Instantiate at position (0, 0, 0) and zero rotation.
            GameObject temp_game_object = Instantiate(brightPreFab, new Vector3(0, 0, 0), Quaternion.identity);
            temp_game_object.transform.SetParent(parent.transform, false);
            temp_game_object.transform.localPosition = new Vector3(32.5f + (float)(130*i), 0, 0f);
            //temp_game_object.transform.localScale = Vector3.one * 1f; //  sets scale to 1, objects were not scaling properly for some reason
            blocks.Add(temp_game_object);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
