using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRest : MonoBehaviour {

	public void ReReset()
    {
        PlayerPrefs.DeleteAll();
    }
}
