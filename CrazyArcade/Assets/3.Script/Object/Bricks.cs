using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    //������
    [SerializeField]
    private GameObject Item_Potion;

    [SerializeField]
    private GameObject Item_Ballon;

    [SerializeField]
    private GameObject Item_Skate;

    private BoxCollider2D collider;

    private int ItemPercent;    //������ ���� Ȯ��
    private int ItemType;       //������ 3���� �ϳ�

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("���ٱ� ����");
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
                    Debug.Log("����");
                    GameObject Potion = Instantiate(Item_Potion, gameObject.transform.position, Quaternion.identity);
                    break;

                case 2:
                    Debug.Log("��ǳ��");
                    GameObject Ballon = Instantiate(Item_Ballon, gameObject.transform.position, Quaternion.identity);
                    break;

                case 3:
                    Debug.Log("�Ź�");
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
