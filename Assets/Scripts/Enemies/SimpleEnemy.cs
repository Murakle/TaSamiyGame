using UnityEngine;

namespace Enemies
{
    public class SimpleEnemy : Enemy
    {
        public override void Shoot()
        {
            var SimpleBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            SimpleBullet.transform.SetParent(GameObject.Find("Bullets").transform);
        }
    }
}