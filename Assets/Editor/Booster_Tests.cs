using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class Booster_Tests
{
    [Test]
    public void booster_Use_Test()
    {
        var booster = new Booster();
        int prefuel = booster.GetFuel();
        int fuelUseRate = booster.GetFuelUseRate();
        int expectedFuel = prefuel -= fuelUseRate;
        booster.AccelerateYVelocity();
        Assert.That(expectedFuel == booster.GetFuel());
    }
}
