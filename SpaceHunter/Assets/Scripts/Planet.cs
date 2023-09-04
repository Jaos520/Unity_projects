using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public string Name;
    public string Discription;
    public Vector2 Coordinats = new Vector2(0,0);
    public string Scene;

    public GameObject EnterButton;

    void Start()
    {
        gameObject.transform.position = Coordinats;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<TravelPlanet>() != null) {
            other.gameObject.GetComponent<TravelPlanet>().EnterPlanet(Name, Scene, EnterButton);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<TravelPlanet>() != null) {
            other.gameObject.GetComponent<TravelPlanet>().ExitPlanet();
        }
    }
}
