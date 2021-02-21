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
    public GameObject pause_Panel;
    public GameObject game_over;
    public GameObject cam;
    public GameObject butt;
    public GameObject newRecordText;
    public Text RecordText;
    private AudioSource asour;
    public float scaleTime;
    public float scaleInc;


    // Start is called before the first frame update
    void Start()
    {
        game_over.SetActive(false);
        asour = cam.GetComponent<AudioSource>();
        InvokeRepeating("GameTimeScale", scaleTime, scaleTime);
    }

    // Update is called once per frame
    void Update()
    {
        cambiaUI();
        Game_over();
        if (Time.timeScale == 2.5f)
        {
            CancelInvoke("GameTimeScale");
        }
    }
    void Game_over()
    {
        if (pj.Lives <= 0 && !game_over.activeSelf)
        {
            MyRecord();
            game_over.SetActive(true);
            asour.clip = death_clip;
            asour.Play();
            CancelInvoke("GameTimeScale");
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
        Invoke("NewGame", asour.clip.length);
        resetTimeScale();
    }
    private void NewGame()
    {
        SceneManager.LoadScene("New_Game");
    }
    public void Pause_Game()
    {

        pause_Panel.SetActive(true);
        Time.timeScale = 0;

    }
    public void Reanude_Game()
    {
        pause_Panel.SetActive(false);
        Time.timeScale = 1;
    }
    void GameTimeScale()
    {
        Time.timeScale += scaleInc;
    }
    public void resetTimeScale()
    {
        CancelInvoke("GameTimeScale");
        Time.timeScale = 1;
    }
    private void MyRecord()
    {
        if (NewRecord())
        {
            newRecordText.SetActive(true);
            PlayerPrefs.SetInt("COIN", pj.Coins);
        }
        int record = PlayerPrefs.GetInt("COIN");
        RecordText.text = "record: " + record.ToString();
    }
    private bool NewRecord()
    {
        int record = PlayerPrefs.GetInt("COIN");
        return pj.Coins > record;
    }
}
