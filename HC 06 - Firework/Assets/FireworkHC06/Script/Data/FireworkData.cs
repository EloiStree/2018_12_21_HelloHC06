using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireworkData : MonoBehaviour
{

    //public JsonConfiguration m_configuration;
    public JsonConfigurationScriptable m_configScript;
    [TextArea(0,30)]
    public string m_json;

    private void Start()
    {
        string json = JsonConfiguration.GetSaveAsJson(m_configScript.m_configurationData);
        Debug.Log(json);
        m_json = json;
       // m_configScript.m_configurationData = m_configuration;
    }
}
