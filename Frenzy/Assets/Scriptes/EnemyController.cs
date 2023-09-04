using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        StartCoroutine(Movement());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        
    }

    public void Damage()
    {

    }

    IEnumerator Movement()
    {
        while (true)
        {
            if (player.transform.position.x < transform.position.x) rb.AddForce(new Vector3(-0.1f,1,0)*500);
            else rb.AddForce(new Vector3(0.1f, 1, 0) * 500);
            yield return new WaitForSeconds(3);
        }
    }
}
