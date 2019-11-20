using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject MapPart;
    [SerializeField] private int offset;

    private int cx, cy;
    private float w, h;

    void Start()
    {
        w = MapPart.GetComponent<RectTransform>().rect.width;
        h = MapPart.GetComponent<RectTransform>().rect.height;
        cx = 0;
        cy = 0;
        GenerateMissing();
    }

    private void FixedUpdate()
    {
        var PlayerPos = getPlayerPos();
        if (!PlayerPos.equal(cx, cy))
        {
            cx = PlayerPos.First;
            cy = PlayerPos.Second;

            Clear();
            GenerateMissing();
        }
    }

    private void GenerateMapPart(int x, int y)
    {
        Vector3 pos = new Vector3(x * w, y * h, 1);
        var MapPartClone = Instantiate(MapPart);
        MapPartClone.GetComponent<RectTransform>().position = pos;
        MapPartClone.transform.SetParent(gameObject.transform);
        MapPartClone.GetComponent<EnemyGenerator>().setXY(x, y);
    }

    private void Generate()
    {
        for (int i = cx - offset; i <= cx + offset; i++)
        {
            for (int j = cy - offset; j <= cy + offset; j++)
            {
                GenerateMapPart(i, j);
            }
        }
    }

    private void GenerateMissing()
    {
        for (int i = cx - offset; i <= cx + offset; i++)
        {
            for (int j = cy - offset; j <= cy + offset; j++)
            {
                if (!FindInChildrens(i, j))
                {
                    GenerateMapPart(i, j);
                }
            }
        }
    }

    private bool FindInChildrens(int x, int y)
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.name.Equals("MapPart(" + x + "," + y + ")"))
            {
                return true;
            }
        }

        return false;
    }

    private void Clear()
    {
        foreach (Transform child in gameObject.transform)
        {
            if (!good(child.GetComponent<EnemyGenerator>().getX(), child.GetComponent<EnemyGenerator>().getY()))
            {
                Debug.Log("Cleared " + child.GetComponent<EnemyGenerator>().getX() + " " +
                          child.GetComponent<EnemyGenerator>().getY());
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    private bool good(int xx, int yy)
    {
        return xx <= cx + offset && xx >= cx - offset && yy >= cy - offset && yy <= cy + offset;
    }

    private Pair<int, int> getPlayerPos()
    {
        var player = GameObject.FindWithTag("Player");
        return new Pair<int, int>((int) (player.transform.position.x / w), (int) (player.transform.position.y / h));
    }
}