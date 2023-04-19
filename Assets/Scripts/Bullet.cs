using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   [SerializeField] private float damage;

    public float Damage { get => damage; set => damage = value; }
}
