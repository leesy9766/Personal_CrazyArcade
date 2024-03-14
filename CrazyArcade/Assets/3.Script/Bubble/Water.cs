using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private BoxCollider2D collider;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }


}
