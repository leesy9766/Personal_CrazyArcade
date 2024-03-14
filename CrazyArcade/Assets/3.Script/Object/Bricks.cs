using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    //아이템
    [SerializeField]
    private GameObject Item_Potion;

    [SerializeField]
    private GameObject Item_Ballon;

    [SerializeField]
    private GameObject Item_Skate;

    private BoxCollider2D collider;

    private int ItemPercent;    //아이템 나올 확률
    private int ItemType;       //아이템 3개중 하나

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("물줄기 박음");
        if(collision.CompareTag("Water"))
        {
            Destroy(gameObject);
            Get_Item();

        }
    }


    private void Get_Item()
    {
        ItemPercent = Random.Range(1, 6);

        if(ItemPercent == 1)
        {
            ItemType = Random.Range(1, 3);

            switch(ItemType)
            {
                case 1:
                    Debug.Log("물약");
                    GameObject Potion = Instantiate(Item_Potion, gameObject.transform.position, Quaternion.identity);
                    break;

                case 2:
                    Debug.Log("물풍선");
                    GameObject Ballon = Instantiate(Item_Ballon, gameObject.transform.position, Quaternion.identity);
                    break;

                case 3:
                    Debug.Log("신발");
                    GameObject Skate = Instantiate(Item_Skate, gameObject.transform.position, Quaternion.identity);
                    break;

            }
        }
        else
        {
            Debug.Log("NONE");
        }
    }


}
