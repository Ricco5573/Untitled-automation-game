using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class OreNode : GridObject
{
    [SerializeField]
    private int type; //1 = iron, 2 = coal, 3 = oil
    [SerializeField]
    private int amount;
    private bool beset;
    [SerializeField]
    private TextMeshProUGUI left;

    public void Instantiate()
    {
        amount = Random.Range(600, 800);
        left.text = amount.ToString();

    }
    public bool Decrease()
    {
        if(amount > 0)
        {
            amount--;
            left.text = amount.ToString();
            return true;

        }
        else
        {
            return false;
        }
    }

    public void SetBeset(bool set)
    {
        beset = set;
    }
    public bool GetBeset() {

        return beset;
    }

    public void SetType(int ore) 
    {
        type = ore;
    }
    public int getAmount()
    {
        return amount;
    }
    public new int GetType()
    {
        return type;
    }
}
