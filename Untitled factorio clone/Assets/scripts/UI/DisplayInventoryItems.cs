using UnityEngine;
using TMPro;

public class DisplayInventoryItems : MonoBehaviour
{
	private TMP_Text myText;
	private Inventory inventory;
	[SerializeField] Items myItem;

	private void Awake()
	{
		myText = GetComponent<TMP_Text>();
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
	}

	private void OnEnable() => myText.text = inventory.GetStringItemAmount(myItem);
}
