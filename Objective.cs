using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{

    public bool isOn = false;
    public GameObject Panel;
    public bool failSafe = false;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("OKey"))
        {
            if (isOn == false && failSafe == false)
            {
                Debug.Log("O KEY PRESSED - OBJECTIVE DISPLAY ON");
                failSafe = true;
                Panel.SetActive(true);
                isOn = true;
                StartCoroutine(FailSafe());
            }
            if (isOn == true && failSafe == false)
            {
                Debug.Log("O KEY PRESSED - OBJECTIVE DISPLAY OFF");
                failSafe = true;
                Panel.SetActive(false);
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

