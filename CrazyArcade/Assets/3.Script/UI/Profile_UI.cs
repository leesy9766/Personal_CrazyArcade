using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile_UI : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(GameManager.Instance.p1_selected_character == "Dizni")
        {
            animator.SetBool("Dizni", true);
        }
        if (GameManager.Instance.p1_selected_character == "Bazzi")
        {
            animator.SetBool("Bazzi", true);
        }
        if (GameManager.Instance.p1_selected_character == "Dao")
        {
            animator.SetBool("Dao", true);
        }

    }

}
