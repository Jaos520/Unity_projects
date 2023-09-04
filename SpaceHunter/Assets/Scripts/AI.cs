using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{


    FractionSystem fractionSystem;
    Movement movement;
    Soul soul;
    WeaponSystem weaponSystem;
    bool coroutineShoot;

    //Enemy
    public GameObject enemy;
    CircleCollider2D circle;

    public bool danger;
    public float hp;
    Coroutine coroutineDanger;

    private void Start() {
        fractionSystem = gameObject.GetComponent<FractionSystem>();
        movement = gameObject.GetComponent<Movement>();
        soul = gameObject.GetComponent<Soul>();
        weaponSystem = gameObject.GetComponent<WeaponSystem>();
        circle = gameObject.GetComponent<CircleCollider2D>();
        hp = soul.HP;
    }

    private void Update() {

        AttackEnemy();

        if (soul.HP < hp) {
            danger = true;
            hp = soul.HP;
        }
        
    }

    void AttackEnemy()
    {
        if (enemy != null) {
            circle.enabled = false;
            transform.RotateAround(enemy.transform.position, Vector3.back, movement.rotateSpeed);
            Vector3 v_diff = (enemy.transform.position - transform.position);    
            float angel = Mathf.Atan2(v_diff.x, v_diff.y)* Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angel, Vector3.back), Time.deltaTime);

            if (danger) {
                StartCoroutine(Move(Random.Range(1,3), Random.Range(0f,2f)));
                danger = false;
            }
            // Debug.Log(Vector3.Distance(enemy.transform.position, transform.position));
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance > 20) {
                movement.forward = true;
            } else {
                movement.forward = false;
            }
            
            if (distance < 10) {
                movement.back = true;
            } else {
                movement.back = false;
            }
        }
    }

    // IEnumerator Danger() {
    //     danger = true;
    //     yield return new WaitForSeconds(2);
    //     danger = false;
    // }

    /*
    0 - forward
    1 - left
    2 - right
    3 - back
    */
    IEnumerator Move(int direction, float sec) {
        if (direction == 0) movement.forward = true;
        else if (direction == 1) movement.left = true;
        else if (direction == 2) movement.right = true;
        else if (direction == 3) movement.back = true;

        yield return new WaitForSeconds(sec);

        if (direction == 0) movement.forward = false;
        else if (direction == 1) movement.left = false;
        else if (direction == 2) movement.right = false;
        else if (direction == 3) movement.back = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.GetComponent<FractionSystem>() != null) {

            FractionSystem fractionSys = other.gameObject.GetComponent<FractionSystem>();

            if (fractionSystem.fraction.isEnemy(fractionSys.fractionName)) {

                enemy = other.gameObject;
                movement.activeTargetMove = true;

            }

        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.GetComponent<Soul>() != null) {
            Soul otherSoul = other.GetComponent<Soul>();
            if (otherSoul.UID == enemy.GetComponent<Soul>().UID) {
                if (!coroutineShoot) {
                    StartCoroutine(Shoot());
                }
            }
        }

    }

    IEnumerator Shoot() {
        coroutineShoot = true;
        weaponSystem.Shoot();
        yield return new WaitForSeconds(1);
        coroutineShoot = false;

    }
    
}
