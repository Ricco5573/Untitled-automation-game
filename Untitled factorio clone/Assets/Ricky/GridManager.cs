using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Tile[,] grid = new Tile[1500, 1500];
    [SerializeField]
    private float tilesize;
    [SerializeField]
    private int renderdistance;
    [SerializeField]
    private GameObject tile;
    [SerializeField]    
    private Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        StartCoroutine(GenerateGrid(renderdistance, new Vector3(-100 * tilesize, 0, -100 * tilesize)));
    }

    private IEnumerator GenerateGrid(int Size, Vector3 start)
    {

        Debug.Log("Start Generation");
        for (int v = 1; v <= Size; v++)
        {


            for (int h = 1; h <= Size; h++)
            {

                GameObject tiled = Instantiate(tile, new Vector3(start.x + (h * tilesize ), 0, start.z + (v * tilesize)), Quaternion.identity);
                grid[v, h] = tiled.GetComponent<Tile>();
                tiled.GetComponent<Tile>().SetGridManager(this, v, h);

            }
        }
        Debug.Log("Finish generation");
        yield return new WaitForEndOfFrame();
    }

    public Vector3 GetNearestTile(Transform position)
    {
        float x = 100 + (position.position.x / tilesize);
        float z = 100 + (position.position.z / tilesize);


        return grid[(int)z, (int)x].gameObject.transform.position;

    }

    public bool IsWater(int x, int y)
    {
        if (x > 0 && y > 0 && grid[x,y] != null)
        {
            return grid[x, y].GetWater();

        }
        else
        {
            return false;
        }
    }

    public Transform GetPlayerPos()
    {
        return Player;
    }
    public void Spread(int x, int y)
    {
        if (x > 0 && y > 0)
        {

            if (grid[x - 1, y] != null&& !grid[x-1, y].GetAlive() && !grid[x - 1, y].GetWater() )
            {
                grid[x - 1, y].SetAlive(true);
            }
            else if (grid[x + 1, y] != null && !grid[x + 1, y].GetAlive() && !grid[x + 1, y].GetWater() )
            {
                grid[x + 1, y].SetAlive(true);
            }
            else if (grid[x, y - 1] != null && !grid[x, y - 1].GetAlive() && !grid[x, y - 1].GetWater() )
            {
                grid[x, y - 1].SetAlive(true);
            }
            else if (grid[x, y + 1] != null && !grid[x, y + 1].GetAlive() && !grid[x, y + 1].GetWater())
            {
                grid[x,y + 1].SetAlive(true);
            }
        }
    }
}
