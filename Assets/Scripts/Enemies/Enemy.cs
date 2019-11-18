using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] private float shootingFrequency; // 
        [SerializeField] private GameObject Bullet;

        private float lastShoot;

        private void Awake()
        {
            lastShoot = Time.time;
            shootingFrequency += Random.Range(-0.5f, 0.1f);
        }

        private void FixedUpdate()
        {
            if (Time.time - lastShoot > shootingFrequency)
            {
                Shoot();
                lastShoot = Time.time;
            }
        }

        public void Shoot()
        {
            var BulletCopy = Instantiate(Bullet, transform.position, Quaternion.identity);
            BulletCopy.transform.SetParent(GameObject.Find("Bullets").transform);
        }
    }
}