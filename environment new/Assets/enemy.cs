using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class enemy : MonoBehaviour {

    
    public move s;
    public Transform traget;
    NavMeshAgent agent;
    public float look = 10f;
    public float damge = 10f;
    private bool neer = false;
    private bool notdie = true;
    private float health = 30f;
    private bool seen = false;

    private AudioSource source;
    public AudioClip clip;

    public Image healthimage;

    public Animator animate;
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(attack());
	}

    private void Awake()
    {
        source = GetComponent<AudioSource>();

    }

	void Update () {
        float d = Vector3.Distance(traget.position, transform.position);
        if(d<=look)
        {
            seen = true;
           
        }
        if(seen)
        {
            animate.SetBool("run", true);
            agent.SetDestination(traget.position);
            if (d <= agent.stoppingDistance)
            {
                face();
                neer = true;

            }
            else
            {
                neer = false;
            }
        }
       
	}
    IEnumerator attack()
    {
        if (neer && notdie)
        {
            animate.Play("attack");
           
             s.sdamge(damge);
            yield return new WaitForSeconds(2f);

        }
        yield return null;
        StartCoroutine(attack());
    }
    void face()
    {
        Vector3 t = (traget.position - transform.position).normalized;
        Quaternion lookrotation = Quaternion.LookRotation(new Vector3(t.x, 0, t.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookrotation,Time.deltaTime * 5f);
    }
    void OnDrowGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, look);
    }
    
    
    public void setdamge(float amount)
    {
        health -= amount;
        healthimage.fillAmount = health / 30f;
        if (health <= 0)
        {
            notdie = false;
            StartCoroutine(die());
        }
    }

    IEnumerator die()
    {
        
        animate.SetBool("death", true);
        s.win();
        death();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    void death()
    {
        source.PlayOneShot(clip);
    }

}
