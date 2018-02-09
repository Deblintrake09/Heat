using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {
    public GameObject boxFire;
    public GameObject explosionPrefab;
    public int scorevalue = 200;
    public float temp = 0f;
    public float vidaMax= 50f;
    float vida;
    public float energyEmission = .2f;
    public float energyResistance = 1f;
    public float tempDispersion = 1f;
    public bool dinamita = false;
    GameStats gm;
    
    
	// Use this for initialization
	void Awake()
    {
        vida = vidaMax;
        gm = FindObjectOfType<GameStats>().GetComponent<GameStats>();
    }
	
	// Update is called once per frame
	void Update () {
        TempManager();
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Crate")
        {
            if( temp > 0) {
                other.gameObject.SendMessage("RecieveEnergy", temp * energyEmission);
            }

        }
    }

    public void RecieveEnergy(float energy)
    {
        temp += energy*Time.deltaTime / 2;
    }

    void TempManager()
    {
        if(temp > 0)
        {
        vida -= temp*Time.deltaTime;
        temp = Mathf.Clamp(temp-tempDispersion * Time.deltaTime,0f,1000f);
            if (vida <= vidaMax *.75) { boxFire.SetActive(true); }
        }
        if ( temp <0) temp = Mathf.Clamp(temp + tempDispersion * Time.deltaTime, -100f, 0f);
        
        if (vida<=0)
        {
            
            
            
            if (dinamita)
            {
                Player pl = FindObjectOfType<Player>().GetComponent<Player>();
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                pl.SendMessage("Die");
                gm.loseGame = true;
            }
            gm.UpdateScore(200);
            Destroy(gameObject);
        }
    }
}
