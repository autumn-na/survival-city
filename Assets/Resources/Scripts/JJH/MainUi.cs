using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUi : MonoBehaviour {

    public GameObject[] mainUi;
    public GameObject[] CheckUi;
    public GameObject[] PopUpUi;
    public GameObject[] HelpUi;
    bool Check;
    public UnityEngine.UI.Image fadePan;
    float fades = 1.0f;
    float time = 0;

    // Use this for initialization
    void Start () {
        Check = false;
        SoundMng.instance.sound();
        StartCoroutine("Music2Start");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (AlphaMng.instance.GetSetFade==false) // 사라짐
        {
            time += Time.deltaTime;
            if (fades > 0.0f && time >= 0.1f)
            {
                fades -= 0.1f;
                fadePan.color = new Color(0, 0, 0, fades);
                time = 0;
            }
            else if (fades <= 0.0f)
            {
                time = 0;
                fadePan.gameObject.SetActive(false);
            }
        }

        if(AlphaMng.instance.GetSetFade)  //흑백으로 채워짐
        {
            time += Time.deltaTime;
            if (fades < 1.0f && time >= 0.1f)
            {
                fades += 0.1f;
                fadePan.color = new Color(0, 0, 0, fades);
                time = 0;
            }
            else if (fades >= 1.0f)
            {
                SceneManager.LoadScene("2_GameScene");
                StopCoroutine(Music2Start());
                SoundMng.instance.BackGroundoff();
                time = 0;
            }
        }
    }
    IEnumerator Music2Start()
    {
        while(true)
        {
            yield return new WaitForSeconds(5.0f);
            SoundMng.instance.sound2();

        }
    }
    public void GameScene()
    {
        fadePan.gameObject.SetActive(true);
        AlphaMng.instance.GetSetFade = true;
        fades = 0;
    }

    public void Sizeup(int num)
    {
        mainUi[num].transform.localScale = new Vector3(1.3f,1.3f,1);
    }

    public void Sizedown(int num)
    {
        mainUi[num].transform.localScale = new Vector3(1, 1, 1);
    }

    public void CheckBox(int num)
    {
        if (Check == false)
        {
            CheckUi[num].SetActive(true);
            if (num == 2)
            {
                SoundMng.instance.Mute = true;
                SoundMng.instance.BackGroundONOFF(0);
            }
            Check = true;
        }
        else
        {
            CheckUi[num].SetActive(false);
            if (num == 2)
            {
                SoundMng.instance.Mute = false;
                SoundMng.instance.BackGroundONOFF(1);
            }
            Check = false;
        }

    }

    public void SettingOn(int num)
    {
        PopUpUi[num].SetActive(true);
    }

    public void SettingOff(int num)
    {
        PopUpUi[num].SetActive(false);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void HelpPopUp()
    {
        HelpUi[0].SetActive(false);
        HelpUi[1].SetActive(false);
    }
    public void HelpPopUpon()
    {
        HelpUi[0].SetActive(true);
        HelpUi[1].SetActive(true);
    }


}
