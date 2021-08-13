using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHMapMng : MonoBehaviour
{
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 private 변수
    GameObject objPlayer;

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 public 변수

    public static NMHMapMng instance;

    public GameObject objUnitParent;
    public GameObject objZombieSpawner;

    public GameObject objZombiePrefab;

    public GameObject objGunSpawner;
    public GameObject[] objGunArr;

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 Unity 함수

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        InitMng();
    }

    public void Start()
    {
       
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 private 함수

    private void InitMng()
    {
        objPlayer = GameObject.Find("Player");
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 public 함수

    public void SpawnUnit(NMHUnit.UnitType _objT)
    {
       if(_objT == NMHUnit.UnitType.ZOMBIE)
        {
            BoxCollider[] colZombieSpawner = new BoxCollider[12];                                                               //박스 콜리더 객체 생성
            colZombieSpawner = objZombieSpawner.GetComponentsInChildren<BoxCollider>();                                         //좀비 스포너의 콜리더를 가져옴

            BoxCollider colTmp = new BoxCollider();                                                                             //정렬을 위한 임시 콜리더 객체

            for (int i = 0; i < 12; i ++)                                                                                       //버블 정렬 : 플레이어로부터 가장 가까운 콜리더 순서대로 정렬
            {
                for(int j = i + 1; j < 12; j++)
                {
                    float fDistance1 = Vector3.Distance(objPlayer.transform.position, colZombieSpawner[i].transform.position);
                    float fDistance2 = Vector3.Distance(objPlayer.transform.position, colZombieSpawner[j].transform.position);

                    if (fDistance1 > fDistance2)
                    {
                        colTmp = colZombieSpawner[i];
                        colZombieSpawner[i] = colZombieSpawner[j];
                        colZombieSpawner[j] = colTmp;
                    }
                }
            }

            int nRandSpawner = Random.Range(0, 3);                                                                              //가장 가까운 콜리더 3개 중 하나 선별

            Vector3 vec3Pos = new Vector3();                                                                                    //좀비 생성 위치 위한 Vec3 객체

            vec3Pos = colZombieSpawner[nRandSpawner].transform.position;                                                                                        //콜리더의 위치 대입
            vec3Pos.x = vec3Pos.x + Random.Range(-(colZombieSpawner[nRandSpawner].size.x / 2), colZombieSpawner[nRandSpawner].size.x / 2);                      //콜리터의 x, y 사이즈에 다라 생성 위치의 x, z값을 랜덤하게 넣어줌 (콜리더 범위 안으로)
            vec3Pos.z = vec3Pos.z + Random.Range(-(colZombieSpawner[nRandSpawner].size.z / 2), colZombieSpawner[nRandSpawner].size.z / 2);

            GameObject objClone = Instantiate(objZombiePrefab, vec3Pos, Quaternion.identity, objUnitParent.transform);
        }
    }

    public void SpawnGun()
    {
        BoxCollider[] colGunSpawner = new BoxCollider[10];                                                               //박스 콜리더 객체 생성
        colGunSpawner = objGunSpawner.GetComponentsInChildren<BoxCollider>();                                         //좀비 스포너의 콜리더를 가져옴

        BoxCollider colTmp = new BoxCollider();                                                                             //정렬을 위한 임시 콜리더 객체

        for(int i = 0; i < 10; i ++)
        {
            int nRandSpawner = Random.Range(0, 9);                                                                              //가장 가까운 콜리더 3개 중 하나 선별

            int nRandRun = Random.Range(1, 3);

            Vector3 vec3Pos = new Vector3();                                                                                    //총 생성 위치 위한 Vec3 객체

            vec3Pos = colGunSpawner[nRandSpawner].transform.position;                                                                                        //콜리더의 위치 대입
            vec3Pos.x = vec3Pos.x + Random.Range(-(colGunSpawner[nRandSpawner].size.x / 2), colGunSpawner[nRandSpawner].size.x / 2);                      //콜리터의 x, y 사이즈에 다라 생성 위치의 x, z값을 랜덤하게 넣어줌 (콜리더 범위 안으로)
            vec3Pos.z = vec3Pos.z + Random.Range(-(colGunSpawner[nRandSpawner].size.z / 2), colGunSpawner[nRandSpawner].size.z / 2);

            GameObject objClone = Instantiate(objGunArr[nRandRun], vec3Pos, Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 90f)), objUnitParent.transform);
        }
    }
}
