using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{

    private GameObject IMGO;
    private ItemManager itemMan;

    private void Awake()
    {
        IMGO = GameObject.Find("ItemManager");
        if (IMGO != null) itemMan = IMGO.GetComponent<ItemManager>();
    }

    public void LoadEnding()
    {
        SceneManager.LoadScene("Ending");
    }
    public void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void LoadTitle()
    { 
        SceneManager.LoadScene("Title");
    }
    public void LoadH2P()
    {
        SceneManager.LoadScene("H2P");
    }
    public void Resume()
    {
        if(IMGO != null) itemMan.paused = false;
    }
    public void Pause()
    {
        if (IMGO != null) itemMan.paused = true;
    }
}
