using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    public int participantID;

    private string path;

    void Start()
    {

        if (SceneManager.GetActiveScene().name.Contains("Crystal"))
        {
            path = @"" + participantID + ".txt";
        }
        else
        {
            path = @"" + participantID + ".txt";
        }

        // Create file and write test person number.
        using (StreamWriter sw = File.CreateText(path)){ 
            sw.WriteLine("_____________________________________________________");
            sw.WriteLine("Participant ID: " + participantID);
            sw.WriteLine("_____________________________________________________");
            sw.WriteLine(" ");
            sw.WriteLine(" ");
        }
    }
    
    /// <summary>
    /// This method writes instruction mode and model name to the text file.
    /// </summary>
    /// <param name="method">The instruction mode: pointing or building.</param>
    /// <param name="model">The model name.</param>
    public void WriteCondition(string method, string model)
    {
        using (StreamWriter sw = new StreamWriter(path, append: true))
        {
            sw.WriteLine(" ");
            sw.WriteLine("-----------------------------------------------------");
            sw.WriteLine(method + " - " + model);
            sw.WriteLine("-----------------------------------------------------");
        }
    }

    /// <summary>
    /// This method exports the assembly time to the text file.
    /// </summary>
    /// <param name="time">The assembly time.</param>
    public void ExportTime(float time)
    {
        using (StreamWriter sw = new StreamWriter(path, append: true))
        {
            sw.WriteLine(time);
        }
    }
    
}
