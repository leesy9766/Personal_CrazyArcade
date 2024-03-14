using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursoro_UI : MonoBehaviour
{
    [SerializeField]
    private Texture2D Mouse_Image;

    private AudioSource audio;

    [SerializeField]
    private AudioClip Click_Clip;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(Mouse_Image, Vector2.zero, CursorMode.Auto);
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      
        
    }

    public void Click_Sound()
    {
        audio.clip = Click_Clip;
        audio.Play();
    }

}
