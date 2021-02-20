using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player pj;
    public Text vidas_txt;
    public Text monedas_txt;
    public AudioClip death_clip;
    public AudioClip select_clip;
    public GameObject game_over;
    public GameObject cam;
    public GameObject butt;
    private AudioSource asour;


    // Start is called before the first frame update
    void Start()
    {
        game_over.SetActive(false);
        asour = cam.GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        cambiaUI();

        if (pj.Lives <= 0 && !game_over.activeSelf)
        {
            game_over.SetActive(true);
            asour.clip = death_clip;
            asour.Play();
        }
        
    }

    void cambiaUI()
    {
        int monedas = pj.Coins;
        if (monedas < 10)
        {
            monedas_txt.text = "00" + monedas;
        }
        else if (monedas < 100)
        {
            monedas_txt.text = "0" + monedas;
        }
        else
        {
            monedas_txt.text = monedas.ToString();
        }
        vidas_txt.text = "x " + pj.Lives.ToString();
    }
    public void Restart()
    {
        butt.SetActive(false);
        asour.clip = select_clip;
        asour.loop = false;
        asour.Play();
        Invoke("NewGame",asour.clip.length);
    }
    private void NewGame()
    {
        SceneManager.LoadScene("New_Game");
    }
}
