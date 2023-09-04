// using GoogleMobileAds.Api;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fortress : MonoBehaviour {

    public int lvl;
    public int FortressArmy;
    public int MaxFortressArmy;

    private Game game;

    public Text State;
    float StateAlfaColor;
    public Text lvlText;
    public Text FortressArmyText;

    float FallAlfaColor;
    public GameObject FallFortress;
    public GameObject ArmyBarBg;

    public GameObject ArmyBar;
    public GameObject NextFortressBtn;
    public GameObject FortressG;

    private int HaveWood;
    private int HaveMetal;
    private int HaveRubies;
    private int HaveExp;

    public Text HaveWoodText;
    public Text HaveMetalText;
    public Text HaveRubiesText;

    public GameObject FortressArmyG;
    //public Transform FortressArmyRespawn;

    public GameObject DmgFortressImg;

    public Image FortressImg;
    public Sprite[] skin;

    public Transform FortressArmyTextPoint;

    private bool atk;

    private bool next;

    // InterstitialAd ad;
    // AdRequest request;

    bool iState;

    public Transform gateT;

    public Vector3 fortressGone;

    public Image fortressTexture;
    public Sprite[] stoneTexture;

    public float lowArmy;
    public float highArmy;

    private void Start()
    {
        // ad = new InterstitialAd(goAD);
        // AdRequest request = new AdRequest.Builder().Build();
        // //request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("681347196925CC01").Build();
        // ad.LoadAd(request);

        FortressArmyText.gameObject.SetActive(true);
        ArmyBarBg.SetActive(true);
        State.gameObject.SetActive(false);
        // State.gameObject.transform.position = new Vector3(0, 2.5f, gameObject.transform.position.z);

        FallFortress.SetActive(false);

        game = GameObject.Find("Main Camera").GetComponent<Game>();

        gameObject.transform.position = new Vector3(-5, 0, 0);
        fortressGone = new Vector3(6, 0, 0);
        //skin = new Sprite[11];

        transform.SetSiblingIndex(2);

        FortressImg.GetComponent<Image>().sprite = skin[Random.Range(0, 10)];

        
        /*
        if (game.FortressCount <= 5)
        {
            lvl = 1;
            FortressArmy = Random.Range(100, 1000);
            HaveWood = Random.Range(0, 20);
            HaveMetal = Random.Range(0, 10);
            HaveRubies = Random.Range(0, 2);
        }
        else if (game.FortressCount <= 15)
        {
            lvl = 2;
            FortressArmy = Random.Range(1000, 10000);
            HaveWood = Random.Range(0, 20);
            HaveMetal = Random.Range(0, 10);
            HaveRubies = Random.Range(0, 2);
        }
        else if (game.FortressCount <= 35)
        {
            lvl = 3;
            FortressArmy = Random.Range(10000, 100000);
            HaveWood = Random.Range(0, 20);
            HaveMetal = Random.Range(0, 10);
            HaveRubies = Random.Range(0, 2);
        }
        else if (game.FortressCount <= 50)
        {
            lvl = 4;
            FortressArmy = Random.Range(100000, 1000000);
            HaveWood = Random.Range(0, 20);
            HaveMetal = Random.Range(0, 10);
            HaveRubies = Random.Range(0, 2);
        }
        else if (game.FortressCount > 50)
        {
            lvl = 5;
            FortressArmy = Random.Range(1000000, 10000000);
            HaveWood = Random.Range(0, 20);
            HaveMetal = Random.Range(0, 10);
            HaveRubies = Random.Range(0, 3);
        }
        */

        lvl = Random.Range(game.SiegeLvl, game.SiegeLvl + 2);
        FortressArmy = Random.Range((int)((1.1f * lvl) * 100), (int)((1.1f * lvl) * 100) + (int)(100* (1.1f * lvl)));
        HaveWood = Random.Range((int)((1.1f * lvl) * 5), (int)((1.1f * lvl) * 10) + 10);
        HaveMetal = Random.Range((int)((1.1f * lvl) * 5), (int)((1.1f * lvl) * 5) + 5);
        HaveRubies = Random.Range(0, 2);
        HaveExp = Random.Range(lvl * 10, lvl * 20);

        next = false;

        iState = true;

        MaxFortressArmy = FortressArmy;

        NextFortressBtn.SetActive(false);

        HaveWoodText.text = HaveWood.ToString("");
        HaveMetalText.text = HaveMetal.ToString("");
        HaveRubiesText.text = HaveRubies.ToString("");

        lvlText.text = "" + lvl;

        highArmy = ( MaxFortressArmy / 3 ) * 2;
        lowArmy = MaxFortressArmy / 3;
    }

    private void Update()
    {
        Atk();

        Fall();

        Click();

        StateBattleFun();

        StoneTexture();
    }

    void StoneTexture()
    {
        if (FortressArmy > highArmy)
        {
            fortressTexture.sprite = stoneTexture[0];
        }
        else if (FortressArmy > lowArmy)
        {
            fortressTexture.sprite = stoneTexture[1];
        }
        else
        {
            fortressTexture.sprite = stoneTexture[2];
        }
    }

    void StateBattleFun()
    {
        if (game.battle)
        {
            if (iState)
            {
                State.gameObject.transform.localScale += new Vector3(1f, 1f, 0) * Time.deltaTime;
            }
            else if (!iState)
            {
                State.gameObject.transform.localScale -= new Vector3(1f, 1f, 0) * Time.deltaTime;
            }
        }
    }

    public void CatapultDmg(int dmg)
    {
        Explosion();
        FortressArmy -= dmg;
    }

    public void Explosion()
    {
        DmgFortressImg.GetComponent<Animator>().Play("Explosion");
    }

    void Click()
    {
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, Vector3.one, Time.deltaTime * 20);
        if (FortressArmy > 0) State.gameObject.transform.localScale = Vector3.Lerp(State.gameObject.transform.localScale, Vector3.one, Time.deltaTime);
    }

    private void Fall()
    {
        if (NextFortressBtn.transform.localScale.y < 1)
        {
            NextFortressBtn.transform.localScale += new Vector3(1f, 1f, 0) * Time.deltaTime * 5;
        }
    }

    private void Death()
    {
        if (FortressArmy <= 0)
        {
            FortressArmyText.gameObject.SetActive(false);
            ArmyBarBg.SetActive(false);
            //if (State.gameObject.transform.position.y > 1f) State.gameObject.transform.position -= new Vector3(0, 0.1f, 0);
            //State.gameObject.transform.position = new Vector3(0, 1.8f, 0);

            game.exp += HaveExp;
            game.WeaponChance--;
            game.battle = false;
            FortressArmy = 0;
            atk = false;
            game.FortressCount++;

            State.text = "Captured";
            State.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            State.color = new Color(0,200,0,255);

            //NextFortressBtn.transform.localScale = new Vector3(0.1f, 0.1f, 1);
            //NextFortressBtn.SetActive(true);

            FallAlfaColor = 0;
            FallFortress.GetComponent<Image>().color = new Color(FallFortress.GetComponent<Image>().color.r, FallFortress.GetComponent<Image>().color.g, FallFortress.GetComponent<Image>().color.b, FallAlfaColor);
            FallFortress.SetActive(true);
        }

        
    }

    public void Next()
    {
        next = true;
        
    }

    public void NextFortress()
    {
        if (next)
        {
            // gameObject.transform.position += new Vector3(5f, 0, 0) * Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, fortressGone, Time.deltaTime * 10);
            if (gameObject.transform.position.x > 5)
            {
                // if (ad.IsLoaded()) ad.Show();
                game.Wood += HaveWood;
                game.Metal += HaveMetal;
                game.Rubies += HaveRubies;
                
                Instantiate(FortressG, new Vector3(-5, 0, 0), Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("Map").transform, false);
                Destroy(gameObject);
            }
        }
        else
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, Vector3.zero, Time.deltaTime * 10);
        }
    }

    private void Atk()
    {
        if (FallFortress.GetComponent<Image>().color.a < 1) FallAlfaColor += 0.1f * Time.deltaTime;

        State.color = new Color(State.color.r, State.color.g, State.color.b, StateAlfaColor);
        FallFortress.GetComponent<Image>().color = new Color(FallFortress.GetComponent<Image>().color.r, FallFortress.GetComponent<Image>().color.g, FallFortress.GetComponent<Image>().color.b, FallAlfaColor);

        FortressArmyText.transform.position = Vector3.Lerp(FortressArmyText.transform.position, FortressArmyTextPoint.transform.position, Time.deltaTime * 20);
        FortressArmyText.text = FortressArmy.ToString("");
        FortressArmyText.gameObject.transform.localScale = Vector3.Lerp(FortressArmyText.gameObject.transform.localScale, Vector3.one, Time.deltaTime * 5);

        ArmyBar.transform.localScale = new Vector3((float)FortressArmy / (float)MaxFortressArmy, 1, 1);
        if (atk)
        {
            if (game.ArmyStrength == 0)
            {
                // if (ad.IsLoaded()) ad.Show();

                // ad = new InterstitialAd(goAD);
                // AdRequest request = new AdRequest.Builder().Build();
                // //request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("681347196925CC01").Build();
                // ad.LoadAd(request);

                game.WeaponChance--;
                game.battle = false;
                atk = false;

                State.text = "Defeated";
                State.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                State.color = Color.red;

                StartCoroutine(Recruit());
            }
            Death();

        }
        else if (StateAlfaColor > 0)
        {
            State.transform.localScale -= new Vector3(1f, 1f, 0) * Time.deltaTime;
            StateAlfaColor -= 1f * Time.deltaTime;
        }
        NextFortress();
    }

    public void Attack()
    {
        if (FortressArmy == 0 && !atk)
        {
            NextFortressBtn.transform.localScale = new Vector3(0.1f, 0.1f, 1);
            NextFortressBtn.SetActive(true);
        }
        else if (!atk)
        {
            game.battle = true;
            gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1);

            StateAlfaColor = 1;
            State.gameObject.SetActive(true);
            State.text = "Battle";
            State.color = Color.yellow;
            atk = true;
            StartCoroutine(StateBattle());
            StartCoroutine(Battle());
            StartCoroutine(ArmyCharge());
        }
        else if (atk)
        {
            gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1);

            game.WeaponChance--;
            game.battle = false;
            atk = false;

            State.text = "Retreat";
            State.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            State.color = Color.yellow;

            StopAllCoroutines();

            StartCoroutine(Recruit());
        }
    }

    IEnumerator StateBattle()
    {
        while (game.battle)
        {
            iState = !iState;
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator Battle()
    {
        while(FortressArmy > 0 && game.ArmyStrength > 0 )//&& game.battle)
        {
            if (Random.Range(0, 100) < game.WeaponChance)
            {
                FortressArmy--;
                FortressArmyText.transform.position += new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
                FortressArmyText.transform.localScale = new Vector3(2, 2, 1); 
            }
            game.ArmyStrength--;
            if (FortressArmy <= 0)
            {
                DmgFortressImg.gameObject.transform.localScale = new Vector3(2, 2, 1);
                Explosion();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ArmyCharge()
    {
        while (atk)
        {
            Instantiate(FortressArmyG, new Vector3(0,-6,0), Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("Map").transform, false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator Recruit()
    {
        while (!game.battle && FortressArmy < MaxFortressArmy)
        {
            FortressArmy += 1;
            yield return new WaitForSeconds(1);
        }
    }
}
