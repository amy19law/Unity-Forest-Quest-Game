using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{

    public bool isOn = false;
    public GameObject lightSource;
    public bool failSafe = false;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("FKey"))
        {
            if (isOn == false && failSafe == false)
            {
                Debug.Log("F KEY PRESSED - FLASH LIGHT ON!");
                failSafe = true;
                lightSource.SetActive(true);
                isOn = true;
                StartCoroutine(FailSafe());
            }
            if (isOn == true && failSafe == false)
            {
                Debug.Log("F KEY PRESSED - FLASH LIGHT OFF!");
                failSafe = true;
                lightSource.SetActive(false);
                isOn = false;
                StartCoroutine(FailSafe());
            }
        }
    }

    IEnumerator FailSafe()
    {
        yield return new WaitForSeconds(0.25f);
        failSafe = false;
    }
}
