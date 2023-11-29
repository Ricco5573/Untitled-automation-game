using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Material grass, water;
    private GridManager grid;
    private int x, z;
    private bool isWater;
    private bool alive;
    private bool isGrowing;
    private Coroutine growth, inst;
    private bool instantiated;
    private bool inArea;
    private int timer;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void FixedUpdate()
    {

        if (inArea)
        {

            if (!instantiated)
            {
                inst = StartCoroutine(Instantiate());
            }
            if (alive && !isGrowing)
            {
                growth = StartCoroutine(Grow());
            }
            else if (!alive && isGrowing)
            {
                StopCoroutine(Grow());
                isGrowing = false;
            }
        }
        

    }   

    private IEnumerator Instantiate()
    {
        instantiated = true;
        yield return new WaitForEndOfFrame();
        int waterNearby = 0;
        if (grid.IsWater(x - 1, z - 1))
        {
            waterNearby++;
        }
        if (grid.IsWater(x, z - 1))
        {
            waterNearby++;
        }
        if (grid.IsWater(x + 1, z - 1))
        {
            waterNearby++;
        }
        if (grid.IsWater(x - 1, z))
        {
            waterNearby++;
        }
        if (grid.IsWater(x + 1, z))
        {
            waterNearby++;
        }
        if (grid.IsWater(x - 1, z + 1))
        {
            waterNearby++;
        }
        if (grid.IsWater(x, z + 1))
        {
            waterNearby++;
        }
        if (grid.IsWater(x + 1, z + 1))
        {
            waterNearby++;
        }


        switch (waterNearby) {
            case 0:
                GrassWater(!RandomChance(0.1f));

                break;

            case 1:
                GrassWater(!RandomChance(60));

                break;
            case 2:
                GrassWater(!RandomChance(40));
                break;

            case 3:
                GrassWater(!RandomChance(60));
                break;
            case 4:
                GrassWater(!RandomChance(70));
                break;
            default:
                GrassWater(!RandomChance(75));


                break;

        }
        StopCoroutine(inst);
    }
    private void LateUpdate()
    {
        if (timer <= 25)
        {
            timer++;
        }
        else
        {
            Transform pPos = grid.GetPlayerPos();
            if (pPos.position.x - this.gameObject.transform.position.x <= 30 && pPos.position.x - this.gameObject.transform.position.x >= -30 && pPos.position.z - this.gameObject.transform.position.z <= 30 && pPos.position.z - this.gameObject.transform.position.z >= -30)
            {
                inArea = true;
            }
            else
            {
                inArea = false;
            }
            timer = 0;
        }

        }
        private void GrassWater(bool Grass)
    {

        if (Grass)
        {
            isWater = false;
            alive = true;
            this.gameObject.GetComponent<Renderer>().material = grass;
        }
        else
        {


            isWater = true;
            alive = false;
            this.gameObject.GetComponent<Renderer>().material = water;
        }
    }

    private IEnumerator Grow()
    {
        isGrowing= true;
        yield return new WaitForSecondsRealtime(20);
        grid.Spread(x, z);

    }
    private bool RandomChance(float chance)
    {
        int seed = UnityEngine.Random.Range(0, 10000);
        float hack = chance * 100f;

        if (hack >= seed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetGridManager(GridManager gridd, int X, int Z)
    {
        grid = gridd;
        x = X;
        z = Z;
    }

    public bool GetWater()
    {
        return isWater;
    }
    public bool GetAlive()
    {
        return alive;
    }
    public void SetAlive(bool isAlive)
    {
        alive = isAlive;    
    }

}
