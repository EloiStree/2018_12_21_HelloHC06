using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ConfigurationDownloader : MonoBehaviour
{

    public InputField m_configurationDownloader;
    public Text m_warning;
    public UI_CreateMicroControllers ui_controllers;
    public UI_Actions m_actions;

    void Start()
    {
        Load();
        StartDownload();
        m_configurationDownloader.onValueChanged.AddListener(SaveUrl);
    }

    private void SaveUrl(string url)
    {
        Save();
    }

    private void OnEnable()
    {
        Load();
    }

    private void Load()
    {
        string txt = PlayerPrefs.GetString("ConfigUrl");
        if (!string.IsNullOrEmpty(txt))
            m_configurationDownloader.text = txt;
    }

    private void OnDisable()
    {
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetString("ConfigUrl", m_configurationDownloader.text);
    }

    // Update is called once per frame
    public void StartDownload()
    {
        StartCoroutine(Download()); 
    }
    [TextArea(0,30)]
    public string t;
    IEnumerator Download() {

        string url = m_configurationDownloader.text;
        WWW www = new WWW(url);
        yield return www;

        if (string.IsNullOrEmpty(www.error)) {
            JsonConfiguration config = null;
            try
            {
                t = www.text;
                config = JsonUtility.FromJson<JsonConfiguration>(t);
               

            }
            catch (System.Exception e) {
                Debug.LogWarning("Not Converted JSON:\n " + e);
            }
            if(config!=null)
            foreach (var item in config.m_platformsAvailable)
            {
                FireworkHC06.AddPlatform(item.m_name, item.m_macId.m_macAddress);
                ui_controllers.ClearAndRefresh();
                m_actions.SetValue(config.m_triggerableActionsGroup);
            }
        } else Debug.LogWarning("Not JSON to download:\n "+www.error);

    }
}
