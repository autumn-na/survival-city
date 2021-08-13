using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHCarCtrl : MonoBehaviour
{
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 private 변수

    float fMovePower;

    bool bIsDriving;

    GameObject objPlayer;

    GameObject objMapParent;

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 public 변수

    public Camera camCar;

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 unity 함수

    void Start()
    {
        InitCar();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && objPlayer.GetComponent<NMHRayForCar>().objSelectedCar == transform.Find("Body").gameObject)
        {
            if (bIsDriving == false)
            {
                objPlayer.transform.Find("Camera").Find("Body").GetComponent<Animator>().Play("Exit");
                objPlayer.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                objPlayer.transform.position = transform.position + new Vector3(0, -5, 0);
                objPlayer.transform.rotation = transform.rotation;
                
                this.gameObject.transform.parent = objPlayer.transform;     //F누르면 임시로 차 탄거처럼 만듬

                camCar.gameObject.SetActive(true);                          //자동차 카메라를 킴

             //   objPlayer.transform.FindChild("Camera").gameObject.SetActive(false);            //플레이어의 카메라를 끔
                objPlayer.GetComponent<PlayerMove>().enabled = false;                                       //플레이어 움직이는 스크립트 끔
                objPlayer.GetComponent<CapsuleCollider>().enabled = false;      //콜리더 끔
                objPlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                objPlayer.AddComponent<BoxCollider>();
                objPlayer.GetComponent<BoxCollider>().size = new Vector3(2, 20f, 5f);
                objPlayer.GetComponent<BoxCollider>().center = new Vector3(0, 14.3f, 0);

                SetIsDriving(true);
            }
            else if (bIsDriving == true)
            {
                this.gameObject.transform.parent = objMapParent.transform;     //(임시) 차에서 내림

                camCar.gameObject.SetActive(false);                                                     //차 타면서 껏던것 모두 키고, 킨 것 모두 끔
                objPlayer.transform.position = transform.position + new Vector3(0, 2.5f, 0);
                objPlayer.transform.Find("Camera").gameObject.SetActive(true);
                objPlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                objPlayer.GetComponent<PlayerMove>().enabled = true;
                objPlayer.GetComponent<CapsuleCollider>().enabled = true;
                Destroy(objPlayer.GetComponent<BoxCollider>());

                SetIsDriving(false);

                objPlayer.GetComponent<NMHRayForCar>().objSelectedCar = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (bIsDriving == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                objPlayer.transform.Translate(Vector3.forward * fMovePower * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                objPlayer.transform.Rotate(new Vector3(0, -90f, 0) * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                objPlayer.transform.Translate(-Vector3.forward * fMovePower * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                objPlayer.transform.Rotate(new Vector3(0, 90f, 0) * Time.deltaTime);
            }

            objPlayer.transform.position = transform.position + new Vector3(0, -5, 0);
        }
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 private 함수

    void InitCar()
    {
        fMovePower = 15f;

        bIsDriving = false;

        objPlayer = GameObject.Find("Player");

        objMapParent = GameObject.Find("Map");
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 getter, setter

    public bool GetIsDriving()
    {
        return bIsDriving;
    }

    public void SetIsDriving(bool _bTDriving)
    {
        bIsDriving = _bTDriving;
    }
}
