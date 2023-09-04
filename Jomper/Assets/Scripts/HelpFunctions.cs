using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpFunctions : MonoBehaviour
{
    public static IEnumerator AppearanceDisappear(float timer, dynamic changeObj, bool action, GameObject actionObj = null) {
        float time = 1/(timer*30);
        if (action) {
            for (float ft = 0f; ft <= 1; ft += time)
            {
                Color c = changeObj.color;
                c.a = ft;
                changeObj.color = c;
                yield return new WaitForFixedUpdate();
            }

        } else {
            for (float ft = 1f; ft >= 0; ft -= time)
            {
                Color c = changeObj.color;
                c.a = ft;
                changeObj.color = c;
                yield return new WaitForFixedUpdate();
            }

            Destroy(actionObj);
        }
    }
}
