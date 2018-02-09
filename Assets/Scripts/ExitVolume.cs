using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitVolume : MonoBehaviour {

    
    GameStats gm;

    private void Start()
    {
        gm = FindObjectOfType<GameStats>().GetComponent<GameStats>();
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            gm.Level++;
            gm.LevelWon();
            Invoke("ReloadLevel", 1f);
            
        }
    }

}
