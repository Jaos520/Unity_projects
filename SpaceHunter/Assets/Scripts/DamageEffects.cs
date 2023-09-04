using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffects : MonoBehaviour
{
    public string effect; 
    // Start is called before the first frame update
    void Start()
    {
        if (effect == "BulletSpark") StartCoroutine(Spark());
    }

    // Update is called once per frame
    void Update()
    {
        if (effect == "Explosion") {
            if (gameObject.GetComponent<ParticleSystem>().isStopped) Destroy(gameObject);
        }
    }

    IEnumerator Spark() {

        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
