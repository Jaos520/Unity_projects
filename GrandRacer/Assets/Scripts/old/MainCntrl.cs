using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using GoogleMobileAds.Api;


public class MainCntrl : MonoBehaviour {

    public bool pause;
    public GameObject pausePanel;
    float saveMainSpeed;

    public string mode;

    public float Distance;
    public float AllDistanceWant;
    public Text DistanceT;
    public Text DistanceTgm;
    public GameObject DistanceG;

    public Text SpeedT;

    //public GameObject cntrlButtons;

    public float MainSpeed;
    GameObject MainRoad;

    public bool start;

    public GameObject mncamG;
    Camera cam;
    public Player pl;
    public Text Hp;
    public GameObject HpPlayer;
    public GameObject FuelBar;

    public bool Bonusrun;
    public GameObject BonusG;

    public int takeMoney;
    public int MoneyCash;
    public Text MoneyCashT;
    public Text takeMoneyT;

    public int getmoney;
    public int savemoney;

    public float ForceSpeed;

    //
    public int StartCountdown;
    public Text StartT;

    //

    public bool bonusDoubleCoin;
    int a;
    
    public RectTransform Transition;
    Vector2 transitionDown;
    bool ToMenu;

    // InterstitialAd ad;
    //AdRequest request;

    void Start () {

        //Реклама
        // ad = null;
        // if (!PlayerPrefs.HasKey("adCount")) PlayerPrefs.SetInt("adCount", 0);
        // else if (PlayerPrefs.GetInt("adCount") < 3) PlayerPrefs.SetInt("adCount", PlayerPrefs.GetInt("adCount") + 1);
        // else if (PlayerPrefs.GetInt("adCount") == 3)
        // {
        //     ad = new InterstitialAd(goAD);
        //     AdRequest request = new AdRequest.Builder().Build();
        //     //request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("681347196925CC01").Build();
        //     ad.LoadAd(request);
        //     PlayerPrefs.SetInt("adCount", 0);
        // }
        

        //Инициализация на старте
        MainSpeed = 5f;
        takeMoney = 0;
        Distance = 0;
        SpeedT.text = (MainSpeed * 20).ToString("0");

        MainRoad = gameObject;
        bonusDoubleCoin = false;
        start = false;
        ToMenu = false;
        Transition.gameObject.SetActive(true);
        transitionDown = Transition.anchoredPosition;
        Transition.anchoredPosition = Vector2.zero;

        BonusG.SetActive(false);
        Bonusrun = false;

        pause = false;
        pausePanel.SetActive(false);
        //gameObject.transform.position = new Vector3(0, 0, 0);

        ForceSpeed = 10;

        StartCountdown = 3;
        StartT.gameObject.transform.localScale = new Vector3(1, 1, 1);
        StartT.gameObject.SetActive(true);
        HpPlayer.SetActive(false);
        //cntrlButtons.SetActive(false);

        //Начальная "кат-сцена"
        pl.gameObject.transform.position = new Vector3(0, -5f, 0);
        cam = mncamG.GetComponent<Camera>();
        cam.orthographicSize = 1;
        mncamG.transform.position = new Vector3(0, -5, -10);

        //Музыка
        if (PlayerPrefs.GetInt("Music") == 1) GetComponent<AudioSource>().Play();
    }
	
