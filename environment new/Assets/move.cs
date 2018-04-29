using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class move : MonoBehaviour {

    public Image healthsource;
    public Image map;
    public int noenemy;
    private float health = 100f;
    private float damge = 10f;
    private CharacterController controller;
    private AudioSource source;
    private bool isgrounded;
    public float walkingspeed;
    public float rotationspeed;
    public AudioClip clip;
    public Camera backcamera;
    public float range = 10f;
    private float speed;
    private float rspeed;
    Animator animator;
    
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        
    }
    private void footstaps()
    {

        source.PlayOneShot(clip);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        isgrounded = true;
    }


    void controllone(string action)
    {
        switch (action)
        {
            case "walk":
                animator.SetBool("walk", true);
                animator.SetBool("attack", false);
                animator.SetBool("idle", false);
                animator.SetBool("run", false);
                animator.SetBool("back", false);
                animator.SetBool("death", false);
                break;
            case "idle":
                animator.SetBool("walk", false);
                animator.SetBool("attack", false);
                animator.SetBool("idle", true);
                animator.SetBool("run", false);
                animator.SetBool("back", false);
                animator.SetBool("death", false);
                break;
           case "attack":
                animator.SetBool("walk", false);
                animator.SetBool("attack", true);
                animator.SetBool("idle", false);
                animator.SetBool("run", false);
                animator.SetBool("back", false);
                animator.SetBool("death", false);
                break;
            case "run":
                animator.SetBool("walk", false);
                animator.SetBool("attack", false);
                animator.SetBool("idle", false);
                animator.SetBool("run", true);
                animator.SetBool("back", false);
                animator.SetBool("death", false);
                break;
            case "back":
                animator.SetBool("walk", false);
                animator.SetBool("attack", false);
                animator.SetBool("idle", false);
                animator.SetBool("run", false);
                animator.SetBool("back", true);
                animator.SetBool("death", false);
                break;
            case "death":
                animator.SetBool("walk", false);
                animator.SetBool("attack", false);
                animator.SetBool("idle", false);
                animator.SetBool("run", false);
                animator.SetBool("death", true);
                animator.SetBool("back", false);
                break;
        }


    }
    
    void Update()
    {
        if (isgrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                controllone("walk");
                speed = walkingspeed;
                if(Input.GetKey(KeyCode.W)&&Input.GetKey(KeyCode.LeftShift))
                {
                    controllone("run");
                    speed = walkingspeed + 0.3f;
                }
               
            }
             else if (Input.GetKey(KeyCode.Space))
            {
                controllone("run");

                speed = walkingspeed;


            }
            else if (Input.GetKey(KeyCode.S))
            {
                controllone("back");

                speed = walkingspeed;
            }
            else
            {
                controllone("idle");
                speed = 0;
            }

            if (Input.GetKey(KeyCode.A))
            {
                rspeed = rotationspeed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rspeed = rotationspeed;
            }
            else { rspeed = 0; }
        }

        var z = Input.GetAxis("Vertical") * speed;
        var y = Input.GetAxis("Horizontal") * rspeed;
        transform.Translate(0, 0, z);
        transform.Rotate(0, y, 0);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            shoot();
        }
       
    }
    void shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(backcamera.transform.position, backcamera.transform.forward, out hit, range))
        {
            
            enemy e = hit.transform.GetComponent<enemy>();
            if (e != null)
            {
                controllone("attack");
                e.setdamge(damge);
                
            }

        }
    }
    public void sdamge(float amount)
    {
        health -= amount;
        healthsource.fillAmount = health / 100f;
        
        if (health <= 0)
        {
            StartCoroutine(die());
           
        }
    }
    IEnumerator die()
    {
        animator.SetBool("walk", false);
        animator.SetBool("attack", false);
        animator.SetBool("idle", false);
        animator.SetBool("run", false);
        animator.SetBool("death", true);
        animator.SetBool("back", false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
    public void win()
    {
        noenemy -= 1;
        if(noenemy==0)
        {
            SceneManager.LoadScene("win");
        }

    }
    
}
