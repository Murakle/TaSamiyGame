using UnityEngine;

namespace Bullets
{
    public class SimpleBullet : Bullet
    {
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