using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LerpTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void LerpTestsSimplePasses()
    {
        // Use the Assert class to test conditions
        var lerpResult = LerpExtension.LerpInRange(1,2, 0.5f);
        Assert.AreEqual(lerpResult, 1.5f);
        var maxLerpResult = LerpExtension.LerpInRange(1, 2, 2f);
        Assert.AreEqual(maxLerpResult, 2f);
        var invLerpResult = LerpExtension.Invlerp(10,20, 15);
        Assert.AreEqual(invLerpResult, 0.5f);
        var maxInvLerpResult = LerpExtension.Invlerp(10, 20, 30);
        Assert.AreEqual(maxInvLerpResult, 1);
    }
}
