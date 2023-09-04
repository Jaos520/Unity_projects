using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float HP;
    public float MaxHP;

    // public SpriteRenderer CurHP;
    // public SpriteRenderer PathHP;

    // public GameObject HPObj;
    // public float pathHP;
    public int LVL;

    Player player;

    public GameObject prefabExplosion;
    GameObject explosion;
    ParticleSystem explosionParticleSystem;

    public GameObject DamageEffects;

    Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        // HPObj.SetActive(false);
        // MaxHP = 100;
        HP = MaxHP;
        // pathHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        // if (player != null) Quaternion.Slerp(HPObj.transform.rotation, player.transform.rotation, Time.deltaTime);
    }

    public void GetDamage(int dmg)
    {
        HP -= dmg;
        player.TargetDamage(dmg, HP);
        // StopAllCoroutines();
        // StartCoroutine(LostHP());
        // CurHP.size = new Vector2(HP/MaxHP, CurHP.size.y);
        if (HP <= 0) {
            Death();
        }
    }

    void Death()
    {
        if (player != null) {
            player.targetEnemy = false;
        }

        Destroy(gameObject);

        Instantiate(prefabExplosion, gameObject.transform.position, Quaternion.identity);
    }

    // IEnumerator LostHP() {


    //     yield return new WaitForSeconds(0.5f);
    //     float step = (pathHP-HP)/30;
    //     for (; pathHP > HP; pathHP -= step) 
    //     {
    //         PathHP.size = new Vector2(pathHP/MaxHP, PathHP.size.y);
    //         yield return new WaitForFixedUpdate();
    //     }
    //     pathHP = HP;
    //     PathHP.size = new Vector2(pathHP/MaxHP, PathHP.size.y);
    // }

    private void OnMouseDown() {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.targetEnemyObj = gameObject;
        }
        player.targetEnemy = !player.targetEnemy;
        player.target_MaxHP = MaxHP;
        player.target_HPObj.SetActive(player.targetEnemy);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<Bullet>() != null)
        {
            if (other.gameObject.GetComponent<Bullet>().UIDOwner != "NPC")
            {
                GetDamage(other.gameObject.GetComponent<Bullet>().Damage);

                ContactPoint2D[] contacts = new ContactPoint2D[2];

                other.GetContacts(contacts);
                Vector3 point = contacts[0].point;

                // rb.AddForce(point, ForceMode2D.Force);
                

                Instantiate(DamageEffects, point, transform.rotation);
                Destroy(other.gameObject);
            }
        }
    }
}
