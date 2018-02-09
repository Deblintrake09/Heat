using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public GameObject heatParticles;
    public GameObject coldParticles;
    public GameObject flameOff;
    public GameObject hotLight;
    public GameObject coldLight;
    public GameObject weaponCollGO;
    CapsuleCollider weaponColl; // Collider del Lanzallamas
    public float speed = 5f; // velocidad de movimiento
    bool firingHeat = false;
    bool firingCold = false;
    bool isDead = false;
    public float heatEnergy = 5f;
    public float coldEnergy = -5f;
    ParticleSystem ps =null;
    PauseMenu pMenu; 

    Rigidbody rb;
    Animator anim; // referencia al animador

    // Use this for initialization
	void Start ()
    {
        
        weaponColl = weaponCollGO.GetComponent<CapsuleCollider>();
        pMenu = FindObjectOfType<PauseMenu>().GetComponent<PauseMenu>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        HandleAttackInput();  
    }


    private void FixedUpdate()
    {
        HandleMoveInput();
    }

    void Die()
    {
        ps.Stop();
        anim.SetBool("Die", true);
        isDead = true;
    }

    //Controla el Movimiento del personaje
    void HandleMoveInput()
    {
        if (isDead == false)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                rb.velocity = new Vector3(0f, 0f, Input.GetAxis("Horizontal") * speed);
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                anim.SetBool("ismoving", true);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                rb.velocity = new Vector3(0f, 0f, Input.GetAxis("Horizontal") * speed);
                transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                anim.SetBool("ismoving", true);
            }
            else if (Input.GetAxis("Vertical") > 0)
            {
                rb.velocity = new Vector3(Input.GetAxis("Vertical") * speed, 0f, 0f);
                transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                anim.SetBool("ismoving", true);
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                rb.velocity = new Vector3(Input.GetAxis("Vertical") * speed, 0f, 0f);
                transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
                anim.SetBool("ismoving", true);
            }
            else
            {
                rb.velocity = Vector3.zero;
                anim.SetBool("ismoving", false);
            }
        }
    }

    //Controla Los ataques, activando y desactivando opbjetos de acuerdo a como se dispara
    void HandleAttackInput()
    {
        if (!pMenu.GameIsPaused && isDead == false)
        {
            if (Input.GetButton("Fire1") && firingCold == false)
            {
                FireHeat();
            }
            if (Input.GetButton("Fire2") && firingHeat == false)
            {
                FireCold();
            }
            if (Input.GetButtonUp("Fire1") && !Input.GetButton("Fire2"))
            {

                firingHeat = false;
                weaponColl.enabled = false;
                ps.Stop();
                heatParticles.SetActive(false);
                hotLight.SetActive(false);

            }
            else if (Input.GetButtonUp("Fire2") && !Input.GetButton("Fire1"))
            {
                firingCold = false;
                weaponColl.enabled = false;
                ps.Stop();
                coldParticles.SetActive(false);
                coldLight.SetActive(false);

            }
        }
    }

    /*Activa los componentes necesarios cuando se dispara el boton Primario - 
     * TODO Generar Funciones genericas para que se puedan cambiar los tipos de energía en cada boton y no tener multiples funciones*/
    void FireHeat()
    {
        heatParticles.SetActive(true);
        ps = heatParticles.GetComponent<ParticleSystem>();
        ps.Play();
        weaponColl.enabled = true;
        firingHeat = true;
        hotLight.SetActive(true);

    }

    /*Activa los componentes necesarios cuando se dispara el boton Secundario - 
     * TODO Generar Funciones genericas para que se puedan cambiar los tipos de energía en cada boton y no tener multiples funciones*/
    void FireCold()
    {
        coldParticles.SetActive(true);
        ps = coldParticles.GetComponent<ParticleSystem>();
        ps.Play();
        weaponColl.enabled = true;
        firingCold = true;
        coldLight.SetActive(true);

    }
    
    //Maneja Collider del Lanzador de Energía - TODO ver posibilidad de cambiarlo a Raycast
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Crate")
        {
            if (firingHeat) { other.gameObject.SendMessage("RecieveEnergy", heatEnergy); }
            else if (firingCold) { other.gameObject.SendMessage("RecieveEnergy", coldEnergy); }

        }
    }
}
