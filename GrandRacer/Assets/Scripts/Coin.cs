using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Coin : MonoBehaviour {
    
    Image img;
    public Sprite[] sprites;

    public GameObject textCoin;

    float speed;
    bool take;

    Player pl;
    MainCntrl mn;

    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Player>();
        mn = GameObject.Find("Main_road").GetComponent<MainCntrl>();
        take = false;

       

        img = gameObject.GetComponent<Image>();
        StartCoroutine(NukeMethod());
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
        if (take && gameObject.transform.localScale.y < 1.5f) gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0);
        else if (take)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Car" && !take)
        {
            //gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
            take = true;
        }
        if (col.gameObject.tag == "Player" && !take)
        {
            Instantiate(textCoin, new Vector3(Random.Range(-2,2), Random.Range(-4, 4), 0), Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
            take = true;
        }
        if (col.gameObject.tag == "Coin")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Pit")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Fuel")
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator NukeMethod()
    {
        //destroy all game objects
        for (int i = 0; i < sprites.Length; i++)
        {
            img.sprite = sprites[i];
            if (i == sprites.Length-1) i = 0;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
