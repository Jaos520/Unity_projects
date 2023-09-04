using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoveMenu : MonoBehaviour
{
    public float speed;

    string side = "left";

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (transform.position.x < -2.50f) side = "right";
        else if (transform.position.x > 2.50f) side = "left";

        if (side == "left") transform.Translate(Vector2.left * Time.deltaTime * speed);
        else if (side == "right") transform.Translate(Vector2.right * Time.deltaTime * speed);
    }
}
