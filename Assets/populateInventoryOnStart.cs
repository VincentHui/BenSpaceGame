using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct resourceSlot { public string key; public int value ; }


public class populateInventoryOnStart : MonoBehaviour
{
    public List<resourceSlot> initialValues = new List<resourceSlot>();
    // Start is called before the first frame update
    void Start()
    {
        var newWallet = new Dictionary<string, int>();
        foreach (var slot in initialValues) {
            newWallet.Add( slot.key, slot.value);
        }
        gameObject.setInventory(newWallet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
