using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cars : MonoBehaviour {

    public float speed;
    //public float begspeed;

    public GameObject Explosion;
    public Sprite idDeath;
    public bool death;

    public Player pl;
    public MainCntrl mn;

    GameObject car;

    bool Dright;
    bool Drollright;
    

	void Start () {
        car = gameObject;//.GetComponent<GameObject>();

        pl = GameObject.Find("Player").GetComponent<Player>();
        mn = GameObject.Find("Main_road").GetComponent<MainCntrl>();

        death = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (!mn.pause) speed = mn.MainSpeed + 1f;//begspeed * mn.MainSpeed * 10;
        else speed = 0;

        if (!pl.death && mn.start) Move();
        else if (!death) gameObject.transform.position += new Vector3(0, mn.MainSpeed, 0) * Time.deltaTime;

        CarsAway();
        
        if(death && !pl.death) Impact();
	}

    void Impact()
    {
        if (Dright) gameObject.transform.position += new Vector3(0.02f, 0, 0) * Time.deltaTime;
        else gameObject.transform.position -= new Vector3(0.02f, 0, 0) * Time.deltaTime;

        if (Drollright) gameObject.transform.Rotate(new Vector3(0, 0, 200) * Time.deltaTime);
        else gameObject.transform.Rotate(new Vector3(0, 0, 200) * Time.deltaTime);
    }

    void Move() { car.transform.position -= new Vector3(0, speed * Time.deltaTime, 0); }

    void CarsAway()
    {
        if (gameObject.transform.position.y < -7 || gameObject.transform.position.y > 7) Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Car" && col.gameObject.transform.position.y < gameObject.transform.position.y)
        {
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "CarD") Death(Random.Range(1,4));
    }

    public void Death(int Set)
    {
        Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
        gameObject.GetComponent<Image>().sprite = idDeath;

        if (Set == 1)
        {
            Dright = true;
            Drollright = true;
        }
        else if (Set == 2)
        {
            Dright = true;
            Drollright = false;
        }
        else if (Set == 3)
        {
            Dright = false;
            Drollright = true;
        }
        else if (Set == 4)
        {
            Dright = false;
            Drollright = false;
        }

        gameObject.tag = "CarD";
        death = true;
    }
}
