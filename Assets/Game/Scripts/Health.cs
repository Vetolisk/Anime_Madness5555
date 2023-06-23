using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    
    public float maxHealth;
    public Animator anim;
    NavMeshAgent agent;
    [HideInInspector]
     public float currentHealth;

     AiLocomotion ai;
    // Start is called before the first frame update
    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
        ai=GetComponent<AiLocomotion>();
        var rigidBodies =GetComponent<Rigidbody>();
        currentHealth=maxHealth;
    }
    public void TakeDamage(float amount, Vector3 direction){
        currentHealth-=amount;
        if(currentHealth<=0.0f){
            agent.enabled=false;
            ai.enabled=false;
            Die();
        }
    }
    
    public void Die(){
        anim.SetTrigger("Dead");
        Invoke("DestroyObj",2);
    }
    public void DestroyObj(){
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
