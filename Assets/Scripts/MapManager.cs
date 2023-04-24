using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

class Pair<T>
{
    public Pair()
    {

    }
    public Pair(T _t1, T _t2)
    {
        x = _t1;
        y = _t2;
    }

    public T x;
    public T y;
}

public class MapManager : Singleton<MapManager>
{

    const int GridWidth = 101;
    const int GridHeight = 101;

    //1=obstacle, 0=walkable
    public int[,] Grid = new int[GridWidth, GridHeight];
    public int[,] Distance = new int[GridWidth, GridHeight];
    public bool[,] IsUpdated = new bool[GridWidth, GridHeight];
    public Vector2[,] FlowField = new Vector2[GridWidth, GridHeight];

    int[][] Direction =
    {
             new int[]{ -1, 0 },
             new int[]{ 1, 0 },
             new int[]{ 0, -1 },
             new int[]{ 0, 1 }
        };

    public override void Init()
    {
        InitGrid();
    }

    private void InitGrid()
    {
        for (int x = -50; x <= 50; x++)
        {
            for (int y = -50; y <= 50; y++)
            {
                Grid[x + 50, y + 50] = 0;
                Ray ray = new Ray(new Vector3(x, 0.0f, y), Vector3.up);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (string.Compare(hitInfo.collider.gameObject.tag, "Obstacle") == 0)
                    {
                        Grid[x + 50, y + 50] = 1;
                    }
                }
            }
        }
    }

    public Vector2 GetSpeed(float x, float y)
    {
        int gx, gy;
        FindGridIndex(new Vector2(x, y), out gx, out gy);

        Vector2 speed = FlowField[gx, gy];

        if (speed == Vector2.zero)
        {
            for (int i = 0; i < 4; i++)
            {
                int newx = gx + Direction[i][0], newy = gy + Direction[i][1];
                if (IsGridWalkable(newx, newy) && FlowField[newx, newy] != Vector2.zero)
                {
                    speed = FlowField[newx, newy];
                    break;
                }
            }
        }
        else if (speed.x == 0)
        {
            if (IsGridWalkable(gx, gy + 1) ^ IsGridWalkable(gx, gy - 1))
            {
                speed = new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f).normalized;
            }
        }
        else if (speed.y == 0)
        {
            if (IsGridWalkable(gx + 1, gy) ^ IsGridWalkable(gx - 1, gy))
            {
                speed = new Vector2(0.0f, UnityEngine.Random.Range(-1.0f, 1.0f)).normalized;
            }
        }

        return speed;
        //return FlowField[gx, gy];
    }

    private void InitIsUpdated()
    {
        for (int i = 0; i < GridWidth; i++)
        {
            for (int j = 0; j < GridHeight; j++)
            {
                IsUpdated[i, j] = false;
                Distance[i, j] = -1;
            }
        }
    }

    private void FindGridIndex(Vector2 target, out int x, out int y)
    {
        if (Mathf.Abs(target.x) >= 50.5f || Mathf.Abs(target.y) >= 50.5f)
        {
            x = -1;
            y = -1;
            return;
        }
        x = Convert.ToInt32(target.x) + 50;
        y = Convert.ToInt32(target.y) + 50;
        return;
    }

    private bool IsGridWalkable(int x, int y)
    {
        if (x < 0 || x >= GridWidth || y < 0 || y >= GridHeight || Grid[x, y] == 1) return false;
        return true;
    }

    private void CalculateFlowField(int x, int y)
    {
        int[] value = new int[4] { Distance[x, y], Distance[x, y], Distance[x, y], Distance[x, y] };
        for (int i = 0; i < 4; i++)
        {
            int newx = x + Direction[i][0];
            int newy = y + Direction[i][1];

            if (IsGridWalkable(newx, newy))
            {
                value[i] = Distance[newx, newy];
            }
        }

        FlowField[x, y] = new Vector2(value[0] - value[1], value[2] - value[3]).normalized;
    }

    public void UpdateFlowField(Vector2 target)
    {
        InitIsUpdated();

        int x, y;
        FindGridIndex(target, out x, out y);
        if (x == -1 || y == -1)
        {
            Debug.Log("error! invalid target position");
        }

        //generate heatmap
        Queue<Pair<int>> q = new Queue<Pair<int>>();
        Distance[x, y] = 0;
        q.Enqueue(new Pair<int>(x, y));
        while (q.Count != 0)
        {
            var p = q.Dequeue();
            //Debug.Log("Distance[" + p.x + "," + p.y + "]:" + Distance[p.x, p.y]);

            for (int i = 0; i < 4; i++)
            {
                int newx = p.x + Direction[i][0];
                int newy = p.y + Direction[i][1];
                if (IsGridWalkable(newx, newy) && !IsUpdated[newx, newy])
                {
                    Distance[newx, newy] = Distance[p.x, p.y] + 1;
                    IsUpdated[newx, newy] = true;

                    q.Enqueue(new Pair<int>(newx, newy));
                }
            }
        }

        //calculate Flow Field
        for (int i = 0; i < GridWidth; i++)
        {
            for (int j = 0; j < GridHeight; j++)
            {
                CalculateFlowField(i, j);
            }
        }
    }
}
