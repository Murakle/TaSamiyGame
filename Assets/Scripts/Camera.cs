using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;
    [SerializeField] private float damping;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        offset = transform.position - player.transform.position;
    }


    void FixedUpdate()
    {
        Vector3 target = new Vector3(player.transform.position.x - offset.x, player.transform.position.y + offset.y,
            transform.position.z);

        Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);

        transform.position = currentPosition;
    }
}