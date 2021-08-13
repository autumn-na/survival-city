using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHPlayerHP : MonoBehaviour
{
    public bool bIsArmor;

    private int nHP = 100;

    public void SetHP(int _nHP)
    {
        nHP = _nHP;
    }

    public int GetHP()
    {
        return nHP;
    }

    public IEnumerator SetArmor()
    {
        bIsArmor = true;

        yield return new WaitForSeconds(1.0f);

        bIsArmor = false;
    }
}
