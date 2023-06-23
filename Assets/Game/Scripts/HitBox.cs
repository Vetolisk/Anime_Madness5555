using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Health health;
    public GameObject Cross;
    public List<GameObject> CrossDamage;
    [SerializeField]
    private  GameObject UITargetHair;
    
    private void Start() {
        
        Cross = GameObject.FindGameObjectWithTag("Cross");
        for (int i = 0; i < Cross.transform.childCount; i++)
        {
            Transform child = Cross.transform.GetChild(i);
            CrossDamage.Add(child.gameObject);
        }
        /*
        for (int i = 0; i < CrossDamage.Count; i++)
        {
        CrossDamage[i].transform.localScale=new Vector3( 0,0,0);    
        }*/
        UITargetHair = GameObject.FindGameObjectWithTag("Hair");
        UITargetHair.gameObject.GetComponent<Image>().enabled=false;
    }
    public void OnRaycastHit(RaycastWeapon weapon,Vector3 direction){
        
        health.TakeDamage(weapon.damage,direction);
        Debug.Log("Ouch!");
        for (int i = 0; i < CrossDamage.Count; i++)
        {
        CrossDamage[i].transform.localScale=new Vector3( CrossDamage[i].transform.localScale.x,health.DamageCross/100, CrossDamage[i].transform.localScale.z);    
        }
        UITargetHair.gameObject.GetComponent<Image>().enabled=true;
        Invoke("OfHair",0.1f);
    }
    public void OfHair(){
         UITargetHair.gameObject.GetComponent<Image>().enabled=false;
         for (int i = 0; i < CrossDamage.Count; i++)
        {
        CrossDamage[i].transform.localScale=new Vector3( CrossDamage[i].transform.localScale.x,0, CrossDamage[i].transform.localScale.z);    
        }
    }
}
