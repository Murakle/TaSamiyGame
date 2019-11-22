using System;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public int maxHP;
    [SerializeField] public int maxMana;
    [SerializeField] public float manaRegen;
    [SerializeField] public float HPRegen;
    [SerializeField] public float PointPerSecond;
    [SerializeField] public Camera mainCamera;
    [SerializeField] private Transform scoreUI;


    private int controlType = 2;

    private float GameOverStartTime;
    private bool gameOver;

    private float speedX;
    private float speedY;
    private float curMana;
    private float curHP;

    private int totalScore;
    private int additionalScore;
    private int timeScore;
    private float BeginTime;
    private Vector3 dirAngle;

    private Vector2 beginPos;
    private Vector2 direction;
    private bool dirChoosen;
    private Vector3 curDir;

    void Start()
    {
        gameOver = false;
        BeginTime = Time.realtimeSinceStartup;
        totalScore = additionalScore = timeScore = 0;
        curMana = 0;
        curHP = maxHP;
        curDir = new Vector3(0, 0, 1);
        dirAngle = new Vector3(0, 0, 0);
    }

    public void Reload()
    {
        gameOver = false;
        GameObject.Find("Map");
    }

    private void FixedUpdate()
    {
        if (gameOver)
        {
//            GameOverFixedUpdate();
            return;
        }

#if UNITY_STANDALONE
        Vector3 newAngle = new Vector3(0, 0, 0);

        bool up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool move = (up ^ down) || (left ^ right);
        if (up && down)
        {
            up = false;
            down = false;
        }

        if (right && left)
        {
            right = false;
            left = false;
        }

        if (up)
        {
            if (left)
            {
                newAngle = new Vector3(0, 0, -135);
            }
            else if (right)
            {
                newAngle = new Vector3(0, 0, 135);
            }
            else
            {
                newAngle = new Vector3(0, 0, 180);
            }
        }
        else if (down)
        {
            if (left)
            {
                newAngle = new Vector3(0, 0, -45);
            }
            else if (right)
            {
                newAngle = new Vector3(0, 0, 45);
            }
            else
            {
                newAngle = new Vector3(0, 0, 0);
            }
        }
        else
        {
            if (left)
            {
                newAngle = new Vector3(0, 0, -90);
            }
            else if (right)
            {
                newAngle = new Vector3(0, 0, 90);
            }
            else
            {
                newAngle = dirAngle;
            }
        }
        transform.rotation = Quaternion.Euler(newAngle);
        dirAngle = newAngle;
        transform.Translate(Vector2.down * (move ? speed : 0));
#elif UNITY_IOS || UNITY_ANDROID
        if (controlType == 0 && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    beginPos = touch.position;
                    dirChoosen = false;
                    break;
                case TouchPhase.Moved:
                    direction = touch.position - beginPos;
                    break;
                case TouchPhase.Ended:
                    dirChoosen = true;
                    break;
            }
        }

        if (controlType == 1 && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                direction = touch.deltaPosition;
            }

            dirChoosen = true;
        }

        if (controlType == 2 && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                beginPos = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                direction = touch.position - beginPos;
                direction /= 10;
            }

            dirChoosen = true;
        }

        if (dirChoosen)
        {
            float angle = Vector2.SignedAngle(Vector2.down, direction);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            int w = Screen.width, h = Screen.height;
            var newPos = transform.position;
            if (controlType <= 1)
            {
                newPos.x += direction.x / w * speed * 10 * 5;
                newPos.y += direction.y / h * speed * 10 * 5;
            }
            else
            {
                newPos.x += direction.normalized.x / 15;
                newPos.y += direction.normalized.y / 15;
            }

            transform.position = newPos;
            dirChoosen = false;
        }


#endif


        curMana = Math.Min(curMana + manaRegen * Time.deltaTime, maxMana);
        curHP = Math.Min(curHP + HPRegen * Time.deltaTime, maxHP);
        timeScore = (int) (Time.timeSinceLevelLoad * PointPerSecond);
        speedX = speedY = 0;
    }


    public bool Hit(float damage)
    {
        curHP -= damage;
        return curHP > 0;
    }

    private bool dif(float a, float b)
    {
        if (a <= 0 && b >= 0)
        {
            return true;
        }

        return false;
    }

    private bool equal(float a, float b)
    {
        if (Math.Abs(a - b) <= 10)
        {
            return true;
        }

        return false;
    }

    public int getcMana()
    {
        return (int) curMana;
    }

    public int getTotalScore()
    {
        totalScore = additionalScore + timeScore;
        return totalScore;
    }

    public int getMaxMana()
    {
        return maxMana;
    }

    public int getcHP()
    {
        return (int) curHP;
    }

    public int getMaxHP()
    {
        return maxHP;
    }

    public bool GO()
    {
        return gameOver;
    }

    public void GameOver()
    {
        gameOver = true;
        GameObject.FindWithTag("UI").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.FindWithTag("UI").transform.GetChild(3).gameObject.SetActive(true);
    }
}