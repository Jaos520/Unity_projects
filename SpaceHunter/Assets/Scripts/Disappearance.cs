using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disappearance : MonoBehaviour
{
    private void Start() {
        StartCoroutine(HelpFunctions.AppearanceDisappear(2,gameObject.GetComponent<Text>(), false, gameObject));
    }
}
