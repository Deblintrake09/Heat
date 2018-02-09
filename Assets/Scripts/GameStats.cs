using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {

    public int Level = 0;
    public static GameStats instance { get; private set; }
    public int score = 0;
    float time;
    TextMeshProUGUI tmTime;
    TextMeshProUGUI tmScore;
    public bool loseGame = false;
    private LevelManager lm;



    private void Awake()
    {
        if (instance != null && instance != this)
        { Destroy(gameObject); }
        else { instance = this; }
        DontDestroyOnLoad(gameObject);
        tmScore = GameObject.Find("ScoreNumber").GetComponent<TextMeshProUGUI>();
        tmTime = GameObject.Find("TimeLeftNumber").GetComponent<TextMeshProUGUI>();
        lm = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
       
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        time = Random.Range(150, 180);
        tmScore = GameObject.Find("ScoreNumber").GetComponent<TextMeshProUGUI>();
        tmTime = GameObject.Find("TimeLeftNumber").GetComponent<TextMeshProUGUI>();
        lm = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();

    }

    // Use this for initialization
    void Start () {
        Level = 0;
        time = Random.Range(150, 180);
    }
    void OnDisable()
    {
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // Update is called once per frame
    void Update ()
    {
        if(SceneManager.GetActiveScene().buildIndex==1 && !loseGame)
        {
            time -= Time.deltaTime;
            tmTime.text = Mathf.CeilToInt(time).ToString();
        }
        tmScore.text = score.ToString();

	}

    public void UpdateScore(int scorePlus)
    {
        if (!loseGame) { score += scorePlus; }
    }

    public void LevelWon()
    {
        UpdateScore(Mathf.CeilToInt(time) * 50);
    }
    
    //TODO Implementar Game Over
    void GameOver()
    {
    }


    public void GameRestart()
    {
        score = 0;
        Level = 1;
        time = 0;
        loseGame = false;
        lm.LoadLevel("MainGame");
    }
}
