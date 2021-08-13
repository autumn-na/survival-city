using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHWaveMng : MonoBehaviour
{
    public InGameUi IngameUICtrl;

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 private 변수

    private int nCurWave;
    private int nReadyTime;
    private int nWaveTime;
    private int nRestTime;

    private float fLeftTimeToNextState;

    private WaveState tWaveState;

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 public 변수

    public static NMHWaveMng instance;

    public enum WaveState
    {
        NULL,
        READY,
        WAVE,
        REST
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 Unity 함수

    private void Awake()                                            //싱글톤 정보 초기화 (맨 처음에)
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        InitMng();

        StartCoroutine(IEReady());
    }

    private void Update()
    {
        CheckLeftTime();
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 private 함수
    
    private void InitMng()
    {
        nCurWave = 0;                                               //현재 웨이브 0으로 초기화

        nReadyTime = 3;                                             //준비시간 : 3초
        nWaveTime = 30;                                             //웨이브 진행 시간 : 30초
        nRestTime = 10;                                             //웨이브 사이 휴식 시간 : 10초

        fLeftTimeToNextState = 0;                                   //다음 상태까지 남은 시간 : 0초

        tWaveState = WaveState.NULL;                                //웨이브 상태 초기화 : null
    }

    private void CheckLeftTime()                                    //다음 상태까지 남은 시간 구함
    {
        if (fLeftTimeToNextState > 0)
        {
            fLeftTimeToNextState -= Time.deltaTime;
        }
    }

    private IEnumerator IEReady()
    {
        SetWaveState(WaveState.READY);                              //웨이브 상태 레디로

        SetLeftTimeToNextState(GetReadyTime());                     //남은 시간을 레디 시간으로
        yield return new WaitForSeconds(GetReadyTime());            //기다림

        StartCoroutine(IEStartWave());                              //웨이브 시작
    }

    private IEnumerator IEStartWave()
    {
        SetWaveState(WaveState.WAVE);                               //웨이브 상태 변경 : WAVe

        SetLeftTimeToNextState(GetWaveTime());                      //남은 시간을 웨이브 시간으로

        SetCurWave(GetCurWave() + 1);                               //현재 웨이브 + 1 (단계 증가)

        NMHMapMng.instance.SpawnGun();

        for (int i = 0; i < GetCurWave() + 4; i++)                  //현재 웨이브 + 4만큼 좀비 생성
        {
            SpawnZombie();
        }

        IngameUICtrl.SetWaveNum(GetCurWave());

        yield return new WaitForSeconds(GetWaveTime());             //기다림

        StartCoroutine(IEStartRest());                              //휴식 시작
    }

    private IEnumerator IEStartRest()
    {
        SetWaveState(WaveState.REST);                              //웨이브 상태 변경 : 휴식

        SetLeftTimeToNextState(GetRestTime());                      //남은 시간을 휴식 시간으로
        yield return new WaitForSeconds(GetRestTime());             //기다림

        StartCoroutine(IEStartWave());                              //다시 웨이브 시작 (반복)
    }

    private void SpawnZombie()
    {
        NMHMapMng.instance.SpawnUnit(NMHUnit.UnitType.ZOMBIE);
    }
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 일반 public 함수

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 getter, setter 함수

    public int GetCurWave()
    {
        return nCurWave;
    }

    public void SetCurWave(int _nTWave)
    {
        nCurWave = _nTWave;
    }

    public int GetReadyTime()
    {
        return nReadyTime;
    }

    public int GetWaveTime()
    {
        return nWaveTime;
    }

    public void SetWaveTime(int _nTWaveTime)
    {
        nWaveTime = _nTWaveTime;
    }

    public int GetRestTime()
    {
        return nRestTime;
    }

    public void SetRestTime(int _nTRestTime)
    {
        nRestTime = _nTRestTime;
    }

    public float GetLeftTimeToNextState()
    {
        return fLeftTimeToNextState;
    }

    public void SetLeftTimeToNextState(float _fTime)
    {
        fLeftTimeToNextState = _fTime;
    }

    public WaveState GetWaveState()
    {
        return tWaveState;
    }

    public void SetWaveState(WaveState _tTWaveState)
    {
        tWaveState = _tTWaveState;
    }
}
