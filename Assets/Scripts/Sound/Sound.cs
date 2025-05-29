using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.StopMusic();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
