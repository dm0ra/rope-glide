// <copyright file="CSVParsing.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This method gets lift and drag coefficients for the glider.
/// </summary>
public class CSVParsing : MonoBehaviour
{
#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter
#pragma warning disable SA1401 // Fields should be private
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1606 // Element documentation should have summary text
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// Holds the glider drag and lift coefficients.
    /// </summary>
    public TextAsset csvFile; // Reference of CSV file
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1606 // Element documentation should have summary text
#pragma warning restore SA1600 // Elements should be documented
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore SA1307 // Accessible fields should begin with upper-case letter

#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter
#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// Reference of roll no input field.
    /// </summary>
    public InputField rollNoInputField;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore SA1307 // Accessible fields should begin with upper-case letter

#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter
#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// Reference of name input filed.
    /// </summary>
    public InputField nameInputField;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore SA1307 // Accessible fields should begin with upper-case letter

#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter
#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// Reference of contentArea where records are displayed.
    /// </summary>
    public Text contentArea;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore SA1307 // Accessible fields should begin with upper-case letter

    /// <summary>
    /// It defines line separate character.
    /// </summary>
    private char lineSeperater;

    /// <summary>
    /// It defines field separate chracter.
    /// </summary>
    private char fieldSeperator;

    private List<List<float>> results;

/// <summary>
/// This method gets lift coefficient based on the angle.
/// </summary>
/// <param name="index">int index.</param>
/// <returns>float[].</returns>
    public float[] GetLiftCoef(int index)
    {
        return new float[] { this.results[index][0], this.results[index][1] };
    }

#pragma warning disable SA1600 // Elements should be documented
    public float[] GetDragCoef(int index)
#pragma warning restore SA1600 // Elements should be documented
    {
        return new float[] { this.results[index][2], this.results[index][3] };
    }

    /// <summary>
    /// Add data to CSV file.
    /// </summary>
    public void AddData()
    {
        // Following line adds data to CSV file
        Debug.Log(Application.dataPath);
        File.AppendAllText(getPath() + "/Assets/CoefData.csv", this.lineSeperater + this.rollNoInputField.text + this.fieldSeperator + this.nameInputField.text);

        // Following lines refresh the edotor and print data
        this.rollNoInputField.text = string.Empty;
        this.nameInputField.text = string.Empty;
        this.contentArea.text = string.Empty;
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
        this.readData();
    }

    private void Start()
    {
        lineSeperater = '\n';
        fieldSeperator = ',';
        this.results = new List<List<float>>();
        try
        {
            this.readData();
            Debug.Log("Succsessfully read Coefficent Data ");
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch (System.Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
        {
            // Something unexpected went wrong.
            Debug.Log("Unable to read Coefficent Data " + e);

            // Maybe it is also necessary to terminate / restart the application.
        }

        //print(this.results);
    }

    // Read data from CSV file
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SA1300 // Element should begin with upper-case letter
    private void readData()
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore IDE1006 // Naming Styles
    {
        string[] records = this.csvFile.text.Split(this.lineSeperater);

        foreach (string record in records)
        {
            List<float> temp = new List<float>();
            string[] fields = record.Split(this.fieldSeperator);
#pragma warning disable CA1307 // Specify StringComparison
            if (fields[0].Equals("229"))
#pragma warning restore CA1307 // Specify StringComparison
            {
                return;
            }

#pragma warning disable CA1305 // Specify IFormatProvider
            temp.Add(float.Parse(fields[1]));
#pragma warning restore CA1305 // Specify IFormatProvider
#pragma warning disable CA1305 // Specify IFormatProvider
            temp.Add(float.Parse(fields[2]));
#pragma warning restore CA1305 // Specify IFormatProvider
#pragma warning disable CA1305 // Specify IFormatProvider
            temp.Add(float.Parse(fields[3]));
#pragma warning restore CA1305 // Specify IFormatProvider
#pragma warning disable CA1305 // Specify IFormatProvider
            temp.Add(float.Parse(fields[4]));
#pragma warning restore CA1305 // Specify IFormatProvider

            this.results.Add(temp);
        }
    }

    // Get path for given CSV file
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SA1204 // Static elements should appear before instance elements
#pragma warning disable SA1300 // Element should begin with upper-case letter
    private static string getPath()
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore SA1204 // Static elements should appear before instance elements
#pragma warning restore IDE1006 // Naming Styles
    {
#if UNITY_EDITOR
        return Application.dataPath;
#elif UNITY_ANDROID
return Application.persistentDataPath;// +fileName;
#elif UNITY_IPHONE
return GetiPhoneDocumentsPath();// +"/"+fileName;
#else
return Application.dataPath;// +"/"+ fileName;
#endif
    }

    // Get the path in iOS device
    private static string GetiPhoneDocumentsPath()
    {
        string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
        path = path.Substring(0, path.LastIndexOf('/'));
        return path + "/Documents";
    }
}