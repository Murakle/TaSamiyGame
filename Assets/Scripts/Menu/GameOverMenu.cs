using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private GameObject UI;

    private bool GB;

    private bool GO;

    private float GBTime;
    private float GOTime;

    private void Awake()
    {
        GB = false;
        GBTime = Time.realtimeSinceStartup;
        UI = GameObject.FindWithTag("UI");
    }

    private void Start()
    {
        GB = false;
        GBTime = Time.realtimeSinceStartup;
        UI = GameObject.FindWithTag("UI");
    }

    private void OnEnable()
    {
        GB = false;
        GBTime = Time.realtimeSinceStartup;
        UI = GameObject.FindWithTag("UI");
    }

    public void BeginGame()
    {
        UI = GameObject.FindWithTag("UI");
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
        Application.LoadLevel(Application.loadedLevel);
        
//        GBTime = Time.realtimeSinceStartup;
//        GB = true;
//        UI.transform.GetChild(2).gameObject.SetActive(true);
//        UI.transform.GetChild(3).gameObject.SetActive(false);
//        UI.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
//        UI.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void FixedUpdate()
    {
        if (!GB)
        {
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            mainCamera.orthographicSize = Leap(5, 2, 10);
            UI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().fontSize = Leap(50, 100, 10);
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

    public void EndGame()
    {
        GB = false;
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