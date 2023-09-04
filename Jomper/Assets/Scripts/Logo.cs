using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logo : MonoBehaviour
{
    public GameObject backLogo;
    void Start()
    {
        StartCoroutine(HelpFunctions.AppearanceDisappear(3, gameObject.GetComponent<Image>(), true));
        StartCoroutine(Gone());
    }

    IEnumerator Gone()
    {
        
        yield return new WaitForSeconds(4);
        StartCoroutine(HelpFunctions.AppearanceDisappear(2, gameObject.GetComponent<Image>(), false));
        yield return new WaitForSeconds(2);
        StartCoroutine(HelpFunctions.AppearanceDisappear(2, backLogo.gameObject.GetComponent<Image>(), false, backLogo));
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    
}
