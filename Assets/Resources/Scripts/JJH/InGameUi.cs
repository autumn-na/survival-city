using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUi : MonoBehaviour
{
    public GameObject Resume;
    public UnityEngine.UI.Image fadePan;
    public Text Countdown;
    float fades = 1.0f;
    float time = 0;

    public Text txtHP;
    public Text txtWaveText;
    public Text txtKillNum;

    public Text txtBulletNum;

    public GameObject Gameover;

    public Text txtWave;
    public Text txtKill;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (AlphaMng.instance.GetSetFade)  // 사라짐
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
                fadePan.gameObject.SetActive(false);
                time = 0;
            }
        }
        if (AlphaMng.instance.GetSetFade == false)  // 채워짐
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
                SceneManager.LoadScene("1_MainScene");
                time = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Resume.SetActive(true);
        }

        SetBulletNum(GameObject.Find("Player").GetComponent<PlayerMove>().Gun.nCurMagazine, GameObject.Find("Player").GetComponent<PlayerMove>().Gun.nCurBullet);
        SetKillNum(GameObject.Find("Player").GetComponent<PlayerMove>().KillCount);

        SetHPText();
    }

    IEnumerator CountDown()
    {
        Countdown.text = "3";
        yield return new WaitForSeconds(1.0f);
        Countdown.text = "2";
        yield return new WaitForSeconds(1.0f);
        Countdown.text = "1";
        yield return new WaitForSeconds(1.0f);
        Countdown.gameObject.SetActive(false);
        StopCoroutine(CountDown());
    }

    public void RetryButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("2_GameScene");
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        Resume.SetActive(false);
        Countdown.gameObject.SetActive(true);
        StartCoroutine(CountDown());
    }

    public void MainmenuButton()
    {
        Time.timeScale = 1;
        fadePan.gameObject.SetActive(true);
        AlphaMng.instance.GetSetFade = false;
        fades = 0.0f;
    }

    public void SetWaveNum(int _nTWave)
    {
        txtWaveText.text = _nTWave.ToString();
    }

    public void SetKillNum(int _nTKill)
    {
        txtKillNum.text = _nTKill.ToString();
    }

    public void SetBulletNum(int _magazine, int _left)
    {
        txtBulletNum.text = _magazine.ToString() + "/" + _left.ToString();
    }

    public void SetHPText()
    {
        txtHP.text = GameObject.Find("Player").GetComponent<NMHPlayerHP>().GetHP().ToString() + "/" + 100.ToString(); 
    }

    public void ShowGameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Gameover.SetActive(true);
        
        txtWave.text = NMHWaveMng.instance.GetCurWave().ToString();
        txtKill.text = txtKillNum.text;
    }
}
