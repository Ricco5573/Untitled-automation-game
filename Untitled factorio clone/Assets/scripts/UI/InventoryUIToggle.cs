using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIToggle : MonoBehaviour
{
   private bool isEnabled = false;
    [SerializeField] private GameObject InventoryUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((KeyCode.I)))
            if (!isEnabled)
                EnableInventoryUI();
            else
                DisableInventoryUI();
    }
    private void EnableInventoryUI()
    {
        InventoryUI.SetActive(true);
        isEnabled = true;
    }
    private void DisableInventoryUI()
    {
        InventoryUI.SetActive(false);
        isEnabled = false;
    }

}
