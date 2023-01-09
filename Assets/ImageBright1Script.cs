using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageBright1Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.localPosition = new Vector3(-196f, 0, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // "telelport" block to beginning of animation if it moves past 175f x position
        if (transform.localPosition[0] > 357.5f)
        {
            transform.localPosition = new Vector3(-32.5f, 0, 0f);
        }

        // move block to right every frame
        transform.Translate(1f, 0f, 0f);
    }
}
