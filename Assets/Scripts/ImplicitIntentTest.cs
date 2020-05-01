using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplicitIntentTest : MonoBehaviour
{
    // Start is called before the first frame update
    private static string izzyWebsite = @"https://www.theizzyfoundation.org/";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            if(Application.platform == RuntimePlatform.Android)
            {
                //get references to classes to call static methods from them
                AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
                AndroidJavaClass uri = new AndroidJavaClass("android.net.Uri");

                //Create a new intent object
                AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

                //set the action to view content, in order to open a url
                intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_VIEW"));

                //call uri parse to parse the string into a  uri object
                intentObject.Call<AndroidJavaObject>("setData", uri.CallStatic<AndroidJavaObject>("parse", izzyWebsite));

                //get the current activity running 
                AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

                //create implicit intent activity
                AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "The Izzy Foundation");
                currentActivity.Call("startActivity", jChooser);
            }
        }
    }
}
