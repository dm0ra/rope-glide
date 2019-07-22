using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class DBUnitTest
{
    [Test]
    public void DB_UnitTest()
    {
        // Arrange
        //High score value
        float localHighScore = 250.32f;
        //Score value
        float localScore = 120.35f;
        //Level index value
        int localLvlIndex = 1;
        //Preview index value
        int localPrevInd = 2;
        //glider value
        int localGlider = 1;
        //booster value
        int localBooster = 1;
        //max speed
        float localSpeed = 230.05f;
        //max height
        float localHeight = 170.84f;
        //run distance
        float localDist = 250.45f;
        //number of connections
        int localNumCon = 7;
        //cash value
        int localCash = 156;

        // Act
        DB.HighScore = localHighScore;
        DB.Score = localScore;
        DB.LvlIndex = localLvlIndex;
        DB.PreviewIndex = localPrevInd;
        DB.Glider = localGlider;
        DB.Booster = localBooster;
        DB.MaxSpeed = localSpeed;
        DB.MaxHeight = localHeight;
        DB.RunDist = localDist;
        DB.RunConNum = localNumCon;
        DB.BankCash = localCash;

        float high = DB.HighScore;
        float score = DB.Score;
        int ind = DB.LvlIndex;
        int prevInd = DB.PreviewIndex;
        int glider = DB.Glider;
        int booster = DB.Booster;
        float speed = DB.MaxSpeed;
        float height = DB.MaxHeight;
        float dist = DB.RunDist;
        int runCon = DB.RunConNum;
        int cash = DB.BankCash;

        // Assert
        Assert.That(localHighScore, Is.EqualTo(high));
        Assert.That(localScore, Is.EqualTo(score));
        Assert.That(localLvlIndex, Is.EqualTo(ind));
        Assert.That(localPrevInd, Is.EqualTo(prevInd));
        Assert.That(localGlider, Is.EqualTo(glider));
        Assert.That(localBooster, Is.EqualTo(booster));
        Assert.That(localSpeed, Is.EqualTo(speed));
        Assert.That(localHeight, Is.EqualTo(height));
        Assert.That(localDist, Is.EqualTo(dist));
        Assert.That(localNumCon, Is.EqualTo(runCon));
        Assert.That(localCash, Is.EqualTo(cash));
    }
}
