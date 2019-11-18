using System;
using UnityEngine;

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


    void Start()
    {
        curMana = 0;
        curHP = maxHP;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            speedY = speed;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            speedX = -speed;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            speedY = -speed;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            speedX = speed;
        }

        transform.Translate(speedX, speedY, 0);
        curMana = Math.Min(curMana + manaRegen * Time.deltaTime, maxMana);
        curHP = Math.Min(curHP + HPRegen * Time.deltaTime, maxHP);

        speedX = speedY = 0;
    }

    public void Hit(float damage)
    {
        curHP -= damage;
        if (curHP <= 0)
        {
            Debug.Log("GameOVER");
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