using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDamage : MonoBehaviour {

    float alfa;
    MainCntrl mn;
    Player pl;
    int damage;

    private void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Player>();
        mn = GameObject.Find("Main_road").GetComponent<MainCntrl>();
        alfa = 1;
        this.transform.position = new Vector3(pl.transform.position.x + Random.Range(-0.5f, 0.5f), pl.transform.position.y + Random.Range(-0.5f, 0.5f), 0);
        damage = Random.Range(50, 200);
        gameObject.GetComponent<Text>().text = damage.ToString();
        pl.Health -= damage;
    }

    void Update()
    {
        gameObject.transform.position += new Vector3(0, 0.01f, 0);
        gameObject.GetComponent<Text>().color = new Color(gameObject.GetComponent<Text>().color.r, gameObject.GetComponent<Text>().color.g, gameObject.GetComponent<Text>().color.b, alfa);
        if (alfa > 0) alfa -= 0.01f;
        else Destroy(gameObject);
    }
}
