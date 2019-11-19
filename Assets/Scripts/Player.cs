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

    private float speedX;
    private float speedY;
    private float curMana;
    private float curHP;
    private float dirAngle;


    void Start()
    {
        curMana = 0;
        curHP = maxHP;
    }

    private void FixedUpdate()
    {
#if UNITY_STANDALONE

        float newAngle = 0;

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
                newAngle = -135;
            }
            else if (right)
            {
                newAngle = 135;
            }
            else
            {
                newAngle = 180;
            }
        }
        else if (down)
        {
            if (left)
            {
                newAngle = -45;
            }
            else if (right)
            {
                newAngle = 45;
            }
            else
            {
                newAngle = 0;
            }
        }
        else
        {
            if (left)
            {
                newAngle = -90;
            }
            else if (right)
            {
                newAngle = 90;
            }
            else
            {
                newAngle = dirAngle;
            }
        }
#elif UNITY_IOS || UNITY_ANDROID
//todo
#endif

        transform.Rotate(0, 0, -(dirAngle - newAngle));
        dirAngle = newAngle;

        transform.Translate(Vector2.down * (move ? speed : 0));
        curMana = Math.Min(curMana + manaRegen * Time.deltaTime, maxMana);
        curHP = Math.Min(curHP + HPRegen * Time.deltaTime, maxHP);

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

    public int getcMana()
    {
        return (int) curMana;
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