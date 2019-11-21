using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] private float shootingFrequency; // 
        [SerializeField] protected GameObject Bullet;

        private float lastShoot;

        private void Awake()
        {
            lastShoot = Time.timeSinceLevelLoad;
            shootingFrequency += Random.Range(-0.5f, 0.1f);
        }

        private void FixedUpdate()
        {
            if (Time.timeSinceLevelLoad - lastShoot > shootingFrequency)
            {
                Shoot();
                lastShoot = Time.timeSinceLevelLoad;
            }
        }

        public abstract void Shoot();
    }
}