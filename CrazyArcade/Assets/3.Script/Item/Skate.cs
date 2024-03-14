using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skate : MonoBehaviour
{

    private CircleCollider2D collider;
    private Player_Controller collision_Player;

    private bool isTrigger = false;

    private void Start()
    {
        collider = GetComponent<CircleCollider2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision_Player = collision.GetComponent<Player_Controller>();

            collision_Player.MoveSpeed += 10;
            collision_Player.MaxSpeed += 10;

            Destroy(gameObject);

        }
        if (isTrigger)
        {
            if (collision.CompareTag("Water"))
            {
                Destroy(gameObject);
            }

        }


    }



    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);

        isTrigger = true;
        yield break;
    }


}