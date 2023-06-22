using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    
    public float maxHealth;
    public Animator anim;
    [HideInInspector]
     public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        var rigidBodies =GetComponent<Rigidbody>();
        currentHealth=maxHealth;
    }
    public void TakeDamage(float amount, Vector3 direction){
        currentHealth-=amount;
        if(currentHealth<=0.0f){
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
