using UnityEngine.UI;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Health health;

    [SerializeField]
    private  GameObject UITargetHair;
    
    private void Start() {
        UITargetHair = GameObject.FindGameObjectWithTag("Hair");
        UITargetHair.gameObject.GetComponent<Image>().enabled=false;
    }
    public void OnRaycastHit(RaycastWeapon weapon,Vector3 direction){
        
        health.TakeDamage(weapon.damage,direction);
        Debug.Log("Ouch!");
        UITargetHair.gameObject.GetComponent<Image>().enabled=true;
        Invoke("OfHair",0.1f);
    }
    public void OfHair(){
         UITargetHair.gameObject.GetComponent<Image>().enabled=false;
    }
}
