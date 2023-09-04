using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    public Text deathText;
    
    public GameObject StaminaSprite;
    public GameObject Interface;
    public GameObject parts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Death(Vector3 location)
    {
        for (int i = 0; i < 15; i++)
        {
            Instantiate(parts, new Vector3(location.x + Random.Range(-0.05f, 0.05f), location.y + Random.Range(-0.05f, 0.05f), location.z + Random.Range(-0.01f, 0.01f)), Quaternion.identity);
        }

        deathText.gameObject.SetActive(true);
        Interface.SetActive(false);
    }
}
