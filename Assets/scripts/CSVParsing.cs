using UnityEngine;
using UnityEditor;

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;


public class CSVParsing : MonoBehaviour
{
    public TextAsset csvFile; // Reference of CSV file
    public InputField rollNoInputField;// Reference of rollno input field
    public InputField nameInputField; // Reference of name input filed
    public Text contentArea; // Reference of contentArea where records are displayed

    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter
    private List<List<float>> results;
    void Start()
    {
        results = new List<List<float>>();
        try
        {
            readData();
            Debug.Log("Succsessfully read Coefficent Data ");
        }
        catch (System.Exception e)
        {
            // Something unexpected went wrong.
            Debug.Log("Unable to read Coefficent Data " + e);
            // Maybe it is also necessary to terminate / restart the application.
        }
        print(results);
    }
    // Read data from CSV file
    private void readData()
    {
        string[] records = csvFile.text.Split(lineSeperater);
        
        foreach (string record in records)
        {
            
            List<float> temp = new List<float>();
            string[] fields = record.Split(fieldSeperator);
            if(fields[0].Equals("229"))
            {
                return;
            }
            temp.Add(float.Parse(fields[1]));
            temp.Add(float.Parse(fields[2]));
            temp.Add(float.Parse(fields[3]));
            temp.Add(float.Parse(fields[4]));

            results.Add(temp);
        }
    }
    public float[] getLiftCoef(int index)
    {
        return new float[] { results[index][0], results[index][1]};
    }
    public float[] getDragCoef(int index)
    {
        return new float[] { results[index][2], results[index][3] };
    }
    // Add data to CSV file
    public void addData()
    {
        // Following line adds data to CSV file
        Debug.Log(Application.dataPath);
        File.AppendAllText(getPath() + "/Assets/CoefData.csv", lineSeperater + rollNoInputField.text + fieldSeperator + nameInputField.text);
        // Following lines refresh the edotor and print data
        rollNoInputField.text = "";
        nameInputField.text = "";
        contentArea.text = "";
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
        readData();
    }

    // Get path for given CSV file
    private static string getPath()
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