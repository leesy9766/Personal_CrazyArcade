using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


enum Character
{
    Dizni = 0,
    Bazzi,
    Dao
}

public class GameManager : MonoBehaviour
{
    

    public static GameManager Instance = null;

    public string Player_name_a;
    public string Player_name_b;

    public GameMode CurrentGameMode;


    Character p1_character;
    Character p2_character;


    public string p1_selected_character;
    public string p2_selected_character;

    public GameObject Player1;
    public GameObject Player2;

    public bool is_1p;

    public bool is_p1Dead = false;
    public bool is_p2Dead = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("이미 게임매니저가 존재합니다.");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }
    

}
