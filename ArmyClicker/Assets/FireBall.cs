using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    Game gm;
    public Vector3 fortress;

    private void Start()
    {
        gm = GameObject.Find("Main Camera").GetComponent<Game>();
        fortress = GameObject.FindGameObjectWithTag("Fortress").GetComponent<Transform>().position;

        gameObject.transform.localScale = new Vector3(3, 3, 1);
        gameObject.transform.position = new Vector3(4, 4.2f, 0);
    }

    void Update () {
        // gameObject.transform.position -= new Vector3(0.2f, 0.2f, 0);
        gameObject.transform.localScale -= new Vector3(1, 1, 0) * Time.deltaTime * 2f;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, fortress, Time.deltaTime * 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fortress")
        {
            collision.gameObject.GetComponent<Fortress>().CatapultDmg(Random.Range(50*gm.Catapult, 50*gm.Catapult+50));
            Destroy(gameObject);
        }
    }
}
