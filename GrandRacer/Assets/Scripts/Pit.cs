using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pit : MonoBehaviour {

    Image img;
    public Sprite[] sprites;

    float speed;

    Player pl;
    MainCntrl mn;

    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Player>();
        mn = GameObject.Find("Main_road").GetComponent<MainCntrl>();

        img = gameObject.GetComponent<Image>();
        img.sprite = sprites[Random.Range(0, 2)];
    }

    private void Update()
    {
        speed = mn.MainSpeed;
        if (!pl.death && mn.start)
        {
            Move();
        }
        Away();
    }

    void Away()
    {
        if (gameObject.transform.position.y < -7 || gameObject.transform.position.y > 7)
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {
        gameObject.transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Car")
        {
            col.GetComponent<Cars>().Death(Random.Range(1,4));
        }
        if (col.gameObject.tag == "Player")
        {
            col.GetComponent<Player>().Damage();
        }
        if (col.gameObject.tag == "Coin")
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Fuel")
        {
            Destroy(col.gameObject);
        }
    }
}
