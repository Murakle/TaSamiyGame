using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        GetComponent<Text>().text = getMana();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Text>().text = getMana();
    }

    public string getMana()
    {
        return "Mana(" + player.GetComponent<Player>().getcMana() + "/" + player.GetComponent<Player>().getMaxMana() +
               ")";
    }
}