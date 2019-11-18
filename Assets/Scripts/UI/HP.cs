using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        GetComponent<Text>().text = getHP();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Text>().text = getHP();
    }

    public string getHP()
    {
        return "HP(" + player.GetComponent<Player>().getcHP() + "/" + player.GetComponent<Player>().getMaxHP() +
               ")";
    }
}