using System;
using UnityEngine;
using Random = UnityEngine.Random;


public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] protected float speed;
    protected Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        float pi = (float) Math.PI;
        float angle = Random.Range(-pi, pi);
        transform.Rotate(0, 0, angle * 180 / pi);
    }

    public abstract void FixedUpdate();


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.CompareTag("Player"))
        {
//            Debug.Log("popal_blyattt");
            player.GetComponent<Player>().Hit(damage);
            Destroy(gameObject);
        }
    }
    
    protected double VectorLength(Vector2 a)
    {
        return Math.Sqrt(a.x * a.x + a.y * a.y);
    }

}