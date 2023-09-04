using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header("Статичные атрибуты (Храктеристики корабля)")]
    public float maxHP;
    public float friction;
    public float maxSpeed;
    public float forceSpeed;
    public float maxRotateSpeed;
    public float forceRotateSpeed;

    [Space]
    public Sprite SpaceshipSprite;
    public GameObject Weapon;

    private void Start() {
        SpaceshipSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
}
