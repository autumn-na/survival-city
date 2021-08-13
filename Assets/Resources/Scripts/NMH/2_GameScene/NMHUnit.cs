using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHUnit : MonoBehaviour
{
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 protected 변수

    protected int nID;
    protected int nHP;

    protected float fMoveSpeed;

    UnitType tUnitType;

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 public 변수

    public enum UnitType
    {
        NULL,
        PLAYER,
        ZOMBIE
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 Unity 함수

    private void Start ()
    {
        InitUnit(-1, -1, -1, UnitType.NULL);
    }

    private void Update()
    {
       
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 protected 함수

    protected void InitUnit(int _nTID, int _nTHP, float _fTMoveSpeed, UnitType _tTUnitType)                                       //유닛 초기화 함수
    {
        nID = _nTID;                                                   //ID 초기화 : -1이면 이상한놈 (-1이 아니어야 함)
        nHP = _nTHP;                                                   //HP 초기화 : -1이면 이상한놈

        fMoveSpeed = _fTMoveSpeed;                                      //이동속도 초기화 : -1이면 이상한놈

        tUnitType = _tTUnitType;                                        //유닛타입 초기화 : 널이면 이상한놈
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 Getter, Setter

    public int GetID()
    {
        return nID;
    }

    public void SetID(int _nTID)
    {
        nID = _nTID;
    }

    public int GetHP()
    {
        return nHP;
    }

    public void SetHP(int _nTHP)
    {
        nHP = _nTHP;
    }

    public float GetMoveSpeed()
    {
        return fMoveSpeed;
    }

    public void SetMoveSpeed(float _fTMoveSpeed)
    {
        fMoveSpeed = _fTMoveSpeed;
    }
    public UnitType GetUnitType()
    {
        return tUnitType;
    }

    public void SetUnitType(UnitType _tTUnitType)
    {
        tUnitType = _tTUnitType;
    }
}
