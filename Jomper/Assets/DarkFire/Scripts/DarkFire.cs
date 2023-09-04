using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkFire : MonoBehaviour
{

    public Player player;

    public WorldGeneration worldGeneration;

    public GameObject ResetBtn;
    public GameObject MenuBtn;

    bool CreateComplite = false;

    void Update()
    {
        Begin();

        End();
    
    }

    void End()
    {
        if (player.Dead && transform.position.y-Camera.main.transform.position.y < 10)
        {
            transform.position += new Vector3(0, 15, 0) * Time.deltaTime;
        }
        else if (player.Dead && !CreateComplite)
        {
            ResetBtn.SetActive(true);
            MenuBtn.SetActive(true);
            StartCoroutine(HelpFunctions.AppearanceDisappear(2, ResetBtn.GetComponent<Image>(), true));
            StartCoroutine(HelpFunctions.AppearanceDisappear(2, MenuBtn.GetComponent<Image>(), true));

            CreateComplite = true;
        }
    }

    void Begin()
    {
        if (worldGeneration.start && transform.position.y-Camera.main.transform.position.y < -3)
        {
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
        }
    }
}
