using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public int maxHP;
    [SerializeField] public int maxMana;
    [SerializeField] public float manaRegen;
    [SerializeField] public float HPRegen;
    [SerializeField] public float PointPerSecond;
    private float speedX;
    private float speedY;
    private float curMana;
    private float curHP;
    private int totalScore;
    private int additionalScore;
    private int timeScore;
    private Vector3 dirAngle;


    void Start()
    {
        totalScore = additionalScore = timeScore = 0;
        curMana = 0;
        curHP = maxHP;
        dirAngle = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
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
#elif UNITY_IOS || UNITY_ANDROID
//todo
#endif


        transform.rotation = Quaternion.Euler(newAngle);
        dirAngle = newAngle;
        transform.Translate(Vector2.down * (move ? speed : 0));


        curMana = Math.Min(curMana + manaRegen * Time.deltaTime, maxMana);
        curHP = Math.Min(curHP + HPRegen * Time.deltaTime, maxHP);
        timeScore = (int) (Time.time * PointPerSecond);
        speedX = speedY = 0;
    }


    public void Hit(float damage)
    {
        curHP -= damage;
        if (curHP <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
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
}