using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Material grass, water, dead;
    private GridManager grid;
    private int x, z;
    private bool isWater;
    [SerializeField]
    private bool alive;
    [SerializeField]
    private bool isGrowing;
    private Coroutine growth, inst, burn;
    private bool instantiated;
    private bool inArea;
    private int timer;
    [SerializeField]
    private GameObject waterCol;
    private GameObject node, building;
    [SerializeField]
    private GameObject coalNode, ironNode, oilNode;
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
        if (RandomChance(0.5f))
        {
            int randomint = UnityEngine.Random.Range(1, 3);
            switch (randomint){
                case 1: node =  Instantiate(coalNode, this.gameObject.transform.position, Quaternion.identity);  break;
                case 2: node =  Instantiate(ironNode, this.gameObject.transform.position, Quaternion.identity); break;
                case 3: node =  Instantiate(oilNode, this.gameObject.transform.position, Quaternion.identity); break;
                default: break;

            }
            node.GetComponent<OreNode>().Instantiate();
        }

        StopCoroutine(inst);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Fire" && alive)
        {
            burn = StartCoroutine(burning());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Fire" && alive && burn != null)
        {
            StopCoroutine(burn);
        }
    }
    private IEnumerator burning()
    {
        Debug.Log("Burning");
        yield return new WaitForSecondsRealtime(0.2f);
        alive = false;
        this.gameObject.GetComponent<Renderer>().material = dead;
        StopCoroutine(Grow());
        isGrowing = false;
        StopCoroutine(burn);

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
            waterCol.SetActive(false) ;

        }
        else
        {


            isWater = true;
            alive = false;
            this.gameObject.GetComponent<Renderer>().material = water;
            waterCol.SetActive(true);
        }
    }

    private IEnumerator Grow()
    {
        isGrowing = true;
        while (isGrowing && alive)
        {
            yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(25, 55));
            Debug.Log("Growing: " + alive);
            if (alive)
            {
                grid.Spread(x, z);
            }
        }

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
    public int GetNode()
    {
        if (node != null)
        {
            return node.GetComponent<OreNode>().GetType();
        }
        else
        {
            return 0;
        }
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
        if (alive)
        {
            this.gameObject.GetComponent<Renderer>().material = grass;

        }
    }
    public OreNode GetOreNode()
    {
        return node.GetComponent<OreNode>();
    }
}
