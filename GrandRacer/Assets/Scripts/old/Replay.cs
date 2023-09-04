using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Replay : MonoBehaviour {

    public Text GameOverT;
    public Transform EnterPointG;
    public Transform ExitPointG;

    public GameObject ReplayBtn;
    public GameObject ReplayBtn1;

    public GameObject pl;
    public MainCntrl mn;

    public GameObject ReplayBg;
    public float AlphaColor;

    public GameObject firework;
    public Text distance;

    bool replay;

    void Start () {
        AlphaColor = 0;
        ReplayBg.SetActive(false);
        firework.SetActive(false);
        if (!PlayerPrefs.HasKey("DistanceRecord"))
        {
            PlayerPrefs.SetFloat("DistanceRecord", 0);
        }
    }
	
	void Update () {
        ReplayBg.GetComponent<Image>().color = new Color(1, 1, 1, AlphaColor);

        if (pl.GetComponent<Player>().death && AlphaColor < 1)
        {
            AlphaColor += 0.1f;
        }

        if (pl.GetComponent<Player>().death)
        {
            ReplayBg.SetActive(true);
            GameOverT.transform.position = Vector3.Lerp(GameOverT.gameObject.transform.position, EnterPointG.position, 10 * Time.deltaTime);
            if (mn.Distance > PlayerPrefs.GetFloat("DistanceRecord"))
            {
                PlayerPrefs.SetFloat("DistanceRecord", mn.Distance);
                firework.SetActive(true);
            }
        }
        else
        {
            ReplayBg.SetActive(false);
            GameOverT.gameObject.transform.position = Vector3.Lerp(GameOverT.gameObject.transform.position, ExitPointG.position, 10 * Time.deltaTime);
        }

        if (replay)
        {
            ReplayBtn.transform.localScale += new Vector3(30, 30, 0) * Time.deltaTime;
            ReplayBtn1.transform.localScale += new Vector3(30, 30, 0) * Time.deltaTime;
            if (ReplayBtn.transform.localScale.y > 30) Application.LoadLevel("Endless");
        }
	}

    public void TransitionReplayBtn()
    {
        replay = true;
    }
}
