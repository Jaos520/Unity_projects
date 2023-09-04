using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atkSim : MonoBehaviour {

    int count;

    private void Start()
    {
        gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), -0.5f, 0);
    }

    private void Update()
    {
        if (count >= 2)
        {
            Destroy(gameObject);
        }
    }

    public void Count()
    {
        count++;
        gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), -0.5f, 0);
    }
}
