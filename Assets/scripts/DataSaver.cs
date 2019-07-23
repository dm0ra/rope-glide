﻿// <copyright file="DataSaver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using System;
using System.IO;
using System.Text;
using UnityEngine;

#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable
/// <summary>
/// This class determines how to read from and write to a file.
/// </summary>
public class DataSaver
#pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
{
    /// <summary>
    /// Save data to file.
    /// </summary>
    /// <typeparam name="T">
    /// Generic type.
    /// </typeparam>
    /// <param name="dataToSave">
    /// The data to save.
    /// </param>
    /// <param name="dataFileName">
    /// The name of the file to save to.
    /// </param>
    public static void SaveData<T>(T dataToSave, string dataFileName)
    {
        string tempPath = Path.Combine(Application.persistentDataPath, "data");
        tempPath = Path.Combine(tempPath, dataFileName + ".txt");

        // Convert To Json then to bytes
        // string jsonData = JsonUtility.ToJson(dataToSave, true);
        string temp = (string)(object)dataToSave;
        byte[] bytes = Encoding.ASCII.GetBytes(temp);

        // Create Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(tempPath));
        }

        // Debug.Log(path);
        try
        {
            File.WriteAllBytes(tempPath, bytes);
            Debug.Log("Saved Data to: " + tempPath.Replace("/", "\\"));
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.LogWarning("Failed To PlayerInfo Data to: " + tempPath.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }
    }

    /// <summary>
    /// Load data from file.
    /// </summary>
    /// <typeparam name="T">
    /// Generic type.
    /// </typeparam>
    /// <param name="dataFileName">
    /// Name of the file being loaded.
    /// </param>
    /// <returns>
    /// Return the data pulled from the file.
    /// </returns>
    public static T LoadData<T>(string dataFileName)
    {
        string tempPath = Path.Combine(Application.persistentDataPath, "data");
        tempPath = Path.Combine(tempPath, dataFileName + ".txt");

        // Exit if Directory or File does not exist
        if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
        {
            Debug.LogWarning("Directory does not exist");
#pragma warning disable IDE0034 // Simplify 'default' expression
            return default(T);
#pragma warning restore IDE0034 // Simplify 'default' expression
        }

        if (!File.Exists(tempPath))
        {
            Debug.Log("File does not exist");
#pragma warning disable IDE0034 // Simplify 'default' expression
            return default(T);
#pragma warning restore IDE0034 // Simplify 'default' expression
        }

        byte[] bytes = null;
        try
        {
            bytes = File.ReadAllBytes(tempPath);
            Debug.Log("Loaded Data from: " + tempPath.Replace("/", "\\"));
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.LogWarning("Failed To Load Data from: " + tempPath.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }

        // Convert to json string
        string ret = Encoding.ASCII.GetString(bytes);
        object ret1 = ret;

        // Convert to Object.
#pragma warning disable CA1305 // Specify IFormatProvider
        return (T)Convert.ChangeType(ret1, typeof(T));
#pragma warning restore CA1305 // Specify IFormatProvider
    }

    /// <summary>
    /// Deletes the data file.
    /// </summary>
    /// <param name="dataFileName">
    /// The name of the file to be deleted.
    /// </param>
    /// <returns>
    /// Returns a boolean indicating success or failure of the file deletion.
    /// </returns>
    public static bool DeleteData(string dataFileName)
    {
        bool success = false;

        // Load Data
        string tempPath = Path.Combine(Application.persistentDataPath, "data");
        tempPath = Path.Combine(tempPath, dataFileName + ".txt");

        // Exit if Directory or File does not exist
        if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
        {
            Debug.LogWarning("Directory does not exist");
            return false;
        }

        if (!File.Exists(tempPath))
        {
            Debug.Log("File does not exist");
            return false;
        }

        try
        {
            File.Delete(tempPath);
            Debug.Log("Data deleted from: " + tempPath.Replace("/", "\\"));
            success = true;
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.LogWarning("Failed To Delete Data: " + e.Message);
        }

        return success;
    }
}