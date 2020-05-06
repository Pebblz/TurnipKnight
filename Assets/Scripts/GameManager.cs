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
        try
        {
            using (BinaryReader br = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                highscore = br.ReadInt32();
            }
        }catch(Exception exc)
        {
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
            using (BinaryWriter wr = new BinaryWriter(File.Open(filepath, FileMode.OpenOrCreate)))
            {
                wr.Write(highscore);
            }
        } catch(IOException ex)
        {

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
                return GetAndroidExternalDir();
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

    private static string GetAndroidExternalDir()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                AndroidJavaObject[] externalDirs = context.Call<AndroidJavaObject[]>("getExternalFilesDirs");
                AndroidJavaObject emulated = null;
                AndroidJavaObject sdCard = null;

                for (int i = 0; i < externalDirs.Length; i++)
                {
                    AndroidJavaObject directory = externalDirs[i];
                    using (AndroidJavaClass env = new AndroidJavaClass("android.os.Environment"))
                    {
                        bool isRemovable = env.CallStatic<bool>("isExternalStorageRemovable", directory);
                        bool isEmulated = env.CallStatic<bool>("isExternalStorageEmulated", directory);

                        if (isEmulated)
                        {
                            emulated = directory;
                        }
                        else if (isRemovable && isEmulated == false)
                        {
                            sdCard = directory;
                        }
                    }
                }

                string path = "";
                if (sdCard != null)
                {
                    path = sdCard.Call<string>("getAbsolutePath");
                    
                }
                else
                {
                    path =  emulated.Call<string>("getAbsolutePath");
                }

                return path;
            }
        }
    }
    #endregion
}