using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using GoogleMobileAds.Api;

public class Shop : MonoBehaviour {
    public bool test;

    public Transform posCenter;
    public Transform posLeft;
    public Transform posRight;
    public Transform posLeftback;
    public Transform posRightback;

    public int ChangeId;
    public GameObject[] id;
    public int MaxSizeArray;

    public Text Price;
    public Text NameCh;
    public Text HP;
    public Text Fuel;
    public Text Speed;

    public Transform Leftbtn;
    public Transform Rightbtn;

    void Start () {

        //PlayerPrefs.SetFloat("Money", 50000);
        //PlayerPrefs.SetInt("Gears", 50000);

        //posCenter = new Vector2(0, -10);
        //posLeft = new Vector2(-1, -10);
        //posRight = new Vector2(1, -10);

        if (!PlayerPrefs.HasKey("Skin"))
        {
            ChangeId = 0;
            PlayerPrefs.SetInt("Skin", 0);
        }
        ChangeId = PlayerPrefs.GetInt("Skin");

        if (!PlayerPrefs.HasKey("Hp"))
        {
            PlayerPrefs.SetInt("Hp", 0);
            PlayerPrefs.SetFloat("Fuel", 0);
            PlayerPrefs.SetFloat("Speed", 0);
        }
	}

    void Update() {

        ChoosePos();

        States();

        Btn();
    }

    void Btn()
    {
        if (Leftbtn.localScale.y > 1) Leftbtn.localScale -= new Vector3(0.1f, 0.1f, 0);
        if (Rightbtn.localScale.y > 1) Rightbtn.localScale -= new Vector3(0.1f, 0.1f, 0);
    }

    void States()
    {
        if (id[ChangeId].GetComponent<Skin>().bought) Price.text = "Purchased";
        else Price.text = id[ChangeId].GetComponent<Skin>().price.ToString("0");

        NameCh.text = id[ChangeId].GetComponent<Skin>().name;
        HP.text = id[ChangeId].GetComponent<Skin>().hp.ToString("0");
        Fuel.text = id[ChangeId].GetComponent<Skin>().fuel.ToString("0")+"L";
        Speed.text = id[ChangeId].GetComponent<Skin>().speed.ToString("0");

        if (id[ChangeId].GetComponent<Skin>().bought)
        {
            PlayerPrefs.SetInt("Hp", id[ChangeId].GetComponent<Skin>().hp);
            PlayerPrefs.SetFloat("Fuel", id[ChangeId].GetComponent<Skin>().fuel);
            PlayerPrefs.SetFloat("Speed", id[ChangeId].GetComponent<Skin>().speed);
        }

    }

    void ChoosePos()
    {
        int i = ChangeId+2;
        if (id[ChangeId].GetComponent<Skin>().bought)
        {
            PlayerPrefs.SetInt("Skin", ChangeId);
        }


        id[ChangeId].transform.localScale = new Vector3(0.5f, 0.5f, 1);
        id[ChangeId].transform.position = Vector3.Lerp(id[ChangeId].transform.position, posCenter.position, Time.deltaTime * 10);
        id[ChangeId].GetComponent<Skin>().changed = true;

        

        if (ChangeId + 1 <= MaxSizeArray)
        {
            id[ChangeId + 1].transform.position = Vector3.Lerp(id[ChangeId + 1].transform.position, posRight.position, Time.deltaTime * 10);
            id[ChangeId + 1].transform.localScale = new Vector3(0.4f, 0.4f, 1);
        }
        if ((ChangeId - 1) >= 0)
        {
            id[ChangeId - 1].transform.position = Vector3.Lerp(id[ChangeId - 1].transform.position, posLeft.position, Time.deltaTime * 10);
            id[ChangeId - 1].transform.localScale = new Vector3(0.4f, 0.4f, 1);
        }

        for (; i <= MaxSizeArray; i++)
        {
            id[i].transform.position = Vector3.Lerp(id[i].transform.position, posRightback.position, Time.deltaTime * 10);
        }

        for (i=ChangeId-2; i>=0; i--)
        {
            id[i].transform.position = Vector3.Lerp(id[i].transform.position, posLeftback.position, Time.deltaTime * 10);
        }
    }

    public void Left()
    {
        if (ChangeId != 0)
        {
            Leftbtn.localScale = new Vector3(2, 2, 1);
            id[ChangeId].GetComponent<Skin>().changed = false;
            ChangeId -= 1;
        }

        
    }
    public void Right()
    {
        if (ChangeId < MaxSizeArray)
        {
            Rightbtn.localScale = new Vector3(2, 2, 1);
            id[ChangeId].GetComponent<Skin>().changed = false;
            ChangeId += 1;
        }
    }
}
