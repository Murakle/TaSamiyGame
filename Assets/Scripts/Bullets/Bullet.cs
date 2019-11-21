using System;
using UnityEngine;
using Random = UnityEngine.Random;


public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    
    protected Transform player;

    public abstract void Awake();
    

    public abstract void FixedUpdate();


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.CompareTag("Player"))
        {
            if (!player.GetComponent<Player>().Hit(damage) && !player.GetComponent<Player>().GO())
            {
                player.GetComponent<Player>().GameOver();
                Destroy(gameObject, 0.04f);
            }
            else
            {
                Destroy(gameObject, 0.04f);
            }
        }
    }

    protected double VectorLength(Vector2 a)
    {
        return Math.Sqrt(a.x * a.x + a.y * a.y);
    }
}