using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
	public Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = exitButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void TaskOnClick()
    {
    	Debug.Log("Redirecting back to Menu");
        Application.LoadLevel("Menu");
    }
}
