using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Select_Character : MonoBehaviour
{

    [SerializeField]
    GameObject SinglePanel;

    [SerializeField]
    GameObject MultiPanel;


    //1p버튼
    [SerializeField]
    private Button P1_Dizni_btn;

    [SerializeField]
    private Button P1_Bazzi_btn;

    [SerializeField]
    private Button P1_Dao_btn;


    //2p버튼
    [SerializeField]
    private Button P2_Dizni_btn;

    [SerializeField]
    private Button P2_Bazzi_btn;

    [SerializeField]
    private Button P2_Dao_btn;




    [SerializeField]
    private Image p1_check;

    [SerializeField]
    private Image p2_check;


    [SerializeField]
    private Sprite blue_check;

    [SerializeField]
    private Sprite pink_check;


    [SerializeField]
    private Button StartBtn;


    [SerializeField]
    private Text Player1_name;

    [SerializeField]
    private Text Player2_name;


    private bool is_p1_Select = false;
    private bool is_p2_Select = false;



    private bool is_p1Ready = false;
    private bool is_p2Ready = false;



    private bool isPossible_GameStart = false;


    private void Start()
    {

        Player1_name.text = GameManager.Instance.Player_name_a;
        Player2_name.text = GameManager.Instance.Player_name_b;

        if (GameManager.Instance.CurrentGameMode == GameMode.Multi)
        {
            SinglePanel.SetActive(false);
            
            MultiPanel.SetActive(true);
        }
        else
        {
            SinglePanel.SetActive(true);
            MultiPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if(is_p1Ready && is_p2Ready)
        {
            isPossible_GameStart = true;
            
        }
    }


    public void P1_Dizni_Btn()
    {
        if (is_p1_Select == false)
        {
            GameManager.Instance.p1_selected_character = "Dizni";

            P1_Bazzi_btn.GetComponent<Outline>().effectDistance = new Vector2(0f, 0f);
            P1_Bazzi_btn.GetComponent<Outline>().effectColor = new Color(111, 0, 0, 0);


            P1_Dao_btn.GetComponent<Outline>().effectDistance = new Vector2(0f, 0f);
            P1_Dao_btn.GetComponent<Outline>().effectColor = new Color(0, 17, 168, 0);

            //--------------------------------------------------------------
            P1_Dizni_btn.GetComponent<Outline>().effectDistance = new Vector2(5f, 5f);
            P1_Dizni_btn.GetComponent<Outline>().effectColor = new Color(207, 0, 255, 1);
            is_p1_Select = true;
        }
    }
      

    public void P1_Bazzi_Btn()
    {
        if (is_p1_Select == false)
        {
            GameManager.Instance.p1_selected_character = "Bazzi";

            P1_Dizni_btn.GetComponent<Outline>().effectDistance = new Vector2(0f, 0f);
            P1_Dizni_btn.GetComponent<Outline>().effectColor = new Color(207, 0, 255, 0);


            P1_Dao_btn.GetComponent<Outline>().effectDistance = new Vector2(0f, 0f);
            P1_Dao_btn.GetComponent<Outline>().effectColor = new Color(0, 17, 168, 0);

            //--------------------------------------------------------------
            P1_Bazzi_btn.GetComponent<Outline>().effectDistance = new Vector2(5f, 5f);
            P1_Bazzi_btn.GetComponent<Outline>().effectColor = new Color(111, 0, 0, 1);
            is_p1_Select = true;
        }
        
    }

    public void P1_Dao_Btn()
    {
        if (is_p1_Select == false)
        {
            GameManager.Instance.p1_selected_character = "Dao";

            P1_Dizni_btn.GetComponent<Outline>().effectDistance = new Vector2(0f, 0f);
            P1_Dizni_btn.GetComponent<Outline>().effectColor = new Color(207, 0, 255, 0);

            P1_Bazzi_btn.GetComponent<Outline>().effectDistance = new Vector2(0f, 0f);
            P1_Bazzi_btn.GetComponent<Outline>().effectColor = new Color(111, 0, 0, 0);


            //--------------------------------------------------------------
            P1_Dao_btn.GetComponent<Outline>().effectDistance = new Vector2(5f, 5f);
            P1_Dao_btn.GetComponent<Outline>().effectColor = new Color(0, 17, 168, 1);
            is_p1_Select = true;
        }
          
    }






    public void P2_Dizni_Btn()
    {
        if (is_p2_Select == false)
        {
            GameManager.Instance.p2_selected_character = "Dizni";

            P2_Bazzi_btn.GetComponent<Outline>().effectDistance = new Vector2(0f, 0f);
            P2_Bazzi_btn.GetComponent<Outline>().effectColor = new Color(111, 0, 0, 0);


            P2_Dao_btn.GetComponent<Outline>().effectDistance = new Vector2(0f, 0f);
            P2_Dao_btn.GetComponent<Outline>().effectColor = new Color(0, 17, 168, 0);

            //--------------------------------------------------------------
            P2_Dizni_btn.GetComponent<Outline>().effectDistance = new Vector2(5f, 5f);
            P2_Dizni_btn.GetComponent<Outline>().effectColor = new Color(207, 0, 255, 1);
            is_p2_Select = true;
        }
        
    }

    public void P2_Bazzi_Btn()
    {
        if (is_p2_Select == false)
        {
            GameManager.Instance.p2_selected_character = "Bazzi";

            P2_Dizni_btn.GetComponent<Outline>().effectDistance = new Vector2(0f, 0f);
            P2_Dizni_btn.GetComponent<Outline>().effectColor = new Color(207, 0, 255, 0);


            P2_Dao_btn.GetComponent<Outline>().effectDistance = new Vector2(0f, 0f);
            P2_Dao_btn.GetComponent<Outline>().effectColor = new Color(0, 17, 168, 0);

            //--------------------------------------------------------------
            P2_Bazzi_btn.GetComponent<Outline>().effectDistance = new Vector2(5f, 5f);
            P2_Bazzi_btn.GetComponent<Outline>().effectColor = new Color(111, 0, 0, 1);

            is_p2_Select = true;
        }
           
    }

    public void P2_Dao_Btn()
    {
        if(is_p2_Select == false)
        {
            GameManager.Instance.p2_selected_character = "Dao";

            P2_Dizni_btn.GetComponent<Outline>().effectDistance = new Vector2(0f, 0f);
            P2_Dizni_btn.GetComponent<Outline>().effectColor = new Color(207, 0, 255, 0);

            P2_Bazzi_btn.GetComponent<Outline>().effectDistance = new Vector2(0f, 0f);
            P2_Bazzi_btn.GetComponent<Outline>().effectColor = new Color(111, 0, 0, 0);


            //--------------------------------------------------------------
            P2_Dao_btn.GetComponent<Outline>().effectDistance = new Vector2(5f, 5f);
            P2_Dao_btn.GetComponent<Outline>().effectColor = new Color(0, 17, 168, 1);
            is_p2_Select = true;

        }
      

        
    }



    public void p1_Ready()
    {  
        if(is_p1Ready == false && is_p2_Select)
        {
            is_p1Ready = true;
            p1_check.sprite = pink_check;
        }
        else
        {
            is_p1Ready = false;
            p1_check.sprite = blue_check;
        }

       
    }


    public void p2_Ready()
    {
        if (is_p2Ready == false && is_p2_Select)
        {
            is_p2Ready = true;
            p2_check.sprite = pink_check;
        }
        else
        {
            is_p2Ready = false;
            p2_check.sprite = blue_check;
        }

    }


    public void Start_Btn()
    {
        if(isPossible_GameStart)
        {
            SceneManager.LoadScene("Game_Village");

        }
        
    }

}
