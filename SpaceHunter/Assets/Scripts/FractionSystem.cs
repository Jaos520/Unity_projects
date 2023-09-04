using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractionSystem : MonoBehaviour
{
    [SerializeField]
    public string fractionName;

    public Fraction fraction;

    private void Start() {
        fraction = FractionsRelations.getFraction(fractionName);
    }

}
