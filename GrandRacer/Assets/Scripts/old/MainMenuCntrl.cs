using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCntrl : MonoBehaviour {

    public Text RecordT;
    //
    public GameObject EndPlay;
    public GameObject RacePlay;
    public GameObject NameGame;

    public GameObject BackBtn;
    public GameObject ShopBtn;
    public GameObject ShopPanel;
    public RectTransform TransitionToGame;
    bool play;

    public int count;
    public Transform pointName;
    
    Camera cam;
    RectTransform ShopPanelT;
    public Vector2 shopPos;

    public bool Shop;
	
	void Start () {
        play = false;
        Shop = false;
        gameObject.transform.position = new Vector3(0, 0, 0);

        count = 1;
        EndPlay.transform.position += new Vector3(-10f,0,0);
        RacePlay.transform.position += new Vector3(-10f, 0, 0);

        NameGame.transform.position += new Vector3(0, 10, 0);

        if (PlayerPrefs.GetInt("Music") == 1) GetComponent<AudioSource>().Play();

        cam = gameObject.GetComponent<Camera>();
        
        ShopPanelT = ShopPanel.GetComponent<RectTransform>();
        // ShopPanelT.anchoredPosition = new Vector2(0, -Screen.height);
        shopPos = ShopPanelT.anchoredPosition;
        
    }
	
	void Update () {

        RecordT.text = "Record:"+PlayerPrefs.GetFloat("DistanceRecord").ToString("0")+"m";
        //
        NameGame.transform.position = Vector3.Lerp(NameGame.transform.position, pointName.position, Time.deltaTime*2);
        if (count == 1)
        {
            EndPlay.transform.position += new Vector3(20, 0, 0) * Time.deltaTime;
            if (EndPlay.transform.position.x >= 0f)
            {
                EndPlay.transform.position = new Vector3(0, EndPlay.transform.position.y, EndPlay.transform.position.z);
                count = 2;
            }
        }
        else if (count == 2)
        {
            RacePlay.transform.position += new Vector3(20, 0, 0) * Time.deltaTime;
            if (RacePlay.transform.position.x >= 0f)
            {
                RacePlay.transform.position = new Vector3(0, RacePlay.transform.position.y, RacePlay.transform.position.z);
                count = 3;
            }
        }
        //

        if (Shop)
        {
            
            ShopPanelT.anchoredPosition = Vector3.MoveTowards(ShopPanelT.anchoredPosition, Vector2.zero, Time.deltaTime * 2000);
        }
        else if (!Shop)
        {
            ShopPanelT.anchoredPosition = Vector3.MoveTowards(ShopPanelT.anchoredPosition, shopPos, Time.deltaTime * 2000);
        }

        if (play)
        {
            if(TransitionToGame.anchoredPosition.y == 0)
            {
                Application.LoadLevel("Endless");
            }
            else
            {
                TransitionToGame.anchoredPosition = Vector3.MoveTowards(TransitionToGame.anchoredPosition, Vector2.zero, Time.deltaTime * 2000);
            }
        }

        
    }

    public void ShopEnter()
    {
        if (PlayerPrefs.GetInt("Sound") == 1) ShopBtn.GetComponent<AudioSource>().Play();
        Shop = true;
    }
    public void ShopExit()
    {
        if (PlayerPrefs.GetInt("Sound") == 1) BackBtn.GetComponent<AudioSource>().Play();
        Shop = false;
    }

    public void TransitionToGameBtn()
    {
        if (PlayerPrefs.GetInt("Sound") == 1) EndPlay.GetComponent<AudioSource>().Play();
        play = true;
    }
}
