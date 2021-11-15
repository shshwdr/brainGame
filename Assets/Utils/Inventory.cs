using LitJson;
using PixelCrushers.DialogueSystem;
using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : Singleton<Inventory>
{
    //public Dictionary<string, ItemInfo> itemDict = new Dictionary<string, ItemInfo>();
    public List<string> itemList = new List<string>();
    public int maxCount = 7;
    public bool canAddItem(string itemName)
    {

        return !(itemList.Count>=maxCount);
        //if (itemValueDict.ContainsKey(itemName))
        //{
        //    return true;
        //}
        //if (itemValueDict.Count < inventoryUnlockedCellCount)
        //{
        //    return true;
        //}
        //return false;
    }


    //public void updateSelectedItem(string name)
    //{
    //    selectedItemName = name;

    //    DialogueLua.SetVariable("holdingItem", name);
    //}

    //public void sendGift()
    //{
    //    if (selectedItemName == "")
    //    {
    //        Debug.LogError("you send what a you send");
    //        return;
    //    }
    //    //sometimes should not be able to send
    //    consumeItem(selectedItemName, 1);

    //    selectedItemName = "";

    //    EventPool.Trigger("inventoryChanged");
    //}

    //public void select(int index)
    //{
    //    selectedItemIndex = index;
    //    EventPool.Trigger("inventoryChanged");
    //}

    //public void addItem(string itemName)
    //{
    //    addItem(itemName, 1);
    //}
    public void addItem(string itemName)
    {
        if (!canAddItem(itemName))
        {
            Debug.LogError("Your bag is full");
            //DialogueManager.ShowAlert("Your bag is full");
            return;
        }
        itemList.Add(itemName);

        EventPool.Trigger("inventoryChanged");
    }

    public bool canConsumeItems(Dictionary<string, int> items)
    {
        foreach(var item in itemList)
        {
            if (items.ContainsKey(item))
            {
                items[item] -= 1;
            }
        }
        foreach(var pair in items)
        {
            if (pair.Value > 0)
            {
                return false;
            }
        }
        return true;
    }

    public void consumeItem(int index)
    {
        itemList.RemoveAt(index);
        EventPool.Trigger("inventoryChanged");
    }

    public void consumeItems(Dictionary<string,int> items)
    {

        foreach(var pair in items)
        {
            for(int i = 0; i < pair.Value; i++)
            {

                itemList.Remove(pair.Key);
            }
        }


        //if(!itemDict.ContainsKey(itemName) || itemDict[itemName].amount < value)
        //{
        //    if (CheatManager.Instance.hasUnlimitResource)
        //    {
        //        return;
        //    }
        //    Debug.LogError("not enough item to consume");
        //    return;
        //}
        //itemDict[itemName].amount -= value;

        //if (itemValueDict[itemName] <= 0)
        //{
        //    itemValueDict.Remove(itemName);
        //}
        EventPool.Trigger("inventoryChanged");
    }


    //public int itemAmount(string itemName)
    //{
    //    return itemDict.ContainsKey(itemName) ? itemDict[itemName].amount : 0;
    //}
    //public bool hasItemAmount(string itemName,int amount)
    //{
    //    if (CheatManager.Instance.hasUnlimitResource)
    //    {
    //        return true;
    //    }
    //    return itemDict.ContainsKey(itemName) && itemDict[itemName].amount >= amount;
    //}
    //public bool hasItem(string itemName)
    //{
    //    return hasItemAmount(itemName, 1);
    //}
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        //if (startInventory)
        //{

        //    for (int i = 1; i <= 7; i++)
        //    {
        //        if (Input.GetKeyDown(KeyCode.Alpha0 + i))
        //        {
        //            consumeItem(i - 1);
        //        }
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    foreach(var key in itemDict.Keys)
        //    {
        //        //if (!itemDict[key].noItemCollected)
        //        {
        //            itemDict[key].amount += 1;
        //        }
        //    }
        //}
    }
}
