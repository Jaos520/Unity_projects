using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    private Rigidbody2D rb;

    public GameObject progressClimbGM;
    private GameObject climbBar;
    private Transform climbProgress;

    private float progress;

    private float side;

    public bool climb;


    private void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();

        climb = false;
    }

    private void Update()
    {
        // if (climbBar != null)
        // {
        //     climbProgress.localScale = new Vector3(progress,1,1);
        //     progress -= 0.001f;

        //     if (Input.GetMouseButtonDown(0)) progress += 0.2f;

        //     if (progress <= 0) Destroy(climbBar);
        //     else if (progress >= 1) {
        //         transform.position += new Vector3(side, 0.5f, 0);
        //         rb.bodyType = RigidbodyType2D.Dynamic;
        //         Destroy(climbBar);
        //     }
        // }

        if (climb)
        {
            transform.position -= new Vector3(0,0.001f,0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Climb")
        {
            rb.bodyType = RigidbodyType2D.Static;
            climb = true;
            // progress = 0.5f;
            if (other.transform.position.x < transform.position.x) side = -0.5f;
            else side = 0.5f;
            // climbBar = Instantiate(progressClimbGM, transform.position+ new Vector3(0,1,0), Quaternion.identity);
            // climbProgress = climbBar.transform.GetChild(0).GetComponent<Transform>();

        }
    }

    private void OnMouseDown() {
        if (climb)
        {
            transform.position += new Vector3(side, 0.5f, 0);
            climb = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Climb")
        {
            climb = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
