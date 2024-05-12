using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeTOSPanel : MonoBehaviour
{
    public static NativeTOSPanel Instance;
    [TextArea(10, 50)]
    [SerializeField] private string message, title;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
#if UNITY_ANDROID
        Debug.Log("Native Call");
        ShowAlert();
#endif
    }

    public void ShowAlert()
    {
        AndroidJavaObject activity = null;
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            AndroidJavaObject dialog = null;
            using (var html = new AndroidJavaClass("android.text.Html"))
            {
                var titleHtml = html.CallStatic<AndroidJavaObject>("fromHtml", title);
                
                using (var builder = new AndroidJavaObject("android.app.AlertDialog$Builder", activity))
                {
                    builder.Call<AndroidJavaObject>("setTitle", titleHtml).Dispose();
                    builder.Call<AndroidJavaObject>("setMessage", message).Dispose();
                    builder.Call<AndroidJavaObject>("setPositiveButton", "OK", new OnClickListener(() =>
                    {
                        Debug.Log("Button pressed");
                    })).Dispose();
                    dialog = builder.Call<AndroidJavaObject>("create");
                    dialog.Call("show");
                }
                
                dialog.Dispose();
                activity.Dispose();
            }

        }));
    }

    private class OnClickListener : AndroidJavaProxy
    {
        public readonly Action Callback;
        public OnClickListener(Action callback) : base("android.content.DialogInterface$OnClickListener")
        {
            Callback = callback;
        }
        public void onClick(AndroidJavaObject dialog, int id)
        {
            Callback();
        }
    }
}

