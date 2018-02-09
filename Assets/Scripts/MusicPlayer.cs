using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    static MusicPlayer instance = null;

    AudioSource aS;

    public AudioClip[] clips;


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip clipToLoad = clips[SceneManager.GetActiveScene().buildIndex];
        if (aS.clip != clipToLoad) { 
        aS.clip = clipToLoad;
        aS.Play();
        }
    }


    void OnDisable()
    {

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Use this for initialization
    void Awake ()
    {
        aS = GetComponent<AudioSource>();

	    if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
	}

}
