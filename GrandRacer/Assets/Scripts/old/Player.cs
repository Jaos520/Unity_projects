using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float TESTWheelAngel;

    public Camera cm;

    public MainCntrl mn;
    public Replay rp;

    GameObject player;
    public GameObject Explosion;
    public Sprite idDeath;
    int Pos;
    bool bbtn;

    public Transform IndBoost;
    public Transform Pedal;
    public bool boost;
    bool eff;
    float effpos;

    public float Maxfuel;
    public float fuel;
    public GameObject FuelBar;
    public Text FuelText;

    public float MaxHealth;
    public GameObject HealthBar;
    public float Health;
    public Text HpText;

    public Text textDamage;

    public float Speed;

    public float ShootDmg;

    public bool death;

    public bool left;
    public bool right;
    bool block;

    public GameObject wheel;
    public float rot;
    public float dirSpeed;
    bool wheelCntrl;

    public bool ChangeColor;
    
    public Sprite[] skin;

    public bool take;
    int i;

    public GameObject TESTg;
    public float TEST;
    //Ray ray;

    bool dmgRun;


    void Start () {
        dmgRun = false;

        player = gameObject;

        MaxHealth = PlayerPrefs.GetInt("Hp");
        Maxfuel = PlayerPrefs.GetFloat("Fuel");

        Speed = PlayerPrefs.GetFloat("Speed");

        fuel = Maxfuel;
        Health = MaxHealth;

        take = false;
        block = false;

        left = false;
        right = false;
        
        death = false;
        ChangeColor = false;
        
        if (!PlayerPrefs.HasKey("Skin"))
        {
            PlayerPrefs.SetInt("Skin", 0);
        }
        player.GetComponent<Image>().sprite = skin[PlayerPrefs.GetInt("Skin")];

        HealthBar.transform.localScale = new Vector3(Health / MaxHealth, 1, 1);

        if (PlayerPrefs.GetInt("Sound") == 1) GetComponent<AudioSource>().Play();

        bbtn = false;
    }

    void Update() {

        bool leftArrow = Input.GetKey(KeyCode.LeftArrow);
        bool rightArrow = Input.GetKey(KeyCode.RightArrow);


        //Поподание
        if (dmgRun)
        {
            cm.transform.position = Vector3.Lerp(cm.transform.position, new Vector3(0, 0, -10), Time.deltaTime * 10);
            cm.transform.position += new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0) * Time.deltaTime;
        }

        if (ChangeColor)
        {
            for (int i = 0; i < 100; i++) { }
            player.GetComponent<Image>().color = Color.red;
            ChangeColor = false;
        }
        else
        {
            player.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        //Проигрышь (жизни)
        HealthBar.transform.localScale = new Vector3(Health / MaxHealth, 1, 1);
        HpText.text = Health.ToString("");
        if (Health <= 0 && !death)
        {
            Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
            player.GetComponent<Image>().sprite = idDeath;
            Health = 0;
            Death();
        }

        //Проигрышь (топливо)
        FuelBar.transform.localScale = new Vector3(fuel / Maxfuel, 1, 1);
        FuelText.text = fuel.ToString("0.0");
        if (fuel <= 0 && !death)
        {
            fuel = 0;
            
            Death();
        }
        //Ограничение (топливо)
        if (fuel >= Maxfuel)
        {
            fuel = Maxfuel;
        }

        //Движение
        if (!death)
        {
            Move();
        }

        //Ускорение 
        if (boost)
        {
            if (IndBoost.localScale.y > 0) IndBoost.localScale -= new Vector3(0, 2, 0) * Time.deltaTime;
            if (IndBoost.localScale.y <= 0) { eff = true; effpos = Random.Range(0.0f, 0.5f); }
            if (eff && IndBoost.localScale.y < effpos) IndBoost.localScale += new Vector3(0, 4, 0) * Time.deltaTime;
            else eff = false;

            if (GetComponent<AudioSource>().pitch < 1.5f) GetComponent<AudioSource>().pitch += 0.01f;
        }
        else if (IndBoost.localScale.y < 1) IndBoost.localScale += new Vector3(0, 2, 0) * Time.deltaTime;
        if (!boost && GetComponent<AudioSource>().pitch > 1) GetComponent<AudioSource>().pitch -= 0.02f;

        //Руль
        TESTg.GetComponent<Text>().text = Input.mousePosition.x.ToString();
        if (Input.GetMouseButton(0) && mn.start || leftArrow && mn.start || rightArrow && mn.start)
        {
            if (!bbtn && !mn.pause)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (ray.origin.y <= 4f)
                {
                    wheelCntrl = true;
                    if (ray.origin.y <= -2.5f) rot = (ray.direction.x - ray.origin.x) * 45;
                    else
                    {
                        float dif = ray.direction.x - ray.origin.x;
                        if (dif <= 0) rot = -90;
                        else rot = 90;

                        if (leftArrow) rot = 90;
                        else if (rightArrow) rot = -90;
                    }
                    wheel.transform.rotation = Quaternion.Euler(0, 0, wheel.transform.rotation.z + rot);
                }
            }
        }
        else if (wheel.transform.localEulerAngles.z > 250 && wheel.transform.localEulerAngles.z < 357) { wheel.transform.rotation = Quaternion.Euler(0, 0, wheel.transform.localEulerAngles.z + 4f); wheelCntrl = true; }
        else if (wheel.transform.localEulerAngles.z < 110 && wheel.transform.localEulerAngles.z > 3) { wheel.transform.rotation = Quaternion.Euler(0, 0, wheel.transform.localEulerAngles.z - 4f); wheelCntrl = true; }
        else wheelCntrl = false;

        dirSpeed = Mathf.Abs(wheel.transform.rotation.z * 0.15f);
        if (wheel.transform.localEulerAngles.z < 110 && wheel.transform.localEulerAngles.z > 3)
        {
            left = true;
            right = false;
        }
        else if (wheel.transform.localEulerAngles.z > 250 && wheel.transform.localEulerAngles.z < 357)
        {
            right = true;
            left = false;
        }
        if (!wheelCntrl)//else
        {
            left = false;
            right = false;
            
        }

        if (wheel.transform.localEulerAngles.z > 225 && wheel.transform.localEulerAngles.z < 270) wheel.transform.rotation = Quaternion.Euler(0, 0, 270);
        if (wheel.transform.localEulerAngles.z > 90 && wheel.transform.localEulerAngles.z < 135) wheel.transform.rotation = Quaternion.Euler(0, 0, 90);
        TESTWheelAngel = wheel.transform.localEulerAngles.z;

        if (block || dirSpeed < 0.03f) player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (wheelCntrl && !death)
        {
            
            if (left)//Налево
            {
                if (!block && dirSpeed > 0.03f) player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 10));
                player.transform.position -= new Vector3(dirSpeed * 30, 0, 0) * Time.deltaTime;
            }
            else if (right)//Направо
            {
                if (!block && dirSpeed > 0.03f) player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -10));
                player.transform.position += new Vector3(dirSpeed * 30, 0, 0) * Time.deltaTime;
            }
        }
        //0.05f
        



    }

    private void Move()
    {

        //Ограничение
        if (player.transform.position.x <= -1.25f)
        {
            block = true;
            player.transform.position = new Vector3(-1.25f, player.transform.position.y, 0);
        }
        else if (player.transform.position.x >= 1.25f)
        {
            block = true;
            player.transform.position = new Vector3(1.25f, player.transform.position.y, 0);
        }
        else block = false;

        //Повороты
        /*
        if (!right && !left || block) player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        if (left)//Налево
        {
            if (!block) player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 10));
            player.transform.position -= new Vector3(dirSpeed, 0, 0);
        }
        else if (right)//Направо
        {
            if (!block) player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -10));
            player.transform.position += new Vector3(dirSpeed, 0, 0);
        }
        */

    }

    public void Left()
    {
        left = true;
    }
    public void UpLeft()
    {
        left = false;
    }

    public void Right()
    {
        right = true;
    }
    public void UpRight()
    {
        right = false;
    }

    void Death()
    {
        boost = false;
        death = true;
        GetComponent<AudioSource>().Stop();

        if (Health <= 0)
        {
            int r = Random.Range(1, 7);
            if (r == 1) rp.GameOverT.text = "Death\nhappens\n¯\\_(ツ)_/¯";
            else if (r == 2) rp.GameOverT.text = "Another\ndeath";
            else if (r == 3) rp.GameOverT.text = "Crash !";
            else if (r == 4) rp.GameOverT.text = "HP = 0";
            else if (r == 5) rp.GameOverT.text = "Mission Failed\nWe'll Get 'Em Next Time";
            else if (r == 6) rp.GameOverT.text = "Health\nis\nover";
            else if (r == 7) rp.GameOverT.text = "And\nanother\ndeath";
        }
        if (fuel <= 0)
        {
            rp.GameOverT.text = "Fuel\nis\nover";
            Color color;
            ColorUtility.TryParseHtmlString("#AAB230", out color);
            rp.GameOverT.color = color;
        }
    }

    public void Damage()
    {
        ChangeColor = true;
        Instantiate(textDamage, gameObject.transform.position, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("Pits").transform, false);
        HealthBar.transform.localScale = new Vector3(Health / MaxHealth, 1, 1);
        StartCoroutine(DmgCamera());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        int Set = 1;
        if (col.gameObject.tag == "Car")
        {
            Damage();

            if (Health > 0)
            {
                if (col.gameObject.transform.position.x > player.transform.position.x)
                {
                    player.transform.position -= new Vector3(0.2f, 0, 0);
                    if (col.gameObject.transform.position.y < player.transform.position.y)
                    {
                        Set = 1;
                    }
                    else Set = 2;
                }
                else
                {
                    player.transform.position += new Vector3(0.2f, 0, 0);
                    if (col.gameObject.transform.position.y > player.transform.position.y)
                    {
                        Set = 3;
                    }
                    else Set = 4;
                }
            }

            col.SendMessageUpwards("Death",Set);
        }

        //if (col.gameObject.tag == "CarD") Damage(); 
    }

    //
    public void Boost()
    {
        Pedal.localScale = new Vector3(1, 0.8f, 1);
        eff = false;

        boost = true;
    }
    public void BoostUp()
    {
        Pedal.localScale = new Vector3(1, 1, 1);
        eff = false;

        boost = false;
    }


    public void BoostBtnEnter()
    {
        bbtn = true;
    }
    public void BoostBtnExit()
    {
        bbtn = false;
    }
    /*//Снаряжение скина
    public void Skin(int id)
    {
        if (id == PlayerPrefs.GetInt("Skin"))
        {
            skin.speed = 0;
        }
        else
        {
            skin.speed = 1;
        }
    }
    */

    IEnumerator DmgCamera()
    {
        dmgRun = true;
        float time = 0;
        while (time == 0)
        {
            time = 0.5f;
            yield return new WaitForSeconds(time);
        }
        dmgRun = false;
    }
}
