using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static readonly string[] highScoreFileParts = { @"TurnipKnight", @"highscore.bin" };
    public static AudioSource soundSource;
    
    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

  

    public static int GetHighScore()
    {
        int highscore = 0;
        string filePath = GetScoreFilePath(Application.platform);

        filePath = Path.Combine(filePath, highScoreFileParts[0], highScoreFileParts[1]);
        Debug.Log("File Path: " + filePath);
        try
        {
            using (BinaryReader br = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                highscore = br.ReadInt32();
            }
        }catch(Exception exc)
        {
            Debug.Log("Error Getting HighScore");
            Debug.Log(exc.Message);
        } 
  
        return highscore;
    }

    public static void SaveHighScore(int highscore)
    {
        try
        {
            
            string filepath = GetScoreFilePath(Application.platform);
            filepath = Path.Combine(filepath, highScoreFileParts[0]);
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            filepath = Path.Combine(filepath, highScoreFileParts[1]);
            Debug.Log("File Path: " + filepath);
            using (BinaryWriter wr = new BinaryWriter(File.Open(filepath, FileMode.OpenOrCreate)))
            {
                wr.Write(highscore);
            }
        } catch(IOException exc)
        {
            Debug.Log("Error Saving High Score");
            Debug.Log(exc.Message);
        }
    }

    /// <summary>
    /// Returns the path to the external file based on the application platform
    /// </summary>
    /// <returns></returns>
    public static string GetScoreFilePath(RuntimePlatform platform)
    {
        switch (platform)
        {
            case RuntimePlatform.Android:
                return GetAndroidInternalDir();
            case RuntimePlatform.WindowsEditor:
                return GetWindowsExternalDir();
            default:
                return "";
        }
    }
    #region EXTERNAL_DIRS

    public static string GetWindowsExternalDir()
    {
       return Directory.GetCurrentDirectory();
    }


    //NOTE: This GetExternalStorageDirectory() has been deprecated in version Android 10;
    private static string GetAndroidExternalDirOld(){
        return "";

    }

    public static string GetAndroidInternalDir(){
        string path = "";
        
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
            using (AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    path = ajo.Call<AndroidJavaObject>("getCacheDir").Call<string>("getAbsolutePath");
                }
            }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error fetching native Android internal storage dir: " + e.Message);
            }
        }
        return path;
    }
    
    #endregion
}