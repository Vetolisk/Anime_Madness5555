using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    public float fuseTime = 3f; // Время до взрыва после броска
    public float explosionRadius = 5f; // Радиус взрыва
    public float explosionForce = 700f; // Сила взрывной волны
    public GameObject explosionEffect; // Эффект взрыва (частица или анимация)
    public float damage = 100f; // Урон от взрыва


    private bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", fuseTime);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        // Создаем эффект взрыва
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Находим все объекты в радиусе
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            // Применяем силу к объектам с Rigidbody
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                Vector3 explosionDir = nearbyObject.transform.position - transform.position;
                // Добавляем немного случайности
                Vector3 force = explosionDir.normalized * explosionForce;
                force += Vector3.up * 500f; // чуть вверх для эффекта "подброса"
                rb.AddForce(force);
            }

            // Наносим урон объектам с компонентом Health или подобным
            // Предположим, у объектов есть скрипт Health с методом TakeDamage()
            Health health = nearbyObject.GetComponent<Health>();
            if (health != null)
            {
                // Расчет урона по расстоянию (опционально)
                float proximity = (transform.position - nearbyObject.transform.position).magnitude;
                float effect = 1 - (proximity / explosionRadius);

                health.TakeDamageGranade(damage * effect);
            }
        }

        // Удаляем гранату после взрыва
        Destroy(gameObject);
    }
}
