using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    //public GameObject Sphere;
    public GameObject rplybtn;
    public GameObject Win;
    public float MaxJumpForce = 200f;
    public float MaxMoveForce = 0.1f;

    public bool grounded = false;

    public float Force = 5f;
    public float Stamina = 100f;
    public float MaxStamina = 100f;

    InterfaceController Interface;
    public Debug D;
    
    GameObject player;
    Rigidbody rb;

    Vector2 MousePos;
    Vector2 MouseDirection;
    Vector2 DirectionForce;

    Ray ray;

    PostProcessVolume m_PostProcessVolume;
    Vignette _Vignette;


    bool death;


    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        rb = GetComponent<Rigidbody>();
        Interface = cam.gameObject.GetComponent<InterfaceController>();
        death = false;

        m_PostProcessVolume = cam.GetComponent<PostProcessVolume>();
        m_PostProcessVolume.profile.TryGetSettings(out _Vignette);

        _Vignette.intensity.value = 0.3f;

        rplybtn.SetActive(false);

        StartCoroutine(StaminaStore());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        StaminaCtrl();
        Death();
        SphereTransform();
    }

    void SphereTransform()
    {
        if (transform.localScale.y < 1) transform.localScale += new Vector3(0,1f,0)*Time.deltaTime;
    }

    void Death()
    {
        if (death)
        {
            rplybtn.SetActive(true);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            
            if (_Vignette.intensity.value < 0.5f) _Vignette.intensity.value += 0.005f;
        }
    }

    void StaminaCtrl()
    {
        if (Stamina >= MaxStamina) Stamina = MaxStamina;

        if (Stamina <= 0) Stamina = 0;

        Interface.StaminaSprite.transform.localScale = new Vector3(Stamina / MaxStamina, 1, 1);
    }

    void DirectionControll()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        
        MouseDirection = ray.direction;
    }

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //float loss = (Mathf.Abs(MouseDirection.x) + Mathf.Abs(MouseDirection.y)) * (Force * 0.1f);
            if (20 < Stamina)
            {
                DirectionControll();
                rb.AddForce(MouseDirection * Force);
                Stamina -= 20;

                //Sphere.transform.localScale = new Vector3(3, 1, 1);
                transform.localScale = new Vector3(1, 0.5f, 1);
                //Get the Screen positions of the object
                Vector2 positionOnScreen = cam.WorldToViewportPoint(transform.position);

                //Get the Screen position of the mouse
                Vector2 mouseOnScreen = (Vector2)cam.ScreenToViewportPoint(Input.mousePosition);

                //Get the angle between the points
                float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            }
            //D.DebugT(loss.ToString());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            rb.drag = 5;
        }

        if (collision.gameObject.tag == "Danger")
        {
            Interface.Death(transform.position);
            rb.isKinematic = true;
            death = true;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Interface.Death(transform.position);
            rb.isKinematic = true;
            death = true;
        }

        if (collision.gameObject.tag == "Win")
        {
            Interface.Death(transform.position);
            rb.isKinematic = true;
            Win.SetActive(true);
            rplybtn.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
            rb.drag = 0;
        }
    }

    IEnumerator StaminaStore()
    {
        while (true)
        {
            if (Stamina < MaxStamina && grounded) Stamina++;
            yield return new WaitForSeconds(0.01f);
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
