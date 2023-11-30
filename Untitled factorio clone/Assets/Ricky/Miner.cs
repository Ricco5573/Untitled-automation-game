using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Miner : Building
{

    private OreNode ironNode;
    private bool farming;
    // Update is called once per frame
  protected override void Update()
    {
        base.Update();
        if(placing == true) { 
        RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit))
            {
                Tile tile = hit.collider.gameObject.GetComponent<Tile>();
                if (tile.GetAlive())
                {
                    if (tile.GetNode() == 2)
                    {
                        canPlace = true;
                        ironNode = tile.GetOreNode();
                    }
                }
                else
                {
                    canPlace = false;
                }
            }
            }
        else
        {
            if (!farming)
            {
                StartCoroutine(farm());
            }
            
        }
        }

    private IEnumerator farm()
    {
        farming = true;
        while(ironNode.getAmount() > 0)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            if (ironNode.Decrease())
            {
                // spawn iron object

            }
        }
    }
}
