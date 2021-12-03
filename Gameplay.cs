using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
	public GameObject itemText;
	public GameObject item1;
	public GameObject item2;
	public GameObject item3;
	public GameObject item4;
	public GameObject item5;
	public GameObject gameEnd;
	public int itemsNum;

   void Update()
   {
   	if(Input.GetButtonDown("CKey"))
   	{
   		Debug.Log("C KEY PRESSED - ITEM COLLECTED!");
         item1.SetActive(false);
         itemsNum += 1;
         itemText.GetComponent<Text>().text = "ITEMS COLLECTED: " + itemsNum;
      }
      if(Input.GetButtonDown("VKey"))
      {
         Debug.Log("V KEY PRESSED - ITEM COLLECTED!");
         item2.SetActive(false);
         itemsNum += 2;
         itemText.GetComponent<Text>().text = "ITEMS COLLECTED: " + itemsNum;
      }
      if(Input.GetButtonDown("BKey"))
      {
         Debug.Log("B KEY PRESSED - ITEM COLLECTED!");
         item3.SetActive(false);
         itemsNum += 3;
         itemText.GetComponent<Text>().text = "ITEMS COLLECTED: " + itemsNum;
      }
      if(Input.GetButtonDown("NKey"))
      {
         Debug.Log("N KEY PRESSED - ITEM COLLECTED!");
         item4.SetActive(false);
         itemsNum += 4;
         itemText.GetComponent<Text>().text = "ITEMS COLLECTED: " + itemsNum;
      }
      if(Input.GetButtonDown("MKey"))
      {
         Debug.Log("M KEY PRESSED - ITEM COLLECTED!");
         item5.SetActive(false);
         itemsNum += 5;
         itemText.GetComponent<Text>().text = "ITEMS COLLECTED: " + itemsNum;
         gameEnd.SetActive(true);
      }
   }
}