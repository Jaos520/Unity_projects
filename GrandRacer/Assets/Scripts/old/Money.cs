using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour {
    
    public Text cointext;

    void Start () {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", 0);
        }
    }
	
	void Update () {
        if (PlayerPrefs.GetInt("Money")>= 999999999)
        {
            PlayerPrefs.SetInt("Money", 999999999);
        }


        cointext.text = PlayerPrefs.GetInt("Money").ToString("0");
    }
}
