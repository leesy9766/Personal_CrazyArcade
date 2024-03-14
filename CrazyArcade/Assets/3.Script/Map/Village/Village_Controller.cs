using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Village_Controller : MonoBehaviour
{
    [SerializeField]
    GameObject Dizni;

    [SerializeField]
    GameObject Bazzi;

    [SerializeField]
    GameObject Dao;

    [SerializeField]
    private GameObject[] StartPoints;

    [SerializeField]
    private Image img_1p;

    [SerializeField]
    private Image img_2p;

    [SerializeField]
    private Text text_1p;

    [SerializeField]
    private Text text_2p;



    [SerializeField]
    private Sprite Dizni_img;

    [SerializeField]
    private Sprite Bazzi_img;

    [SerializeField]
    private Sprite Dao_img;



    [SerializeField]
    public GameObject icon_1p;

    [SerializeField]
    public GameObject icon_2p;

    private GameObject player_icon;


    void Start()
    {
        //GameManager.Instance.Player1 = Instantiate(Dizni, Get_StartPoint(Random.Range(1, 4)), Quaternion.identity);
        //GameManager.Instance.Player2 = Instantiate(Bazzi, Get_StartPoint(Random.Range(1, 4)), Quaternion.identity);
        switch (GameManager.Instance.p1_selected_character)
        {
            case "Dizni":
                GameManager.Instance.Player1 = Instantiate(Dizni, Get_StartPoint(Random.Range(1, 4)), Quaternion.identity);
                player_icon = Instantiate(icon_1p, GameManager.Instance.Player1.GetComponent<Player_Controller>().transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
                player_icon.transform.parent = GameManager.Instance.Player1.transform;
                img_1p.sprite = Dizni_img;
            
                break;

            case "Bazzi":
                GameManager.Instance.Player1 = Instantiate(Bazzi, Get_StartPoint(Random.Range(1, 4)), Quaternion.identity);
                player_icon = Instantiate(icon_1p, GameManager.Instance.Player1.GetComponent<Player_Controller>().transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
                player_icon.transform.parent = GameManager.Instance.Player1.transform;
                img_1p.sprite = Bazzi_img;
                break;

            case "Dao":
                GameManager.Instance.Player1 = Instantiate(Dao, Get_StartPoint(Random.Range(1, 4)), Quaternion.identity);
                player_icon = Instantiate(icon_1p, GameManager.Instance.Player1.GetComponent<Player_Controller>().transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
                player_icon.transform.parent = GameManager.Instance.Player1.transform;
                img_1p.sprite = Dao_img;
                break;
        }


        switch (GameManager.Instance.p2_selected_character)
        {
            case "Dizni":
                GameManager.Instance.Player2 = Instantiate(Dizni, Get_StartPoint(Random.Range(1, 4)), Quaternion.identity);
                player_icon = Instantiate(icon_2p, GameManager.Instance.Player2.GetComponent<Player_Controller>().transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
                player_icon.transform.parent = GameManager.Instance.Player2.transform;
                img_2p.sprite = Dizni_img;
                break;

            case "Bazzi":
                GameManager.Instance.Player2 = Instantiate(Bazzi, Get_StartPoint(Random.Range(1, 4)), Quaternion.identity);
                player_icon = Instantiate(icon_2p, GameManager.Instance.Player2.GetComponent<Player_Controller>().transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
                player_icon.transform.parent = GameManager.Instance.Player2.transform;
                img_2p.sprite = Bazzi_img;
                break;

            case "Dao":
                GameManager.Instance.Player2 = Instantiate(Dao, Get_StartPoint(Random.Range(1, 4)), Quaternion.identity);
                player_icon = Instantiate(icon_2p, GameManager.Instance.Player2.GetComponent<Player_Controller>().transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
                player_icon.transform.parent = GameManager.Instance.Player2.transform;
                img_2p.sprite = Dao_img;
                break;
        }
    }


    public Vector3 Get_StartPoint(int num)
    {
        switch (num)
        {
            case 1:
                transform.position = new Vector3(-5.544f, 4.228f, 0f);
                
                break;

            case 2:
                transform.position = new Vector3(2.94f, 3.59f, 0f);
                break;

            case 3:
                transform.position = new Vector3(3.57f, -3.6f, 0f);
                break;

            case 4:
                transform.position = new Vector3(-4.87f, -2.95f, 0f);
                break;
        }

        return transform.position;
    }


    private void Update()
    {
        if (GameManager.Instance.Player1.GetComponent<Player_Controller>().isDead == true)
        {
            Debug.Log("1p¡Í±¿,,");
            switch (GameManager.Instance.p1_selected_character)
            {
                case "Dizni":
                    img_1p.GetComponent<Animator>().SetBool("Dizni",true);
                    break;

                case "Bazzi":                  
                    img_1p.GetComponent<Animator>().SetBool("Bazzi", true);                   
                    break;

                case "Dao":                  
                    img_1p.GetComponent<Animator>().SetBool("Dao", true);                
                    break;
            }
        }


        if (GameManager.Instance.Player2.GetComponent<Player_Controller>().isDead == true)
        {
            switch (GameManager.Instance.p2_selected_character)
            {
                case "Dizni":
                    img_2p.GetComponent<Animator>().SetBool("Dizni", true);
                    break;

                case "Bazzi":                 
                    img_2p.GetComponent<Animator>().SetBool("Bazzi", true);                
                    break;

                case "Dao":                
                    img_2p.GetComponent<Animator>().SetBool("Dao", true);                   
                    break;
            }
        }


    }
}
