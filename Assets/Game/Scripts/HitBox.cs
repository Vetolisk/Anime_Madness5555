using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Health health;
    public GameObject UITargetHair;
    public void OnRaycastHit(RaycastWeapon weapon,Vector3 direction){
        Debug.Log("Ouch!");
        health.TakeDamage(weapon.damage,direction);
        UITargetHair.SetActive(true);
        Invoke("OfHair",0.1f);
    }
    public void OfHair(){
        UITargetHair.SetActive(false);
    }
}
