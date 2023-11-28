using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private GameObject[,] grid = new GameObject[1000, 1000];
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
        StartCoroutine(GenerateGrid(renderdistance, new Vector3(-250 * tilesize, 0, -250 * tilesize)));
    }

    private IEnumerator GenerateGrid(int Size, Vector3 start)
    {

        Debug.Log("Start Generation");
        for (int v = 0; v <= Size; v++)
        {

            Debug.Log("Row " + v);

            for (int h = 1; h <= Size; h++)
            {

                GameObject tiled = Instantiate(tile, new Vector3(start.x + (h * tilesize), 0, start.z + (v * tilesize)), Quaternion.identity);
                grid[v, h] = tiled;

            }
            yield return new WaitForSecondsRealtime(0.002f);
        }
        yield return new WaitForEndOfFrame();
    }
}
