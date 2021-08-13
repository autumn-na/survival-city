using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LogoScene : MonoBehaviour
{
    public Animator Ani;
    void Start()
    {
        
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("1_MainScene");
    }
}