	void Update () {

        //Бонус
        if (bonusDoubleCoin)
        {
            BonusG.SetActive(true);
            StartCoroutine(BonusRun());
            bonusDoubleCoin = false;
            a = 0;
        }
        if (BonusG.transform.localScale.y >= 1.3f) a = 1;
        else if (BonusG.transform.localScale.y <= 1) a = 0;

        if (Bonusrun && BonusG.transform.localScale.y < 1.3f && a == 0) BonusG.transform.localScale += new Vector3(0.01f, 0.01f, 0);
        else if (Bonusrun && BonusG.transform.localScale.y > 1 && a == 1) BonusG.transform.localScale -= new Vector3(0.01f, 0.01f, 0);

        //Музыка

        if (GetComponent<AudioSource>().volume < 0.2f) GetComponent<AudioSource>().volume += 0.001f;


        //Начальная "кат-сцена"

        if (!ToMenu) Transition.anchoredPosition = Vector3.MoveTowards(Transition.anchoredPosition, transitionDown, Time.deltaTime * 2000);
        else if (ToMenu) Transition.anchoredPosition = Vector3.MoveTowards(Transition.anchoredPosition, Vector2.zero, Time.deltaTime * 2000);
        if (Transition.anchoredPosition.y == 0 && ToMenu)
        {
            Application.LoadLevel("Menu");
        }

        if (!start)
        {
            if (pl.gameObject.transform.position.y < -2.25) pl.gameObject.transform.position += new Vector3(0, 0.91f, 0) * Time.deltaTime;
            if (cam.orthographicSize < 5)
            {
                cam.orthographicSize += 1.33f * Time.deltaTime;
                mncamG.transform.position += new Vector3(0, 1.66f, 0) * Time.deltaTime;
            }
            else if (cam.orthographicSize > 5)
            {
                cam.orthographicSize = 5;
                mncamG.transform.position = new Vector3(0, 0, -10);
            }
        }

        //Ускорение
        if (pl.boost)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(0, 0, -10), Time.deltaTime * 10);
            cam.transform.position += new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0) * Time.deltaTime;

            if (pl.transform.position.y < 0) pl.transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
            if (cam.orthographicSize > 4) cam.orthographicSize -= 1 * Time.deltaTime;
            if (MainSpeed*20 < pl.Speed) MainSpeed += pl.Speed*0.005f * Time.deltaTime;
        }
        else if (start && !pause)
        {
            if (cam.orthographicSize < 5)
            { 
                cam.orthographicSize += 1 * Time.deltaTime;
                cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(0, 0, -10), Time.deltaTime * 10);
            }
            else if (cam.orthographicSize >= 5) { cam.orthographicSize = 5; cam.transform.position = new Vector3(0, 0, -10); }
            if (pl.transform.position.y > -2.25) pl.transform.position -= new Vector3(0, 1, 0) * Time.deltaTime;

            if (MainSpeed > 5)
            {
                MainSpeed -= 0.5f * Time.deltaTime;
            }
            else if (MainSpeed <= 5) MainSpeed = 5;
        }

        //Стартовый отсчёт
        if (StartCountdown == 3)
        {
            StartT.gameObject.transform.localScale -= new Vector3(1, 1, 0) * Time.deltaTime;
            if (StartT.gameObject.transform.localScale.y < 0.01f)
            {
                StartCountdown = 2;
                StartT.text = StartCountdown.ToString("0");
                StartT.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (StartCountdown == 2)
        {
            StartT.gameObject.transform.localScale -= new Vector3(1, 1, 0) * Time.deltaTime;
            if (StartT.gameObject.transform.localScale.y < 0.01f)
            {
                StartCountdown = 1;
                StartT.text = StartCountdown.ToString("0");
                StartT.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (StartCountdown == 1)
        {
            StartT.gameObject.transform.localScale -= new Vector3(1, 1, 0) * Time.deltaTime;
            if (StartT.gameObject.transform.localScale.y < 0.01f)
            {
                StartCountdown = 0;
                StartT.text = StartCountdown.ToString("0");
                StartT.gameObject.transform.localScale = new Vector3(1, 1, 1);
                HpPlayer.SetActive(true);
                //cntrlButtons.SetActive(true);
                start = true;
                Destroy(StartT.gameObject);
            }
        }

        //Game over
        if (pl.death)
        {
            HpPlayer.SetActive(false);
        }
        if (!start)
        {
            pl.left = false;
            pl.right = false;
            //cntrlButtons.SetActive(false);
        }
        else if (start)
        {
            //cntrlButtons.SetActive(true);
        }

        if (Distance <= 0)
        {
            Distance = 0;
        }
        DistanceT.text = Distance.ToString("0")+" m";
        DistanceTgm.text = Distance.ToString("0") + " m";
        MoneyCashT.text = MoneyCash.ToString("0") + " $";

        takeMoneyT.text = "$ " + takeMoney.ToString();
        if (takeMoneyT.transform.localScale.x > 1) takeMoneyT.transform.localScale -= new Vector3(1, 1, 0) * Time.deltaTime;

        if (pl.death && start && mode == "Endless")
        {

            // if (ad != null) if (ad.IsLoaded()) ad.Show();

            //savemoney = takeMoney;//Distance / 10;
            getmoney = takeMoney + PlayerPrefs.GetInt("Money");
            PlayerPrefs.SetInt("Money", getmoney);
            PlayerPrefs.Save();
            start = false;
            DistanceG.SetActive(false);
            StartCoroutine(CashMoney());
        }

        if (mode == "Endless" && !pl.death)
        {
            if (start)
            {
                SpeedT.text = (MainSpeed*20).ToString("0");
                Distance += MainSpeed * Time.deltaTime;
                pl.fuel -= MainSpeed / 100f * Time.deltaTime;
            }
            MainRoad.transform.position -= new Vector3(0, MainSpeed, 0) * Time.deltaTime;
        }

        if (gameObject.transform.position.y <= -10)
        {
            MainRoad.transform.position = new Vector3(0, 0, 0);
        }

        //Пауза
        if (pause)
        {
            MainSpeed = 0;
        }
	}

    IEnumerator BonusRun()
    {
        Bonusrun = true;
        int sec = 20;
        while (Bonusrun)
        {
            sec--;
            BonusG.GetComponent<Image>().fillAmount = (float)sec / 20.0f;
            if (sec == 0)
            {
                Bonusrun = false;
                BonusG.SetActive(false);
            }
            yield return new WaitForSeconds(1);
        }
        
    }

    IEnumerator CashMoney()
    {
        float time = 0.1f;
        while (MoneyCash < takeMoney)
        {
            time *= 0.01f;
            MoneyCash += 1;
            yield return new WaitForSeconds(time);
        }
        MoneyCashT.transform.localScale += new Vector3(0.5f, 0.5f, 0);
    }

    public void MenuBtn()
    {
        if (!pause)
        {
            pause = true;
            pausePanel.SetActive(true);
            saveMainSpeed = MainSpeed;
        }
        else
        {
            pause = false;
            pausePanel.SetActive(false);
            MainSpeed = saveMainSpeed;
        }
        
    }

    public void ExitBtn()
    {
        // Transition.position = new Vector3(0, 10, 0);
        Transition.anchoredPosition = -transitionDown;
        ToMenu = true;
    }
}
