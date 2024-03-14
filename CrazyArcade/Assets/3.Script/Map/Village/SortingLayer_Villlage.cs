using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SortingLayer_Villlage : MonoBehaviour
{

    private Tilemap tilemap;

    private SpriteRenderer spriteRenderer;

    private Vector3 CurrentPos; //월드포지션 

    private Vector3Int CellPos; //셀포지션


    private void Awake()
    {
        tilemap = FindObjectOfType<Tilemap>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        CurrentPos = transform.position;
        CellPos = tilemap.WorldToCell(CurrentPos);
        Sorting_Order();
    }

    void Update()
    {
           
    }

    public void Sorting_Order()
    {
        switch(CellPos.y)
        {
            case -6:
                spriteRenderer.sortingOrder = 13;
                break;
            case -5:
                spriteRenderer.sortingOrder = 12;
                break;
            case -4:
                spriteRenderer.sortingOrder = 11;
                break;
            case -3:
                spriteRenderer.sortingOrder = 10;
                break;
            case -2:
                spriteRenderer.sortingOrder = 9;
                break;
            case -1:
                spriteRenderer.sortingOrder = 8;
                break;
            case 0:
                spriteRenderer.sortingOrder = 7;
                break;
            case 1:
                spriteRenderer.sortingOrder = 6;
                break;
            case 2:
                spriteRenderer.sortingOrder = 5;
                break;
            case 3:
                spriteRenderer.sortingOrder = 4;
                break;
            case 4:
                spriteRenderer.sortingOrder = 3;
                break;
            case 5:
                spriteRenderer.sortingOrder = 2;
                break;

            case 6:
                spriteRenderer.sortingOrder = 1;
                break;
           

        }
    }
}
