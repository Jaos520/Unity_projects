using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Game : MonoBehaviour {

    public int SiegeLvl;
    public int exp;
    public Text expBarT;
    public Text SiegeLvlText;
    public GameObject expBar;

    public int FortressCount;
    public int ArmyStrength;

    public int Wood;
    public int Metal;
    public int Rubies;

    private int RecruitPower;
    public int WeaponChance;
    private int Workers;
    public int Catapult;


    public Text CatapultT;
    public Text CatapultT1;
    public Text WeaponChanceT;
    public Text WeaponChanceT1;
    public GameObject WeaponChanceInd;

    public Text FortressCountText;
    public Text ArmyStrengthText;
    public Text WoodText;
    public Text MetalText;
    public Text RubiesText;
    public Text RecruitPowerText;
    public Text WorkersText;
    

    public Button recruitpowerBtn;
    public Button weaponBtn;
    public Button catapultBtn;
    public GameObject demandPanelCatapult;
    public Button workerBtn;
    public GameObject demandPanelWorker;
    public Button feastBtn;
    
    public float CatapultPriceArmyStrength;
    public float WorkerPriceRubies;
    public float RecruitPowerPriceArmyStrength;
    public Text CatapultPriceArmyStrengthText;
    public Text WorkerPriceRubiesText;
    public Text RecruitPowerPriceArmyStrengthText;
    public Text WeaponPriceMetal;
    public Text CatapultPriceWood;

    public GameObject MainClickBtn;
    public GameObject MainPanel;
    public GameObject MapPanel;
    public GameObject ShopPanel;
    public bool map;
    public bool shop;

    public GameObject RecruitPanel;
    public GameObject BattlePanel;
    string specShop;

    public GameObject FeastG;
    public GameObject FeastInd;
    int feast;
    int i;
    int iFeast;
    bool swapFeast;

    public bool battle;

    public GameObject CatapultBall;
    public GameObject CatapultAbilityBtn;
    public GameObject CatapultAbilityInd;
    bool CtAbCd;

    private Save sv = new Save();

    public RectTransform MainPanelRT;
    public GameObject mainCanvas;
    public float canvasHeight;

    public Vector3 shopPosMainPanel;
    public Vector3 zeroPosMainPanel;

    public RectTransform shopBtnRT;
    public RectTransform siegeBtnRT;

    public Vector3 shopPosShopBtn;
    public Vector3 zeroPosShopBtn;

    public Vector3 siegePosSiegeBtn;
    public Vector3 zeroPosSiegeBtn;

    public Vector3 zeroPanelPos;
    public Vector3 gonePanelPos;

    public Vector3 curPanelPosRecruit;
    public Vector3 curPanelPosBattle;

    public GameObject Soldier;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SV"))
        {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));

            ArmyStrength = sv.ArmyStrength;

            WeaponChance = sv.WeaponChance;
            Catapult = sv.Catapult;

            RecruitPower = sv.RecruitPower;
            Workers = sv.Workers;

            Wood = sv.Wood;
            Metal = sv.Metal;
            Rubies = sv.Rubies;

            CatapultPriceArmyStrength = sv.CatapultPrice;
            RecruitPowerPriceArmyStrength = sv.RecruitPowerPrice;
            WorkerPriceRubies = sv.WorkerPrice;

            FortressCount = sv.FortressCount;
            exp = sv.exp;
            SiegeLvl = sv.SiegeLvl;
        }
        else
        {
            ArmyStrength = 0;

            WeaponChance = 90;
            Catapult = 0;

            RecruitPower = 1;
            Workers = 0;

            Wood = 0;
            Metal = 0;
            Rubies = 0;

            CatapultPriceArmyStrength = 250;
            RecruitPowerPriceArmyStrength = 50;
            WorkerPriceRubies = 1;

            FortressCount = 0;
            exp = 0;
            SiegeLvl = 1;
        }
        
    }

    void Start () {

        //SiegeLvl = 1;
        specShop = "recruit";
        battle = false;

        map = false;
        shop = false;

        StartCoroutine(WorkersRecruit());
        
        feast = 1;
        iFeast = 15;
        swapFeast = false;
        
        /*
        SiegeLvl = 1;
        exp = 0;
        ArmyStrength = 1000;
        Wood = 1000;
        Metal = 1000;
        Rubies = 1000;

        WeaponChance = 90;
        RecruitPower = 1;
        */

        MainPanelRT = MainPanel.GetComponent<RectTransform>();
        canvasHeight = mainCanvas.GetComponent<RectTransform>().rect.height;

        shopPosMainPanel = new Vector3( MainPanelRT.localPosition.x, canvasHeight, MainPanelRT.localPosition.z);
        zeroPosMainPanel = new Vector3( MainPanelRT.localPosition.x, 0, MainPanelRT.localPosition.z);
        
        shopPosShopBtn = new Vector3( shopBtnRT.localPosition.x, shopBtnRT.localPosition.y - shopBtnRT.rect.height, shopBtnRT.localPosition.z);
        siegePosSiegeBtn = new Vector3( siegeBtnRT.localPosition.x, siegeBtnRT.localPosition.y + siegeBtnRT.rect.height, siegeBtnRT.localPosition.z);
        zeroPosShopBtn = shopBtnRT.localPosition;
        zeroPosSiegeBtn = siegeBtnRT.localPosition;

        gonePanelPos = new Vector3(BattlePanel.transform.position.x, 0, 0);
        zeroPanelPos = RecruitPanel.transform.position;

        curPanelPosRecruit = RecruitPanel.transform.position;
        curPanelPosBattle = BattlePanel.transform.position;
    }

    private void Update()
    {
        ExpSystem();

        ArmyStrengthText.text = ArmyStrength.ToString("");

        FortressCountText.text = FortressCount.ToString("");

        WoodText.text = Wood.ToString("");
        MetalText.text = Metal.ToString("");
        RubiesText.text = Rubies.ToString("");

        CatapultT.text = "Lvl "+ Catapult;
        CatapultT1.text = "Lvl " + Catapult;
        WeaponChanceT.text = WeaponChance + "%";
        WeaponChanceT1.text = WeaponChance + "%";
        WeaponChanceInd.GetComponent<Image>().fillAmount = (WeaponChance * 0.01f);

        RecruitPowerText.text = RecruitPower.ToString("");
        WorkersText.text = Workers.ToString("");

        CatapultPriceArmyStrengthText.text = CatapultPriceArmyStrength.ToString("0");
        CatapultPriceWood.text = (20 * SiegeLvl).ToString("0");
        RecruitPowerPriceArmyStrengthText.text = RecruitPowerPriceArmyStrength.ToString("0");
        WorkerPriceRubiesText.text = WorkerPriceRubies.ToString("0");
        WeaponPriceMetal.text = (20 * SiegeLvl).ToString("0");

        ModeSwitch();

        ShopSpecSwitch();

        ArmyStrengthEffect();
        MainClickBtnEffect();

        DissableBuyBtns();
        BuyBtnsEffect();

        FeastFun();

        CatapultAbility();
    }

    void ShopSpecSwitch()
    {
        if (specShop == "recruit")
        {
            RecruitPanel.transform.position = Vector3.MoveTowards(RecruitPanel.transform.position, curPanelPosRecruit, Time.deltaTime * 20);
            BattlePanel.transform.position = Vector3.MoveTowards(BattlePanel.transform.position, curPanelPosBattle, Time.deltaTime * 20);
            // BattlePanel.transform.position += new Vector3(0.5f, 0, 0);
        }
        if (specShop == "battle") 
        {
            RecruitPanel.transform.position = Vector3.MoveTowards(RecruitPanel.transform.position, curPanelPosRecruit - gonePanelPos, Time.deltaTime * 20);
            BattlePanel.transform.position = Vector3.MoveTowards(BattlePanel.transform.position, curPanelPosBattle - gonePanelPos, Time.deltaTime * 20);
            // RecruitPanel.transform.position -= new Vector3(0.5f, 0, 0);
            // BattlePanel.transform.position -= new Vector3(0.5f, 0, 0);
        }
    }
    public void Left()
    {
        specShop = "recruit";
    }
    public void Right()
    {
        specShop = "battle";
    }

    void ExpSystem()
    {
        expBar.transform.localScale = new Vector3((float)exp / (((float)SiegeLvl * 0.15f) * 1000), 1, 1);
        expBarT.text = exp + "/" + (SiegeLvl * 0.15f) * 1000;
        SiegeLvlText.text = SiegeLvl.ToString("");
        if (exp >= (SiegeLvl * 0.15f) * 1000)
        {
            exp -= (int)((SiegeLvl * 0.15f) * 1000);
            SiegeLvl++;
        }
    }

    void CatapultAbility()
    {
        if (Catapult == 0) CatapultAbilityBtn.SetActive(false);
        else if (battle) CatapultAbilityBtn.SetActive(true);
        else CatapultAbilityBtn.SetActive(false);

        if (CtAbCd) CatapultAbilityBtn.GetComponent<Button>().interactable = false;
        else CatapultAbilityBtn.GetComponent<Button>().interactable = true;
    }
    public void CatapultAtk()
    {
        int count = 0;

        CtAbCd = true;
        CatapultAbilityInd.GetComponent<Image>().fillAmount = 0;
        CatapultAbilityInd.SetActive(true);
        StartCoroutine(CtCd(count));
        Instantiate(CatapultBall, new Vector3(5,3,0), Quaternion.Euler(0,0,30)).transform.SetParent(GameObject.FindGameObjectWithTag("Map").transform, false);
    }
    IEnumerator CtCd(int count)
    {
        while (count != 10)
        {
            count++;
            CatapultAbilityInd.GetComponent<Image>().fillAmount = 1 - ((float)count / (float)11);
            yield return new WaitForSeconds(1);
        }
        CatapultAbilityInd.SetActive(false);
        CtAbCd = false;
    }

    void FeastFun()
    {
        if (feast >= 2)
        {
            FeastG.SetActive(true);

            if (!swapFeast)
            {
                FeastG.transform.localScale += new Vector3(1, 1, 0) * Time.deltaTime;
                if (FeastG.transform.localScale.x > 1) swapFeast = true;
            }
            else if (swapFeast)
            {
                FeastG.transform.localScale -= new Vector3(1, 1, 0) * Time.deltaTime;
                if (FeastG.transform.localScale.x < 0.7f) swapFeast = false;
            }
            
        }
        else if (feast < 2) FeastG.SetActive(false);

 
    }

    private void BuyBtnsEffect()
    {
        if (recruitpowerBtn.gameObject.transform.localScale.y < 1)
        {
            recruitpowerBtn.gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0);
        }
        if (weaponBtn.gameObject.transform.localScale.y < 1)
        {
            weaponBtn.gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0);
        }
        if (catapultBtn.gameObject.transform.localScale.y < 1)
        {
            catapultBtn.gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0);
        }
        if (workerBtn.gameObject.transform.localScale.y < 1)
        {
            workerBtn.gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0);
        }
        if (feastBtn.gameObject.transform.localScale.y < 1)
        {
            feastBtn.gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0);
        }
    }

    private void DissableBuyBtns()
    {
        if (ArmyStrength >= RecruitPowerPriceArmyStrength)
        {
            recruitpowerBtn.interactable = true;
        }
        else
        {
            recruitpowerBtn.interactable = false;
        }

        if (ArmyStrength >= 100 && Metal >= (20 * SiegeLvl) && WeaponChance <= 100)
        {
            weaponBtn.interactable = true;
        }
        else
        {
            weaponBtn.interactable = false;
        }

        if (ArmyStrength >= CatapultPriceArmyStrength && Wood >= (20*SiegeLvl) && SiegeLvl >= 5)
        {
            demandPanelCatapult.SetActive(false);
            catapultBtn.interactable = true;
        }
        else if (SiegeLvl >= 5)
        {
            demandPanelCatapult.SetActive(false);
            catapultBtn.interactable = false;
        }
        else
        {
            demandPanelCatapult.SetActive(true);
            catapultBtn.interactable = false;
        }

        if (Rubies >= WorkerPriceRubies && SiegeLvl >= 2)
        {
            demandPanelWorker.SetActive(false);
            workerBtn.interactable = true;
        }
        else if (SiegeLvl >= 2)
        {
            demandPanelWorker.SetActive(false);
            workerBtn.interactable = false;
        }
        else
        {
            demandPanelWorker.SetActive(true);
            workerBtn.interactable = false;
        }

        if (Rubies >= 1 && Wood >= 5 && Metal >= 5)
        {
            feastBtn.interactable = true;
        }
        else
        {
            feastBtn.interactable = false;
        }
    }

    private void MainClickBtnEffect()
    {
        if (MainClickBtn.transform.localScale.y < 1)
        {
            MainClickBtn.transform.localScale += new Vector3(1f, 1f, 0) * Time.deltaTime;
        }
    }

    private void ArmyStrengthEffect()
    {
        if (ArmyStrengthText.gameObject.transform.localScale.y > 1)
        {
            ArmyStrengthText.gameObject.transform.localScale -= new Vector3(1f, 1f, 0) * Time.deltaTime;
        }
    }

    private void ModeSwitch()
    {
        if (shop && !map)
        {
            MainPanelRT.localPosition = Vector3.MoveTowards(MainPanelRT.localPosition, shopPosMainPanel, Time.deltaTime * 2000);
            shopBtnRT.localPosition = Vector3.MoveTowards(shopBtnRT.localPosition, shopPosShopBtn, Time.deltaTime * 2000);
        }
        else if (!shop && map)
        {
            MainPanelRT.localPosition = Vector3.MoveTowards(MainPanelRT.localPosition, -shopPosMainPanel, Time.deltaTime * 2000);
            siegeBtnRT.localPosition = Vector3.MoveTowards(siegeBtnRT.localPosition, siegePosSiegeBtn, Time.deltaTime * 2000);
        }
        else if (!shop && !map)
        {
            MainPanelRT.localPosition = Vector3.MoveTowards(MainPanelRT.localPosition, zeroPosMainPanel, Time.deltaTime * 2000);
            shopBtnRT.localPosition = Vector3.MoveTowards(shopBtnRT.localPosition, zeroPosShopBtn, Time.deltaTime * 2000);
            siegeBtnRT.localPosition = Vector3.MoveTowards(siegeBtnRT.localPosition, zeroPosSiegeBtn, Time.deltaTime * 2000);
        }
        
    }

    public void Map()
    {
        if (!map)
        {
            map = true;
            MapPanel.SetActive(true);
            ShopPanel.SetActive(false);
        }
        else if (map && !battle)
        {
            map = false;
        }
    }
    public void Shop()
    {
        if (!shop)
        {
            shop = true;
            ShopPanel.SetActive(true);
            MapPanel.SetActive(false);
        }
        else if (shop)
        {
            shop = false;
        }
    }


    public void BuyRecruitPower()
    {
        recruitpowerBtn.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        ArmyStrength -= (int)RecruitPowerPriceArmyStrength;
        RecruitPower++;
        RecruitPowerPriceArmyStrength *= 1.8f;
    }
    public void BuyWeapon()
    {
        weaponBtn.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        ArmyStrength -= 100;
        Metal -= (20 * SiegeLvl);
        WeaponChance++;
    }
    public void BuyCatapult()
    {
        catapultBtn.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        ArmyStrength -= (int)CatapultPriceArmyStrength;
        CatapultPriceArmyStrength *= 1.1f;
        Wood -= (20 * SiegeLvl);
        Catapult++;
    }
    public void BuyWorker()
    {
        workerBtn.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        Rubies -= (int)WorkerPriceRubies;
        Workers++;
        WorkerPriceRubies += 1;
    }
    public void BuyFeast()
    {
        feastBtn.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        Wood -= 10;
        Metal -= 10;
        Rubies--;
        feast = 2;
        FeastInd.GetComponent<Image>().fillAmount = 1.1f;
        i = 0;
        StartCoroutine(Feast());
        shop = false;
    }

    IEnumerator Feast()
    {
        while (feast >= 2)
        {
            if (i == 10) feast = 1;
            i++;
            FeastInd.GetComponent<Image>().fillAmount -= 0.1f;
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator WorkersRecruit()
    {
        while (true)
        {
            if (Workers > 0)
            {
                ArmyStrengthText.gameObject.transform.localScale = new Vector3(2, 2, 1);
                ArmyStrength += (Workers*feast);


            }
            yield return new WaitForSeconds(5);
        }
    }

    public void OnClick()
    {
        ArmyStrength += (RecruitPower*feast);

        Vector3 mousePos = Input.mousePosition;
        int i = 0;

        // do
        // {
        Instantiate(Soldier, Vector3.zero, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("Main").transform, false);
            
        // }
        // while(i < RecruitPower*feast);

        ArmyStrengthText.gameObject.transform.localScale = new Vector3(2, 2, 1);
        MainClickBtn.transform.localScale = new Vector3(0.9f, 0.9f, 1);
    }
#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            sv.ArmyStrength = ArmyStrength;
            sv.WeaponChance = WeaponChance;
            sv.RecruitPower = RecruitPower;
            sv.Workers = Workers;

            sv.Wood = Wood;
            sv.Metal = Metal;
            sv.Rubies = Rubies;

            sv.CatapultPrice = CatapultPriceArmyStrength;
            sv.RecruitPowerPrice = RecruitPowerPriceArmyStrength;
            sv.WorkerPrice = WorkerPriceRubies;

            sv.FortressCount = FortressCount;
            sv.exp = exp;
            sv.SiegeLvl = SiegeLvl;

            PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
        }
    }
#else
    private void OnApplicationQuit()
    {
        sv.ArmyStrength = ArmyStrength;
        sv.WeaponChance = WeaponChance;
        sv.RecruitPower = RecruitPower;
        sv.Workers = Workers;

        sv.Wood = Wood;
        sv.Metal = Metal;
        sv.Rubies = Rubies;

        sv.CatapultPrice = CatapultPriceArmyStrength;
        sv.RecruitPowerPrice = RecruitPowerPriceArmyStrength;
        sv.WorkerPrice = WorkerPriceRubies;

        sv.FortressCount = FortressCount;
        sv.exp = exp;
        sv.SiegeLvl = SiegeLvl;

        PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
    }
#endif


}


[Serializable]
public class Save
{
    public int ArmyStrength;

    public int WeaponChance;
    public int Catapult;

    public int RecruitPower;
    public int Workers;

    public int Wood;
    public int Metal;
    public int Rubies;

    public float CatapultPrice;
    public float RecruitPowerPrice;
    public float WorkerPrice;

    public int FortressCount;
    public int exp;
    public int SiegeLvl;
}
