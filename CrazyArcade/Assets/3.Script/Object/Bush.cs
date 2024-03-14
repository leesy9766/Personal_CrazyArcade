using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    private BoxCollider2D collider;

    [SerializeField]
    private AudioSource audio;

    public AudioClip Clip_bush;


    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("´ýºÒ ¹ÚÀ½");
        if(collision.CompareTag("Player"))
        {
            audio.clip = Clip_bush;
            audio.Play();
            
        }
        else if (collision.CompareTag("Water"))
        {
            Destroy(gameObject);
            

        }
        
    }
}
