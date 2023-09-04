using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string UIDOwner;

    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HelpFunctions.AppearanceDisappear(3, gameObject.GetComponent<SpriteRenderer>(), false, gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 20, Space.Self);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<Soul>() != null && other.GetComponent<Soul>().UID == UIDOwner)
        {
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
    }
}
