using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_UI : MonoBehaviour
{

    [SerializeField]
    private Text playerA_id;

    [SerializeField]
    private Text playerB_id;


    private Title title;

 
   

    void Start()
    {


        if(GameManager.Instance.CurrentGameMode == GameMode.Multi)
        {
            playerA_id.text = GameManager.Instance.Player_name_a;
            playerB_id.text = GameManager.Instance.Player_name_b;
        }
        else
        {
            playerA_id.text = GameManager.Instance.Player_name_a;
            playerB_id.text = null;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
