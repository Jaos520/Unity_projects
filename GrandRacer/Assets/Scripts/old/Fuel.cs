using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour {


    float speed;

    bool take;

    Player pl;
    MainCntrl mn;

    // Use this for initialization
    void Start () {
        pl = GameObject.Find("Player").GetComponent<Player>();
        mn = GameObject.Find("Main_road").GetComponent<MainCntrl>();
        take = false;
    }
	
	// Update is called once per frame
	void Update () {
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
        if (take && gameObject.transform.localScale.y < 1.5f) gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0);
        else if (take) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Car" )
        {
            gameObject.transform.SetAsFirstSibling();
            take = true;
        }
        //float fu = Random.Range(1.0f, 2.0f);
        if (col.gameObject.tag == "Player")
        {
            col.GetComponent<Player>().fuel = col.GetComponent<Player>().Maxfuel;
            take = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Pit")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Coin")
        {
            Destroy(col.gameObject);
        }
    }
}
