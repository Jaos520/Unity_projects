using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Transform pointUp;
    public Transform pointMid;
    public Transform pointDown;
    public GameObject transitionGM;

    public bool End = false;
    public bool Begin = false;

    public string sceneName;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Playground")
        {
            Begin = true;
        }
    }

    void Update()
    {
        Follow();
        Ending();
        Beginning();
    }

    void Follow()
    {
        transform.position = new Vector2(transform.position.x, Camera.main.transform.position.y);
    }

    void Ending()
    {
        if (End)
        {
            transitionGM.transform.position = Vector3.MoveTowards(transitionGM.transform.position, pointMid.position, Time.deltaTime*15);
            if (transitionGM.transform.position == pointMid.position)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    void Beginning()
    {
        if (Begin)
        {
            transitionGM.transform.position = Vector3.MoveTowards(transitionGM.transform.position, pointUp.position, Time.deltaTime*15);
            if (transitionGM.transform.position == pointUp.position)
            {
                transitionGM.transform.position = pointDown.position;
                Begin = false;
            }
        }
    }

    public void Play()
    {
        sceneName = "Playground";
        End = true;
    }

    public void Menu()
    {
        sceneName = "Menu";
        End = true;
    }
}
