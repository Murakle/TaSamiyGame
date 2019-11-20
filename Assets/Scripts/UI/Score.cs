using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        GetComponent<TextMeshProUGUI>().text = getScore();
    }

    void FixedUpdate()
    {
        GetComponent<TextMeshProUGUI>().text = getScore();
    }

    public string getScore()
    {
        return "Score\n" + player.GetComponent<Player>().getTotalScore();
    }
}