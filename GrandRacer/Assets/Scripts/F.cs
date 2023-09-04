using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F : MonoBehaviour {

    public Rival r;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Car" && gameObject.transform.position.x > 1) { r.left = true; r.boost = false; }
        else if (col.gameObject.tag == "Car" && gameObject.transform.position.x < -1) { r.right = true; r.boost = false; }
        else if (col.gameObject.tag == "Car") { r.right = true; r.boost = false; }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Car" && gameObject.transform.position.x > 1) { r.left = true; r.boost = false; }
        else if (col.gameObject.tag == "Car" && gameObject.transform.position.x < -1) { r.right = true; r.boost = false; }
        else if (col.gameObject.tag == "Car") { r.right = true; r.boost = false; }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Car")
        {
            r.left = false;
            r.right = false;
            r.boost = true;
        }
    }
}
