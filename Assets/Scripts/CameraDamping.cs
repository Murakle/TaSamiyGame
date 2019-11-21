
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class CameraDamping : MonoBehaviour
{
    private GameObject player;

    private Vector3 offset;
    [SerializeField] private float damping;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        offset = transform.position - player.transform.position;
    }


    private void FixedUpdate()
    {
        Vector3 target = new Vector3(player.transform.position.x - offset.x, player.transform.position.y + offset.y,
            transform.position.z);

        Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);


        transform.position = currentPosition;
    }
    
}