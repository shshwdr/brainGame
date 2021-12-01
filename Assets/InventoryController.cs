using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public Transform content;
    InventoryCell[] cells;
    // Start is called before the first frame update
    void Awake()
    {
        cells = GetComponentsInChildren<InventoryCell>();
        foreach (var cell in cells)
        {
            cell.initDark();
        }

        //content.gameObject.SetActive(false);
        EventPool.OptIn("inventoryChanged", onUpdateInventory);
        EventPool.OptIn("slotChanged", UpdateSlots);
    }

    void UpdateSlots()
    {
        var maxSlots = Inventory.Instance.maxCount;
        if (maxSlots > cells.Length)
        {
            Debug.LogError("slot is larger than " + cells.Length);
        }
        int i;
        for (i= 0;i< maxSlots; i++)
        {

            var cell = cells[i];
            cell.gameObject.SetActive(true);
        }
        for (; i < cells.Length; i++)
        {

            var cell = cells[i];
            cell.gameObject.SetActive(false);
        }
    }

    void onUpdateInventory()
    {
        content.gameObject.SetActive(true);
        int i = 0;
        for (; i < Mathf.Min(Inventory.Instance.itemList.Count, cells.Length); i++)
        {
            //cells[i].gameObject.SetActive(true);
            cells[i].init(i,Inventory.Instance.itemList[i]);
        }
        for (; i < cells.Length; i++)
        {
            //cells[i].gameObject.SetActive(false);
            cells[i].initDark();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
