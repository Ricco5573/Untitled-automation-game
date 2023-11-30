using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool IsInitialized { get; private set; } = false;

    private Dictionary<Items, int> inventory = new Dictionary<Items, int>();

	public void InitializeInventory()
	{
        if (IsInitialized)
            return;
        else if (!IsInitialized)
		{
            inventory.Clear();
            foreach (Items item in Enum.GetValues(typeof(Items)))
                inventory.Add(item, 0);

            IsInitialized = true;
		}
	}

    public void AddItems(Items item, int amount) => inventory[item] += amount;
    public void RemoveItems(Items item, int amount)
    {
        inventory[item] -= amount;
        if (inventory[item] < 0)
            inventory[item] = 0;
    }
    public void DebugInventoryContents()
	{
        foreach (KeyValuePair<Items, int> items in inventory)
            Debug.Log("Key: " + items.Key.ToString() + " Value: " + items.Value.ToString() + "\n" );
	}
    
    public string GetStringItemAmount(Items item)
	{
        string amount = inventory[item].ToString();
        return amount;
	}
    public int GetIntItemAmount(Items item)
	{
        int amount = inventory[item];
        return amount;
	}

    public bool CanItemBeRemovedFromInventory(Items item, int amountToBeRemoved)
	{
        bool canBeRemoved;
        if (inventory[item] - amountToBeRemoved >= 0)
            canBeRemoved = true;
        else
            canBeRemoved = false;
        
        return canBeRemoved;
	}

    private void Awake() => InitializeInventory();
}
