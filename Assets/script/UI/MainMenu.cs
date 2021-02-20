using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip select_clip;
    private AudioSource audio_source;
    // Start is called before the first frame update
    void Start()
    {
        audio_source = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    public void Play()
    {
        //Camera.main.GetComponent<AudioSource>().Stop();
        audio_source.clip = select_clip;
        audio_source.loop = false;
        audio_source.Play();
        Invoke("Next_Scene", audio_source.clip.length);
    }
    private void Next_Scene()
    {
        SceneManager.LoadScene("New_Game");

    }
}
