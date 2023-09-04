using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCars : MonoBehaviour
{

    public bool startSpawn;
    public GameObject Cars;
    public GameObject Fuel;
    public GameObject Coin;
    public GameObject BonusCoin;
    public GameObject Pit;
    public GameObject Repair;

    public float timeSpawn;

    public Player pl;
    public MainCntrl mn;

    Vector2 pos1;
    Vector2 pos2;
    Vector2 pos3;
    Vector2 pos4;

    float dis;
    float disB;
    float disR;
    bool disP;
    float posit;

    void Start()
    {
        timeSpawn = 1;

        startSpawn = true;
        dis = 100;
        disB = Random.Range(200, 400);
        disR = Random.Range(200, 400);
        disP = false;

        pos1 = new Vector2(-150f, 720);
        pos2 = new Vector2(-50f, 720);
        pos3 = new Vector2(50f, 720);
        pos4 = new Vector2(150f, 720);

        StartCoroutine(SpawnCar());
        StartCoroutine(SpawnCoin());
    }

    void Update()
    {

        if (pl.death)
        {
            startSpawn = false;
        }
        else
        {
            startSpawn = true;
        }

        if (!mn.pause)
        {
            if (mn.Distance >= dis)
            {
                posit = Random.Range(0, 5);
                if (posit <= 1)
                {
                    Instantiate(Fuel, pos1, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
                }
                else if (posit <= 2)
                {
                    Instantiate(Fuel, pos2, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
                }
                else if (posit <= 3)
                {
                    Instantiate(Fuel, pos3, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
                }
                else if (posit <= 4)
                {
                    Instantiate(Fuel, pos4, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
                }
                dis += 100;
            }

            if (mn.Distance >= disB)
            {
                posit = Random.Range(0, 5);
                if (posit <= 1)
                {
                    Instantiate(BonusCoin, pos1, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
                }
                else if (posit <= 2)
                {
                    Instantiate(BonusCoin, pos2, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
                }
                else if (posit <= 3)
                {
                    Instantiate(BonusCoin, pos3, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
                }
                else if (posit <= 4)
                {
                    Instantiate(BonusCoin, pos4, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
                }
                disB += Random.Range(200, 400);
            }

            if (mn.Distance >= disR)
            {
                posit = Random.Range(0, 5);
                if (posit <= 1)
                {
                    Instantiate(Repair, pos1, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
                }
                else if (posit <= 2)
                {
                    Instantiate(Repair, pos2, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
                }
                else if (posit <= 3)
                {
                    Instantiate(Repair, pos3, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
                }
                else if (posit <= 4)
                {
                    Instantiate(Repair, pos4, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
                }
                disR += Random.Range(200, 400);
            }
        }
        if (mn.Distance >= 300 && !disP)
        {
            StartCoroutine(SpawnPit());
            disP = true;
        }

    }

    IEnumerator SpawnPit()
    {
        float position;
        float time;

        while (startSpawn)
        {
            position = Random.Range(0, 5);

            if (!mn.pause)
            {
                timeSpawn = 10f / mn.MainSpeed;
                time = Random.Range(timeSpawn, 20.0f / mn.MainSpeed);
            }
            else
            {
                timeSpawn = 10f / 5;
                time = Random.Range(timeSpawn, 20.0f / 5);
            }

            if (!mn.pause)
            {
                if (position <= 1)
                {
                    Instantiate(Pit, pos1, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("Pits").transform, false);
                }
                else if (position <= 2)
                {
                    Instantiate(Pit, pos2, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("Pits").transform, false);
                }
                else if (position <= 3)
                {
                    Instantiate(Pit, pos3, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("Pits").transform, false);
                }
                else if (position <= 4)
                {
                    Instantiate(Pit, pos4, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("Pits").transform, false);
                }
            }
            yield return new WaitForSeconds(time);
        }
    }

    IEnumerator SpawnCar()
    {
        float position;
        float time;

        while (startSpawn)
        {
            position = Random.Range(0, 5);

            if (!mn.pause)
            {
                timeSpawn = 2f / mn.MainSpeed;
                time = Random.Range(timeSpawn, 10.0f / mn.MainSpeed);
            }
            else
            {
                timeSpawn = 2f / 5;
                time = Random.Range(timeSpawn, 10.0f / 5);
            }

            if (!mn.pause)
            {
                if (position <= 1)
                {
                    Instantiate(Cars, pos1, Quaternion.Euler(0, 0, 180)).transform.SetParent(GameObject.FindGameObjectWithTag("CarS").transform, false);
                }
                else if (position <= 2)
                {
                    Instantiate(Cars, pos2, Quaternion.Euler(0, 0, 180)).transform.SetParent(GameObject.FindGameObjectWithTag("CarS").transform, false);
                }
                else if (position <= 3)
                {
                    Instantiate(Cars, pos3, Quaternion.Euler(0, 0, 180)).transform.SetParent(GameObject.FindGameObjectWithTag("CarS").transform, false);
                }
                else if (position <= 4)
                {
                    Instantiate(Cars, pos4, Quaternion.Euler(0, 0, 180)).transform.SetParent(GameObject.FindGameObjectWithTag("CarS").transform, false);
                }
            }
            yield return new WaitForSeconds(time);
        }
    }

    IEnumerator SpawnCoin()
    {
        float position;
        float time;

        while (startSpawn)
        {
            position = Random.Range(0, 5);

            if (!mn.pause)
            {
                timeSpawn = 2f / mn.MainSpeed;
                time = Random.Range(timeSpawn, 10.0f / mn.MainSpeed);
            }
            else
            {
                timeSpawn = 2f / 5;
                time = Random.Range(timeSpawn, 10.0f / 5);
            }

            if (!mn.pause)
            {
                if (position <= 1)
                {
                    Instantiate(Coin, pos1, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
                }
                else if (position <= 2)
                {
                    Instantiate(Coin, pos2, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
                }
                else if (position <= 3)
                {
                    Instantiate(Coin, pos3, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
                }
                else if (position <= 4)
                {
                    Instantiate(Coin, pos4, Quaternion.identity).transform.SetParent(GameObject.FindGameObjectWithTag("SecondCanvas").transform, false);
                }
            }
            yield return new WaitForSeconds(time);
        }
    }
}
