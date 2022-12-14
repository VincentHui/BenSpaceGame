using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class inventoryExtenstions {
    public static void addInventory(this GameObject obj, string name, int amount) {
        var _inventory = obj.GetComponent<inventory>();
        if (_inventory == false) {
            Debug.Log("no inventory");
            return;
        }

        _inventory._inventory = Merchant.buy(
            new Buyable { Cost = new Dictionary<string, int>() {[name]=amount } },
            _inventory._inventory);

    }

    public static Dictionary<string, int> getInventory(this GameObject obj) {
        return obj.GetComponent<inventory>()._inventory;
    }

    public static Dictionary<string, int> setInventory(this GameObject obj, Dictionary<string, int> toSet)
    {
        return obj.GetComponent<inventory>()._inventory = toSet;
    }

    public static void startTransaction(this GameObject obj,
        Dictionary<string, int> values,
        GameObject from,
        GameObject to) {

        var newValues = Merchant.transfer(
            to.getInventory(),
            from.getInventory(),
            values);

        to.setInventory(newValues.to);
        from.setInventory(newValues.from);
    }
}


public class inventory : MonoBehaviour, ISerializationCallbackReceiver
{
    public List<resourceSlot> resources = new List<resourceSlot>();
    public Dictionary<string, int> _inventory = new Dictionary<string, int>();

    public void OnAfterDeserialize()
    {
        _inventory = new Dictionary<string, int>();

        //var newWallet = new Dictionary<string, int>();
        foreach (var slot in resources)
        {
            _inventory.Add(slot.key, slot.value);
        }
    }

    public void OnBeforeSerialize()
    {

        resources.Clear();
        foreach (var kvp in _inventory)
        {
            resources.Add(new resourceSlot {key = kvp.Key, value=kvp.Value });
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
