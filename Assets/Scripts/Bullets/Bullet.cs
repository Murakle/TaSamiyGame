using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        float pi = (float) Math.PI;
        float angle = Random.Range(-pi, pi);
        transform.Rotate(0, 0, angle * 180 / pi);
    }

    private void FixedUpdate()
    {
        Vector2 dis = player.position - transform.position;
        if (VectorLength(dis) > 20)
        {
            Destroy(gameObject);
        }

        transform.Translate(Time.deltaTime * speed * Vector2.right);
    }

    private double VectorLength(Vector2 a)
    {
        return Math.Sqrt(a.x * a.x + a.y * a.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.CompareTag("Player"))
        {
            Debug.Log("popal_blyattt");
            player.GetComponent<Player>().Hit(damage);
            Destroy(gameObject);
        }
    }
}