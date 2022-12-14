using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Buyable
{
    public Dictionary<string, int> Cost { get; set; }
}

public struct Transaction {
    public Dictionary<string, int> to;
    public Dictionary<string, int> from;

}
public class Merchant {

    public static Dictionary<string, int> buy(Buyable buyable, Dictionary<string,int> wallet){
        var newWallet = sum(new Dictionary<string, int>[]{wallet, buyable.Cost});
        return newWallet;
    }



    public static Transaction transfer(
        Dictionary<string, int> to,
        Dictionary<string, int> from,
        Dictionary<string, int> values
        )
    {
        var newTo = sum(new Dictionary<string, int>[] { to, values });
        var newFrom = sum(new Dictionary<string, int>[] { from, scale(values, -1) });
        return new Transaction() {to= newTo, from = newFrom };
    }

    public static Dictionary<string, int> sum(Dictionary<string, int> [] wallets, bool invert=false)
    {
        var resultDict = new Dictionary<string, int>();
        foreach(var wallet in wallets){
            foreach(var itm in wallet){
                if(resultDict.ContainsKey(itm.Key)) {
                    resultDict[itm.Key] += itm.Value * (invert? -1:1);
                }
                else
                {
                    resultDict.Add(itm.Key, itm.Value);
                }
            }
        }
        return resultDict;
    }

    public static bool isWalletsEmpty(Dictionary<string, int>[] wallets) {
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


    public static bool inTheBlack(Dictionary<string, int> wallet){
        return wallet.Where(w=>w.Value <0).Count() == 0;
    }
    public static List<string> currencies(Dictionary<string, int> [] wallets){
        var resultList = new List<string>();
        foreach(var wallet in wallets){
            foreach(var itm in wallet){
                if(resultList.Contains(itm.Key))continue;
                resultList.Add(itm.Key);
            }
        }
        return resultList;
    }
    public static Dictionary<string, int> scale(Dictionary<string, int> wallet, int scale) {
        return wallet.Select(w => new KeyValuePair<string,int>(w.Key, w.Value*scale)).ToDictionary(key=>key.Key,value=> value.Value);
    }
}