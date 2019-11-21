using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class Boom : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void Shoot()
    {
        var Boomerang = Instantiate(Bullet, transform.position, Quaternion.identity);
        Boomerang.transform.SetParent(GameObject.Find("Bullets").transform);
    }
}