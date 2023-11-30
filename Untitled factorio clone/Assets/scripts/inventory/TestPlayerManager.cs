using UnityEngine;

public class TestPlayerManager : MonoBehaviour
{
	private Inventory myInventory;
	private void Awake() => myInventory = gameObject.GetComponent<Inventory>();

	private void Start()
	{
		myInventory.AddItems(Items.GENERATOR, 2);
		myInventory.AddItems(Items.MINER, 2);
		myInventory.AddItems(Items.SMELTER, 1);
		myInventory.AddItems(Items.PUMP, 1);
	}
}
