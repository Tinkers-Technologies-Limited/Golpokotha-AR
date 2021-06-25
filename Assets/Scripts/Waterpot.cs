using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterpot : MonoBehaviour
{
    [SerializeField] StateHandler sh;
    private void OnMouseDown()
    {
        gameObject.SetActive(false);
        sh.IncreasePotCount();
    }
}
