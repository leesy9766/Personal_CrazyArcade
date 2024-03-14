using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject Result_panel;

    [SerializeField]
    private Image Result_img;


    [SerializeField]
    private Sprite p1_win;


    [SerializeField]
    private Sprite p2_win;

    private void Start()
    {
        Result_panel.SetActive(false);
    }

    private void Update()
    {
        if(GameManager.Instance.is_p1Dead)
        {
            Result_img.sprite = p2_win;
        }
        else if( GameManager.Instance.is_p2Dead)
        {
            Result_img.sprite = p1_win;
        }
    }



}
