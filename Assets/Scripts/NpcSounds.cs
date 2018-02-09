using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSounds : MonoBehaviour {

    public AudioClip[] deathSounds;
    public GameObject soundPrefab;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    public void DeathSound()
    {
        soundPrefab.GetComponent<AudioSource>().clip = deathSounds[Random.Range(0, 3)];
        Instantiate(soundPrefab, transform.position, Quaternion.identity);
    }
}
