using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village01 : MonoBehaviour
{


    [SerializeField]
    private AudioSource audio;

    [SerializeField]
    public AudioClip DeadClip;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
}
