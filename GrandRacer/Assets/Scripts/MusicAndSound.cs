using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicAndSound : MonoBehaviour {

    public GameObject MusicBtn;
    public GameObject SoundBtn;

    public Sprite[] musicIm;
    public Sprite[] soundIm;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetInt("Music", 1);
            PlayerPrefs.SetInt("Sound", 1);
        }

        if (PlayerPrefs.GetInt("Music") == 1) MusicBtn.GetComponent<Image>().sprite = musicIm[1];
        else MusicBtn.GetComponent<Image>().sprite = musicIm[0];

        if (PlayerPrefs.GetInt("Sound") == 1) SoundBtn.GetComponent<Image>().sprite = soundIm[1];
        else SoundBtn.GetComponent<Image>().sprite = soundIm[0];
    }
	
    public void Music()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            PlayerPrefs.SetInt("Music", 0);
            GetComponent<AudioSource>().Stop();
            MusicBtn.GetComponent<Image>().sprite = musicIm[0];
        }
        else
        {
            PlayerPrefs.SetInt("Music", 1);
            GetComponent<AudioSource>().Play();
            MusicBtn.GetComponent<Image>().sprite = musicIm[1];
        }
    }

    public void Sound()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            PlayerPrefs.SetInt("Sound", 0);
            SoundBtn.GetComponent<Image>().sprite = soundIm[0];
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            SoundBtn.GetComponent<Image>().sprite = soundIm[1];
        }
    }
}
