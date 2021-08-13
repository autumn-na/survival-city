using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NMHGun
{
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 private 변수

    public int nMaxBullet;                 //무기의 최대 총알 수
    public int nMaxMagazine;               //무기의 1회 장전시 장전되는 양

    public int nCurBullet;                 //무기의 현재 탄창을 제외하고 남은 총 총알 수
    public int nCurMagazine;               //무기의 현재 탄창의 남은 총알 수

    public int nMinFoundBullet;            //무기 획득시 최소 총알 수
    public int nMaxFoundBullet;            //무기 획득 시 최대 총알 수

    public float fShootDelay;              //연사 딜레이
    public float fReloadDelay;             //재장전 딜레이

    public bool bIsChangeble;                //단, 연발 교체

    public int nDamage;                    //데미지

    public GunType tGunType;            // 총의 종류

    public enum GunType
    {
        PISTOL,
        RIPLE,
        SHOTGUN
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 Unity 변수

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 private 함수
    
    /// <summary>
    /// 재장전 시 현재 총알과 남은 총알을 변화시킨다.
    /// </summary>
    public void Reload()
    {
        if (nCurBullet > 0)                                             //현재 남은 총알의 갯수가 0보다 크면
        {
            int nSpace = nMaxMagazine - nCurMagazine;                   //탄창의 빈 공간 체크

            if (nSpace == 0)                                             //빈 공간 이 없다면
            {
                return;                                                 //함수 종료
            }
            else
            {
                nCurBullet -= nSpace;                                   // 빈 공간만큼 남은 총알의 갯수에서 뺌
                nCurMagazine = nMaxMagazine;                            // 현재 탄창을 꽉 채움
            }
        }
    }

    /// <summary>
    /// 총 발사 시 현재 총알과 남은 총알을 변화시킨다.
    /// </summary>
    public void Shoot()
    {
        if(nCurMagazine > 0)                                            //현재 탄창의 총알의 갯수가 0보다 많으면
        {
            nCurMagazine--;                                             //현재 탄창의 갯수 -1
        }
    }
}
