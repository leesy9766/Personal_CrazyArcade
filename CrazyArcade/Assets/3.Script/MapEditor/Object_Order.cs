using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Object_Order : MonoBehaviour
{

    private int[,] MapCell;


    [SerializeField]
    private Tilemap tilemap;
    Vector3 localPos; // 로컬 포지션
    Vector3 worldPos; // 월드 포지션

    private void Awake()
    {
        
    }
    private void Start()
    {
        
        
        
        Debug.Log(tilemap.size.x);
        Debug.Log(tilemap.size.y);
      
    }
}
