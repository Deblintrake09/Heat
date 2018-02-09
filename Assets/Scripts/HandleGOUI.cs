using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGOUI : MonoBehaviour {

    GameObject gOverPanel;
    GameStats gm;

    // Use this for initialization
    void Start () {
        gm = FindObjectOfType<GameStats>().GetComponent<GameStats>();
        gOverPanel = GameObject.Find("GameOverPanel").gameObject;
        gOverPanel.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (gm.loseGame == true) Invoke("ActivateGOPanel", 2f);
    }
    public void ActivateGOPanel()
    {
            gOverPanel.SetActive(true);
	}

    public void RestartButton()
    {
        gm.GameRestart();
    }


}
