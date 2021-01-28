using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetter : MonoBehaviour
{
    public bool setOnStart = true;
    public bool setHappy = true;

    // Start is called before the first frame update
    void Start()
    {
        if (setOnStart)
        {
            SetSong();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSong()
    {
        if (setHappy)
        {
            AudioController.Instance.happy.TransitionTo(1);
        }
        else
        {
            AudioController.Instance.scared.TransitionTo(1);
        }
    }
}
