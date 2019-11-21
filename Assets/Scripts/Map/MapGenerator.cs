using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject MapPart;
    [SerializeField] private int offset;
    private Transform[,] map;
    private int cx, cy;
    private float w, h;


    void Awake()
    {
        w = MapPart.GetComponent<RectTransform>().rect.width;
        h = MapPart.GetComponent<RectTransform>().rect.height;
        cx = 0;
        cy = 0;
        map = new Transform[offset * 2 + 1, offset * 2 + 1];
        Generate();
    }

    private void FixedUpdate()
    {
        var PlayerPos = getPlayerPos();
        if (!PlayerPos.equal(cx, cy))
        {
            Clear(PlayerPos.First - cx, PlayerPos.Second - cy);
//            cx = PlayerPos.First;
//            cy = PlayerPos.Second;
//
//            GenerateMissing();
        }
    }

    private GameObject GenerateMapPart(int x, int y)
    {
        Vector3 pos = new Vector3(x * w, y * h, 1);
        var MapPartClone = Instantiate(MapPart);
        MapPartClone.GetComponent<RectTransform>().position = pos;
        MapPartClone.transform.SetParent(gameObject.transform);
        MapPartClone.transform.name = "MapPart(" + x + "," + y + ")";
        MapPartClone.GetComponent<EnemyGenerator>().setXY(x, y);
        return MapPartClone;
    }

    private void Generate()
    {
        for (int i = -offset; i <= offset; i++)
        {
            for (int j = -offset; j <= offset; j++)
            {
                map[i + offset, j + offset] = GenerateMapPart(cx + i, cy + j).transform;
            }
        }
    }

    private void GenerateMissing()
    {
        for (int i = -offset; i <= offset; i++)
        {
            for (int j = -offset; j <= offset; j++)
            {
                if (map[i + offset, j + offset] == null)
                {
                    GenerateMapPart(cx + i, cy + j);
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

    private void Clear(int dx, int dy)
    {
        cx += dx;
        cy += dy;
        Transform[,] mapClone = new Transform[offset * 2 + 1, offset * 2 + 1];
        for (int i = -offset; i <= offset; i++)
        {
            for (int j = -offset; j <= offset; j++)
            {
                int ddx = (dx - i) * dx;
                int ddy = (dy - j) * dy;

                if (ddx == 2 || ddy == 2)
                {
                    Destroy(map[i + offset, j + offset].gameObject);
                    Debug.Log("Destroyed " + (i) + " " + (j));
                    map[i + offset, j + offset] = null;
                }

                mapClone[i + offset, j + offset] = map[i + offset, j + offset];
            }
        }

        for (int i = -offset; i <= offset; i++)
        {
            for (int j = -offset; j <= offset; j++)
            {
                if (0 <= i + offset + dx && i + offset + dx < offset * 2 + 1
                                         && 0 <= j + offset + dy && j + offset + dy < offset * 2 + 1)
                {
                    map[i + offset, j + offset] = mapClone[i + offset + dx, j + offset + dy];
                    Debug.Log("Moved " + i + " " + j);
                }
                else
                {
                    map[i + offset, j + offset] = GenerateMapPart(cx + i, cy + j).transform;
                }
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