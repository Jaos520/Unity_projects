using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skin : MonoBehaviour {

    public int id;
    public string name;
    public int hp;
    public float fuel;
    public float speed;

    public int price;
    //public Text priceT;

    int spendi;
    
    public int money;
    public int getprice;
    
    public bool changed;
    public bool bought;
    public bool buy;
    
    void Start () {

        if (!PlayerPrefs.HasKey("id" + id.ToString("0")))
        {
            if (id == 0)
            {
                PlayerPrefs.SetInt("id" + id.ToString("0"), 1);
            }
            else
            {
                PlayerPrefs.SetInt("id" + id.ToString("0"), 0);
            }
        }
        
        money = PlayerPrefs.GetInt("Money");
        getprice = price;

        changed = false;
        buy = false;

        if (PlayerPrefs.GetInt("id" + id.ToString("0")) == 0)
        {
            bought = false;
        }
        else if (PlayerPrefs.GetInt("id" + id.ToString("0")) == 1)
        {
            bought = true;
        }
    }
	
	void Update () {
        
        if (bought)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else if (!bought)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }

        if (buy)
        {
            if (money > 0 && price > 0 && !bought)
            {
                price -= spendi;
                money -= spendi;
                spendi = spendi+1;

                if (price <= 0)//&& money >= getprice)
                {
                    money = money - price;
                    bought = true;
                    PlayerPrefs.SetInt("id" + id.ToString("0"), 1);
                    PlayerPrefs.SetInt("Money", money);
                }
                else if (money < 0) UpBuy();
            }
        }

	}

    public void Buy()
    {
        money = PlayerPrefs.GetInt("Money");
        buy = true;
        spendi = 1;
    }
    public void UpBuy()
    {
        buy = false;
        if (!bought)
        {
            price = getprice;
        }
    }
}
