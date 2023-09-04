using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldGeneration : MonoBehaviour
{
    public Text score;
    public GameObject player;

    public GameObject platformsGroup;

    public List<GameObject> platforms = new List<GameObject>();
    public float height;

    public float betweenPlatformHeight = 2;
    public float betweenVoidHeight = 9;
    public float currentLevelHeight;
    public float x;
    public bool side;

    public bool start;
    void Start()
    {
        height = transform.position.y;
        currentLevelHeight = -3;
        side = true;
        start = false;
    }

    void Update()
    {
        height = transform.position.y;
        score.text = ((int)height).ToString()+"M";
        
        if (player.transform.position.y > height+2) {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,player.transform.position.y,transform.position.z), Time.deltaTime);
            start = true;
        }
        else if (start) transform.position += new Vector3(0,1,0) * Time.deltaTime;

        if (currentLevelHeight < betweenVoidHeight+height)
        {
            if (side) x = 1.8f;
            else x = -1.8f;

            Instantiate(platforms[Random.Range(0,platforms.Count)], new Vector3(x,currentLevelHeight,0), Quaternion.identity, platformsGroup.transform);
            side = !side;

            currentLevelHeight += betweenPlatformHeight;
        }
    }

}
