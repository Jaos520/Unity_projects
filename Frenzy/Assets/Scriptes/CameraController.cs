using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject TargetObj;

    Vector2 TargetVector;
    GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        Camera = gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        TargetVector = TargetObj.transform.position;
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, new Vector3(TargetVector.x, TargetVector.y, -10), Time.deltaTime * 10f);
    }
}
