using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pump : Building
{

    private OreNode oilNode;
    private bool farming;
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (placing == true)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit))
            {
                Tile tile = hit.collider.gameObject.GetComponent<Tile>();
                if (tile.GetAlive())
                {
                    if (tile.GetNode() == 3)
                    {
                        canPlace = true;
                        oilNode = tile.GetOreNode();
                        if (!oilNode.GetBeset())
                        {
                            indicator.GetComponent<Renderer>().material = green;
                        }
                    }
                }
                else
                {
                    canPlace = false;
                    indicator.GetComponent<Renderer>().material = red;

                }
            }
        }
        else
        {
            if (!farming)
            {
                Destroy(indicator.gameObject);
                StartCoroutine(farm());
            }

        }
    }

    private IEnumerator farm()
    {
        farming = true;
        oilNode.SetBeset(true);
        while (oilNode.getAmount() > 0)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            if (oilNode.Decrease())
            {
                // spawn iron object

            }
        }
    }
}
