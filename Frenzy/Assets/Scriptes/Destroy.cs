using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject[] Parts;
    public GameObject MainPart;
    // Start is called before the first frame update
    void Start()
    {
        MainPart.SetActive(true);
        for (int i = 0; i < Parts.Length; i++)
        {
            Parts[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MainPart.SetActive(false);
            for (int i = 0; i < Parts.Length; i++)
            {
                Parts[i].SetActive(true);
            }
        }
    }
}
