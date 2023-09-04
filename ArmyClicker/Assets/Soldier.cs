using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier : MonoBehaviour
{
    public Sprite[] soldierSkin;
    Color curColor;
    Image image;
    float targetAlpha = 1.0f;

    float dirX;
    float dirY;

    void Start()
    {
        image = gameObject.GetComponent<Image>();
        curColor = gameObject.GetComponent<Image>().color;
        gameObject.GetComponent<Image>().sprite = soldierSkin[Random.Range(0, 17)];

        dirX = Random.Range(-1f, 1f);
        dirY = Random.Range(-1f, 1f);

        transform.SetSiblingIndex(0);
    }

    void Update()
    {
        //Fade
        if (curColor.a > 0.0001f) {
            curColor.a -= 1f * Time.deltaTime * 0.2f;//Mathf.Lerp(curColor.a, targetAlpha, Time.deltaTime * 5);
            image.color = curColor;
        }
        else
        {
            Destroy(gameObject);
        }

        //Move
        gameObject.transform.position += new Vector3(dirX, dirY, 0) * Time.deltaTime;
    }
}
