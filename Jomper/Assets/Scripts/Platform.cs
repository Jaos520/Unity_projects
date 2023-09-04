using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    void Update()
    {
        if (Camera.main.transform.position.y-transform.position.y > 10) Destroy(gameObject);
    }
}
