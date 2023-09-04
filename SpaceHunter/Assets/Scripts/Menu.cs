using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Text StartBtn;
    public GameObject Player;
    Camera mainCamera;
    bool start;

    public string test;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        start = true;
        if (mainCamera != null && Player != null) mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y-20, mainCamera.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Begin();
    }

    void Begin()
    {
        if (StartBtn != null) {
            if (start && StartBtn.color.a > 0.01f) {
                StartBtn.color = Color.Lerp(StartBtn.color, new Color(StartBtn.color.r, StartBtn.color.g, StartBtn.color.b, 0), Time.deltaTime* 2);
                StartBtn.gameObject.transform.position -= new Vector3(0,0.05f,0);
            } else if (start) {
                Destroy(StartBtn.gameObject);
            }
        }
    }

    public void ToTheSpaceScene()
    {
        SceneManager.LoadScene("Space", LoadSceneMode.Single);
    }
}
