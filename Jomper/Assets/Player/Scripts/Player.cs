using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Force;
    public float MaxForce = 40;

    public Vector3 mouse;

    Rigidbody2D rb;

    Animator anim;

    public bool grounded;

    public bool Dead;

    void Start()
    {
        Force = 0;
        Dead = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Move();

        Portal();

        Death();
        
    }

    void Death()
    {
        if (Camera.main.transform.position.y-transform.position.y > 5.5f)
        {
            Dead = true;
            Destroy(Camera.main.GetComponent<WorldGeneration>());
        }
    }

    void Portal()
    {
        if (transform.position.x < -3) transform.position = new Vector3(3, transform.position.y, transform.position.z);
        else if (transform.position.x > 3) transform.position = new Vector3(-3, transform.position.y, transform.position.z);
    }

    void Move()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0) && Force < MaxForce && grounded)
        {
            anim.SetInteger("State", 1);
            Force += 100f*Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var mouseDir = mousePos - gameObject.transform.position;
            mouseDir.z = 0.0f;
            mouseDir = mouseDir.normalized;

            if (mouseDir.x < 0) transform.localScale = new Vector2(-5, transform.localScale.y);
            else transform.localScale = new Vector2(5, transform.localScale.y);

            rb.AddForce(mouseDir*Force*10);
            Force = 0;
        } 
        else if (!grounded && !Input.GetMouseButton(0))
        {
            anim.SetInteger("State", 2);
        }
        else if (grounded && !Input.GetMouseButton(0))
        {
            anim.SetInteger("State", 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
            rb.velocity = new Vector3(0,0,0);
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
         if (other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
