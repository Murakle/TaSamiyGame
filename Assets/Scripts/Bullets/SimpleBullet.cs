using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bullets
{
    public class SimpleBullet : Bullet
    {
        [SerializeField] protected float speed;

        public override void Awake()
        {
            player = GameObject.FindWithTag("Player").transform;
            float pi = (float) Math.PI;
            float angle = Random.Range(-pi, pi);
            transform.Rotate(0, 0, angle * 180 / pi);
        }

        public override void FixedUpdate()
        {
            Vector2 dis = player.position - transform.position;
            if (VectorLength(dis) > 20)
            {
                Destroy(gameObject);
            }

            transform.Translate(Time.deltaTime * speed * Vector2.left);
        }
    }
}