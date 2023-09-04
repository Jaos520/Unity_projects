using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Army : MonoBehaviour {

    Game gm;
    Fortress fortress;
    bool swap;

    public float alfaColor;

	void Start () {
        gm = GameObject.Find("Main Camera").GetComponent<Game>();
        fortress = GameObject.FindGameObjectWithTag("Fortress").GetComponent<Fortress>();
        gameObject.transform.position = new Vector3(0, -6, 0);
        swap = false;
        alfaColor = 1;
	}
	
	void Update () {
        Move();
        MarchFun();
	}
    
    void Vanish()
    {
        gameObject.GetComponent<Image>().color = new Color(0, 0, 0, alfaColor);
        alfaColor -= 2f * Time.deltaTime;
        if (alfaColor <= 0)
        {
            Destroy(gameObject);
        }
    }

    void MarchFun()
    {
        if (!swap)
        {
            gameObject.transform.localScale += new Vector3(0, 1f * gameObject.transform.localScale.x, 0) * Time.deltaTime * 2;
            if (gameObject.transform.localScale.y > 1.4f * gameObject.transform.localScale.x) swap = true;
        }
        else if (swap)
        {
            gameObject.transform.localScale -= new Vector3(0, 1f * gameObject.transform.localScale.x, 0) * Time.deltaTime * 2;
            if (gameObject.transform.localScale.y < 1f * gameObject.transform.localScale.x) swap = false;
        }
    }

    void Move()
    {
        if (fortress == null)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(gameObject.transform.position.x + 6, gameObject.transform.position.y, 0), Time.deltaTime);
            Vanish();
            return;
        }

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, fortress.gateT.position, Time.deltaTime);
        if (Vector3.Distance(gameObject.transform.position, fortress.gateT.position) > 0.001f)
        {
            gameObject.transform.localScale -= new Vector3(0.15f, 0, 0) * Time.deltaTime;
        }
        else
        {
            Vanish();
        }
    }
}
