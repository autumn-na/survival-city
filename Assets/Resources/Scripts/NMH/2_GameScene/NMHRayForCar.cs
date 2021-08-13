using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHRayForCar : MonoBehaviour
{
    public GameObject objSelectedCar;

    Ray ray;

    public bool bIsCanDrive;

	void Start ()
    {
        bIsCanDrive = false;
    }
	
	void Update ()
    {
        Ray();
        RayCasting();
    }

    void Ray()
    {
        ray = transform.Find("Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
    }

    void RayCasting()
    {
        RaycastHit rhitCar;


        if (Physics.Raycast(ray, out rhitCar, 1f))
        {
            if(rhitCar.transform.name == "Body")
            {
                bIsCanDrive = true;

                objSelectedCar = rhitCar.transform.gameObject;
            }
            else if(rhitCar.transform.name != "Body")
            {
                bIsCanDrive = false;

                objSelectedCar = null;
            }
            else
            {
                bIsCanDrive = false;

                objSelectedCar = null;
            }
        }

        Debug.DrawRay(transform.Find("Camera").GetComponent<Camera>().transform.position,transform.Find("Camera").GetComponent<Camera>().transform.forward * 100, Color.red);
        
    }
}
