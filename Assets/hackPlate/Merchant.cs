using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public struct Item<InputState>
{
    public string Type;
    public Dictionary<string, int> Cost;
    public Func<InputState, Dictionary<string, int>> Effect;
}

//public struct Transaction {
//    public Dictionary<string, InventoryValue> to;
//    public Dictionary<string, InventoryValue> from;
//}



public class Merchant {

    public static Dictionary<string, int> buy<T>(Item<T> buyable, Dictionary<string, int> wallet)
    {
        var newWallet = sum(new Dictionary<string, int>[] {
            wallet,
            scale(buyable.Cost, -1)
        });
        return newWallet;
    }

    public static Dictionary<string, int> scale(Dictionary<string, int> wallet, int scale)
    {
        return wallet.ToDictionary(key => key.Key, value => value.Value * scale);
    }

    public static Dictionary<string, int> Effects<State>(List<Item<State>> items, Dictionary<string, int> wallet, State state )
    {
        var effectsDict = new List<Dictionary<string, int>>() { };
        foreach (var item in items) {
            if (item.Effect == null) continue;
            if (!wallet.ContainsKey(item.Type)) continue;
            effectsDict.Add(scale(item.Effect(state), wallet[item.Type]));
        }
        return sum(effectsDict.ToArray());
    }

    public static Dictionary<string, int> add<T>(Item<T> buyable, Dictionary<string, int> wallet, int amount = 1) {
        return sum(new Dictionary<string, int>[] {
            wallet,
            new Dictionary<string, int>(){
                [buyable.Type]=amount
            }
        });
    }

    //public static Transaction transfer(
    //    Dictionary<string, InventoryValue> to,
    //    Dictionary<string, InventoryValue> from,
    //    Dictionary<string, InventoryValue> values
    //    )
    //{
    //    var newTo = sum(new Dictionary<string, InventoryValue>[] { to, values });
    //    var newFrom = sum(new Dictionary<string, InventoryValue>[] { from, scale(values, -1) });
    //    return new Transaction() {to= newTo, from = newFrom };
    //}

    public static Dictionary<string, int> sum(Dictionary<string, int>[] wallets, bool invert = false)
    {
        var resultDict = new Dictionary<string, int>();
        foreach (var wallet in wallets)
        {
            foreach (var itm in wallet)
            {
                if (resultDict.ContainsKey(itm.Key))
                {
                    resultDict[itm.Key] += itm.Value;
                }
                else
                {
                    resultDict.Add(itm.Key, itm.Value);
                }
            }
        }
        return resultDict;
    }

    public static bool isWalletsEmpty(Dictionary<string, int>[] wallets)
    {
        int increment = 0;
        foreach (var wallet in wallets)
        {
            foreach (var itm in wallet)
            {
                increment += itm.Value;
            }
        }
        return increment <= 0;
    }


    public static bool inTheBlack(Dictionary<string, int> wallet)
    {
        return wallet.Where(w => w.Value < 0).Count() == 0;
    }
    //public static List<string> currencies(Dictionary<string, InventoryValue> [] wallets){
    //    var resultList = new List<string>();
    //    foreach(var wallet in wallets){
    //        foreach(var itm in wallet){
    //            if(resultList.Contains(itm.Key))continue;
    //            resultList.Add(itm.Key);
    //        }
    //    }
    //    return resultList;
    //}
    //public static Dictionary<string, int> scale(Dictionary<string, int> wallet, int scale) {
    //    return wallet.Select(w => new KeyValuePair<string,int>(w.Key, w.Value*scale)).ToDictionary(key=>key.Key,value=> value.Value);
    //}
}