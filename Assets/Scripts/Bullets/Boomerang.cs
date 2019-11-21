using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boomerang : Bullet
{
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float distance;
    [SerializeField] private float width;
    [SerializeField] private float flyTime;
    private float shootedTime;

    private Vector3 direction;

    // Start is called before the first frame update
    public override void Awake()
    {
        shootedTime = Time.timeSinceLevelLoad;
        player = GameObject.FindWithTag("Player").transform;
        float pi = (float) Math.PI;
        float angle = Random.Range(-pi, pi);
        direction = new Vector3(0, 0, angle * 180 / pi);

        transform.Rotate(0, 0, angle * 180 / pi);
        StartCoroutine(Throw());
    }


    public override void FixedUpdate()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        if (Time.timeSinceLevelLoad - shootedTime > flyTime)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Throw()
    {
        Vector3 pos = transform.position;
        Quaternion q = Quaternion.Euler(direction);
        float timer = 0.0f;
        while (timer < flyTime)
        {
            float t = Mathf.PI * 2.0f * timer / flyTime - Mathf.PI / 2.0f;
//            Debug.Log(t);
            float x = width * Mathf.Cos(t);
            float y = distance * Mathf.Sin(t);
            if (t >= 0 && t <= Mathf.PI)
            {
                float qt = (t / Mathf.PI - 0.5f) * 2;
                y -= -0.5f * (qt * qt - 1);
//                Debug.Log(2 * (qt * qt - 1));
            }

//            Debug.Log(y);

            Vector3 v = new Vector3(x, y + distance, 0);
            GetComponent<Rigidbody2D>().MovePosition(pos + (q * v));
            timer += Time.deltaTime;
            yield return null;
        }


        GetComponent<Rigidbody2D>().angularVelocity = 0;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().rotation = 0;
        GetComponent<Rigidbody2D>().MovePosition(pos);
        yield return null;
    }
}