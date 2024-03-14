using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField]
    private InputField input_a;

    [SerializeField]
    private InputField input_b;

    [SerializeField]
    private GameObject Multi_Panel;

    [SerializeField]
    private GameObject Single_Panel;

   
    private Button StartBtn;
    private Button single_Btn;
    private Button multi_Btn;

    

    


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.CurrentGameMode = GameMode.Multi;
        Single_Panel.SetActive(false);
        Multi_Panel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart_Btn()
    {
        if(GameManager.Instance.CurrentGameMode == GameMode.Multi)
        {
            GameManager.Instance.Player_name_a = input_a.text;
            GameManager.Instance.Player_name_b = input_b.text;

        }
        else
        {
            GameManager.Instance.Player_name_a = input_a.text;
            GameManager.Instance.Player_name_b = null;
        }
        
       

        SceneManager.LoadScene("Select_Character");
    }

    public void Multi_Btn()
    {
        GameManager.Instance.CurrentGameMode = GameMode.Multi;
        Single_Panel.SetActive(false);
        Multi_Panel.SetActive(true);
    }

    public void Single_Btn()
    {
        GameManager.Instance.CurrentGameMode = GameMode.Solo;
        Single_Panel.SetActive(true);
        Multi_Panel.SetActive(false);
    }

}
