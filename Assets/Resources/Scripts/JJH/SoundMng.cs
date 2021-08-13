using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMng : MonoBehaviour {

    public static SoundMng instance;

    public static AudioSource Sound;
    public static AudioSource Sound2;
    public static AudioSource Sound3;
    public static AudioSource Sound4;

    public bool Mute;
    int BGM;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        Mute = false;
    }

    private void Start()
    {
        
        Sound = transform.GetChild(0).gameObject.GetComponent<AudioSource>();  //배경
        Sound2 = transform.GetChild(1).gameObject.GetComponent<AudioSource>(); //배경
        Sound3 = transform.GetChild(2).gameObject.GetComponent<AudioSource>(); //배경
        Sound4 = transform.GetChild(3).gameObject.GetComponent<AudioSource>(); // 효과음

    }

    public void SFX()
    {
        if (!Mute)
            Sound4.Play();
    }

    public void sound()
    {
        Sound.Play();
    }
    public void sound2()
    {
        Sound2.Play();
    }
    public void sound3()
    {
        if(Mute)
            Sound3.Play();
    }

    public void BackGround()
    {
        if (BGM == 0)
        {
            Sound.gameObject.GetComponent<AudioSource>().volume = 0;
            Sound2.gameObject.GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            Sound.gameObject.GetComponent<AudioSource>().volume = 1;
            Sound.gameObject.GetComponent<AudioSource>().volume = 1;
        }
    }
    public void BackGroundoff()
    {
        Sound.Stop();
    }
    public void BackGroundoff2()
    {
        Sound3.Stop();
    }
    public void BackGroundONOFF(int num)
    {
        BGM = num;
        BackGround();
    }
}
