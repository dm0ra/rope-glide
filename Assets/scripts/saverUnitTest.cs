


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class saverUnitTest
{
    [Test]
    public void DB_UnitTest()
    {

        string ret;
        DataSaver.SaveData<string>("Hello", "testFile");

        ret = DataSaver.LoadData<string>("testFile");

        // Assert
        Assert.That(ret, Is.EqualTo("Hello"));
  
    }
}


