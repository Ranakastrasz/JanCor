using UnityEngine;
using System.Collections;

public class ShowToast : MonoBehaviour
{
    public static ShowToast Active;
    string toastString;
    AndroidJavaObject currentActivity;

    public void Start()
    {
        Active = this;
    }

    public void MyShowToastMethod()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            showToastOnUiThread("It Worked!");
        }
    }

    void showToastOnUiThread(string toastString)
    {
        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        this.toastString = toastString;

        currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(showToast));
    }

    public static void Toast(string toastString)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Active.showToastOnUiThread(toastString);
        }
        else
        {
            Debug.Log(toastString);
        }
    }

    void showToast()
    {
        Debug.Log("Running on UI thread");
        AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", toastString);
        AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
        toast.Call("show");
    }

}