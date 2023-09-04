using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour {

    public Cars car;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Car")
        {
            car.speed = 0.001f;
            car.speed = 0.05f;
        }
    }

}
