using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCoin : MonoBehaviour {
    float alfa;
    MainCntrl mn;
    Player pl;
    int plus;

    private void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Player>();
        mn = GameObject.Find("Main_road").GetComponent<MainCntrl>();
        alfa = 1;
        this.transform.position = new Vector3(pl.transform.position.x + Random.Range(-0.5f, 0.5f), pl.transform.position.y + Random.Range(-0.5f, 0.5f), 0);
        plus = Random.Range(5, 10);
        if (mn.Bonusrun) plus *= 2;
        gameObject.GetComponent<Text>().text = "+" + plus.ToString();
        mn.takeMoney += plus;
        mn.takeMoneyT.transform.localScale = new Vector3(1.5f, 1.5f, 1);
    }

    void FixedUpdate () {
        gameObject.transform.position += new Vector3(0, 0.01f, 0);
        gameObject.GetComponent<Text>().color = new Color(gameObject.GetComponent<Text>().color.r, gameObject.GetComponent<Text>().color.g, gameObject.GetComponent<Text>().color.b, alfa);
        if (alfa > 0) alfa -= 0.1f;
        else Destroy(gameObject);
	}
}
