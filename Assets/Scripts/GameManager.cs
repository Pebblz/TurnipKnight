using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static readonly string highScoreFile = @"/TurnipKnight/highscore.bin";
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
                        } else if (isRemovable && isEmulated == false)
                        {
                            sdCard = directory;
                        }
                    }
                }

                if (sdCard != null)
                {
                    return sdCard.Call<string>("getAbsolutePath");
                } else
                {
                    return emulated.Call<string>("getAbsolutePath");
                }
            }
        }
    }

    public static int GetHighScore()
    {
        int highscore = 0;
        string filePath = GetAndroidExternalDir() + highScoreFile;
        try
        {
            using (BinaryReader br = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                highscore = br.ReadInt32();
            }
        }catch(IOException exc)
        {
            
        }
  
        return highscore;
    }

    public static void SaveHighScore(int highscore)
    {
        string filepath = GetAndroidExternalDir() + highScoreFile;
        using (BinaryWriter wr = new BinaryWriter(File.Open(filepath, FileMode.OpenOrCreate)))
        {
            wr.Write(highscore);
        }
    }
}