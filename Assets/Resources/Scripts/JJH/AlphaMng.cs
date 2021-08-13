using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaMng : MonoBehaviour {

    public static AlphaMng instance;
    public bool Fade;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        Fade = false;
    }

    public bool GetSetFade
    {
        get
        {
            return Fade;
        }
        set
        {
            Fade = value;
        }
    }
}
