using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    Bullet bullet;
    private void Awake()
    {
        bullet = GetComponent<Bullet>();
    }
    private void OnCollisionEnter(Collision other)
    {
        Enemy enemy = other.collider.GetComponentInParent<Enemy>();
        Debug.Log(other.collider.name);
        if (enemy != null) { Debug.Log(enemy.name); enemy.Damage(bullet); bullet.gameObject.SetActive(false); }
    }
   

}
