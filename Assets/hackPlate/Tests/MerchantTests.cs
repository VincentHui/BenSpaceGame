using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MerchantTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void SimpleMerchantSumTests()
    {
        var result = Merchant.sum(new Dictionary<string, int>[] {
            new Dictionary<string, int>(){ ["gold"]=1 },
            new Dictionary<string, int>(){ ["gold"]=2 }
        });
        Assert.AreEqual(result, new Dictionary<string, int> { ["gold"]=3 });

        var resultNegative = Merchant.sum(new Dictionary<string, int>[] {
            result,
            new Dictionary<string, int>(){ ["gold"]=-4 }
        });

        Assert.AreEqual(resultNegative, new Dictionary<string, int> { ["gold"] = -1 });

        var MultipleCurrencies = Merchant.sum(new Dictionary<string, int>[] {
            resultNegative,
            new Dictionary<string, int>(){ ["silver"]=2 },
            new Dictionary<string, int>(){ ["fish"]=5 },
            new Dictionary<string, int>() {["gold"]=10 }
        });

        Assert.AreEqual(MultipleCurrencies, new Dictionary<string, int> { ["gold"] = 9, ["fish"]=5, ["silver"]=2 });
    }

    // A Test behaves as an ordinary method
    [Test]
    public void SimpleMerchantBuyTests()
    {

        //var outputState = new List<string>();
        var wallet = new Dictionary<string, int>() { ["silver"]=100, ["JADE"] = 21 };
        var items = new List<Item<float>> {
            new Item<float>(){
                Type= "FIRE_SWORD",
                Effect =(state)=> new Dictionary<string, int>(){["FIRE"]=1 },
                Cost= new Dictionary<string, int>(){ ["silver"]=10 }
            },
            new Item<float>(){
                Type= "ECHO_SWORD",
                Effect =(state)=>new Dictionary<string, int>(){["ECHO"]=1 },
                Cost = new Dictionary<string, int>(){ ["JADE"]=20 }
            }
        };
        var newWallet = Merchant.buy(items[0], wallet);
        Assert.AreEqual(newWallet, new Dictionary<string, int> { ["silver"] = 90, ["JADE"]=21 });

        newWallet = Merchant.add(items[0], newWallet);
        Assert.AreEqual(newWallet, new Dictionary<string, int> { ["silver"] = 90, ["FIRE_SWORD"]=1, ["JADE"] = 21 });

        var effectsLedger = Merchant.Effects<float>(items, newWallet, 0f);
        Assert.AreEqual(effectsLedger, new Dictionary<string, int>() { ["FIRE"]=1 });

        var finalWallet = Merchant.buy(items[1], newWallet);
        finalWallet = Merchant.add(items[1] , finalWallet, 2);
        Assert.AreEqual(finalWallet, new Dictionary<string, int> { ["silver"] = 90, ["FIRE_SWORD"] = 1, ["JADE"]=1 , ["ECHO_SWORD"]=2});
        var lastEffectLedger = Merchant.Effects<float>(items, finalWallet, 0f);
        Assert.AreEqual(lastEffectLedger, new Dictionary<string, int>() { ["FIRE"] = 1 , ["ECHO"] = 2});
    }
}
