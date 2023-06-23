using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaycastWeapon : MonoBehaviour
{
    class Bullet{
        public float time;
        public Vector3 initialposition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }
    public bool isFiring=false;
    public int fireRate =25;
    [SerializeField]
    public float damage =10;
    public float bulletSpeed =1000.0f;
    public float bulletDrop = 0.0f;
    public ParticleSystem[] _muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;
    public Transform _raycastOrigin;

    public Transform raycastDestination;
    
    Ray _ray;
    RaycastHit _hitInfo;
    float accumulatedTime;
    List<Bullet> bullets= new List<Bullet>();
    float maxLifeTime=3.0f;
    Vector3 GetPosition(Bullet bullet){
        // p + v*t + 0.5 * g * t * t 
        Vector3 gravity = Vector3.down *bulletDrop;
        return (bullet.initialposition)+(bullet.initialVelocity * bullet.time) + (0.5f * gravity * bullet.time * bullet.time);
    }
    Bullet CreateBullet(Vector3 position,Vector3 velocity){
        Bullet bullet =new Bullet();
        bullet.initialposition=position;
        bullet.initialVelocity=velocity;
        bullet.time=0.0f;
        bullet.tracer = Instantiate(tracerEffect, _ray.origin,Quaternion.identity);
        bullet.tracer.AddPosition(position);
        return bullet;
        
    }
    public void StartFiring(){
        isFiring=true;
        accumulatedTime=0.0f;
        FireBullte();
       
    }
    public void UpdateFiring(float deltaTime){
        accumulatedTime +=deltaTime;
        float fireInterval = 1.0f/fireRate;
        while (accumulatedTime>=0.0f)
        {
             FireBullte();
             accumulatedTime-=fireInterval;
        }
    }
    public void UpdateBullets(float deltaTime){
        SimulateBullets(deltaTime);
        DestroyBullets();
    }
    void SimulateBullets(float deltaTime){
        bullets.ForEach(bullet => {
            Vector3 p0 = GetPosition(bullet);
            bullet.time +=deltaTime;
            Vector3 p1 =GetPosition(bullet);
            RaycastSegment(p0,p1,bullet);
        });
    }
    void DestroyBullets(){
        bullets.RemoveAll(bullet => bullet.time >=maxLifeTime );
    }
    void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet){
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        _ray.origin = start;
        _ray.direction=direction;
        if(Physics.Raycast(_ray, out _hitInfo,distance)){
            //Debug.DrawLine(_ray.origin,_hitInfo.point,Color.red, 1.0f);
            hitEffect.transform.position = _hitInfo.point;
            hitEffect.transform.forward=_hitInfo.normal;
            hitEffect.Emit(1);
            bullet.tracer.transform.position=_hitInfo.point;
            bullet.time = maxLifeTime;
            var hitBox = _hitInfo.collider.GetComponent<HitBox>();
            if(hitBox){
                hitBox.OnRaycastHit(this,_ray.direction);
                Debug.Log(hitBox.name);
            }
        }else{
            bullet.tracer.transform.position=end;
        }
    }
    private void FireBullte(){
         foreach (var particle in _muzzleFlash)
        {
            particle.Emit(1);    
        }
        Vector3 velocity = (raycastDestination.position - _raycastOrigin.position).normalized *bulletSpeed;
        var bullet =CreateBullet(_raycastOrigin.position,velocity);
        bullets.Add(bullet);
       
        
    }
    public void StopFiring(){
        isFiring=false;

    }
}
