using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rival : MonoBehaviour {

    public MainCntrl mn;
    public float speed;
    public bool boost;
    public bool left;
    public bool right;

	void Start () {
        boost = false;
        left = false;
        right = false;
        speed = mn.MainSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        if (left) gameObject.transform.position -= new Vector3(0.05f, 0, 0);

        if (right) gameObject.transform.position += new Vector3(0.05f, 0, 0);
    }
}
