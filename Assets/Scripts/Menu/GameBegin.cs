using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBegin : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private GameObject UI;

    private bool GB;

    private float GBTime;

    

    public void BeginGame()
    {
        UI = GameObject.FindWithTag("UI");
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
        GBTime = Time.realtimeSinceStartup;
        GB = true;
        UI.transform.GetChild(2).gameObject.SetActive(true);
        UI.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        UI.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(false);
//        UI.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void FixedUpdate()
    {
        if (!GB)
        {
            Time.timeScale = 0.000001f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            return;
        }

        if (Time.realtimeSinceStartup - GBTime >= 2)
        {
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f;
            mainCamera.orthographicSize = 5.0f;
            UI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().fontSize = 50;
            gameObject.SetActive(false);
        }


        mainCamera.orthographicSize = Leap(2, 5, 2);
        UI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().fontSize = Leap(100, 50, 2);
    }

    private float Leap(float a, float b, float t)
    {
        if (Time.realtimeSinceStartup - GBTime >= t)
        {
            return b;
        }

        return a + (Time.realtimeSinceStartup - GBTime) / t * (b - a);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}