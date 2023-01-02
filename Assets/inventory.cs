using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class ItemPouch
{

    public static readonly Dictionary<string, Item<GameObject>> pouch = new Dictionary<string, Item<GameObject>>()
    {
        ["CollectibleAsteroid"] = new Item<GameObject>()
        {
            Type = "CollectibleAsteroid",
            Cost = new Dictionary<string, int>() { },
            Effect = (obj) => {
                return new Dictionary<string, int>()
                {
                    ["CollectibleWeightEffect"] = 1
                };
            }
        },
        ["DropAsteroidItem"] = new Item<GameObject>()
        {
            Type = "DropAsteroidItem",
            Cost = new Dictionary<string, int>() { ["CollectibleAsteroid"]= 1 }
        }

    };
}

public static class inventoryExtenstions {
    public static void addInventory(this GameObject obj, string name, int amount, Action Effect = null) {
        var _inventory = obj.GetComponent<inventory>();
        if (_inventory == false) {
            Debug.Log("no inventory");
            return;
        }

        //_inventory._inventory = Merchant.buy(
        //    new Buyable { Cost = new Dictionary<string, InventoryValue>() {[name] = {value=amount, Effect =Effect} } },
        //    _inventory._inventory);

    }

    public static void BuyItem(this GameObject obj, Item<GameObject> toBuy) {
        var inventory = getInventory(obj);
        var newInventory = Merchant.buy(toBuy,inventory);
        if (!Merchant.inTheBlack(newInventory)) {
            Debug.LogError("NOT ENOUGH TO BUY");
            return;
        }
        setInventory(obj,Merchant.add(toBuy,newInventory));

    }

    public static Dictionary<string, int> getInventory(this GameObject obj) {
        var inventory = obj.GetComponent<inventory>();
        //if (inventory == null) {
        //    inventory = obj.AddComponent<inventory>()._wallet;
        //}
        return inventory == null ? obj.AddComponent<inventory>()._wallet : inventory._wallet;
    }

    public static Dictionary<string, int> setInventory(this GameObject obj, Dictionary<string, int> toSet)
    {
        return obj.GetComponent<inventory>()._wallet = toSet;
    }

    public static void startTransaction(this GameObject obj,
        GameObject from,
        GameObject to) {

        //var newValues = Merchant.transfer(
        //    to.getInventory(),
        //    from.getInventory(),
        //    values);

        //to.setInventory(newValues.to);
        //from.setInventory(newValues.from);
    }
}




public class inventory : MonoBehaviour, ISerializationCallbackReceiver
{
    public List<resourceSlot> wallet = new List<resourceSlot>();
    public List<resourceSlot> effectsLedger = new List<resourceSlot>();
    public Dictionary<string, int> _wallet = new Dictionary<string, int>();
    public Dictionary<string, int> _effectsLedger = new Dictionary<string, int>();

    public void OnAfterDeserialize()
    {
        _wallet = new Dictionary<string, int>();

        //var newWallet = new Dictionary<string, int>();
        foreach (var slot in wallet)
        {
            _wallet.Add(slot.key, slot.value);
        }
        foreach (var slot in effectsLedger)
        {
            _effectsLedger.Add(slot.key, slot.value);
        }
    }

    public void OnBeforeSerialize()
    {

        wallet.Clear();
        foreach (var kvp in _wallet)
        {
            wallet.Add(new resourceSlot {key = kvp.Key, value=kvp.Value });
        }
        effectsLedger.Clear();
        foreach (var kvp in _effectsLedger)
        {
            effectsLedger.Add(new resourceSlot { key = kvp.Key, value = kvp.Value });
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _oldInventory = _wallet;
    }
    private Dictionary<string, int> _oldInventory;
    // Update is called once per frame
    void Update()
    {
        if (_oldInventory != _wallet) {
            _oldInventory = _wallet;
            _effectsLedger = Merchant.Effects<GameObject>(ItemPouch.pouch.Values.ToList(), _wallet, gameObject);
        }
    }
}
