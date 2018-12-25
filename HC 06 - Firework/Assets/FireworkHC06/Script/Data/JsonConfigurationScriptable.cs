using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(menuName = "My Assets/Configurable Script")]
public class JsonConfigurationScriptable : ScriptableObject
{
    public JsonConfiguration m_configurationData;
}

[System.Serializable]
public class JsonConfiguration
{
    public string m_userName;
    public List<JsonPlatform> m_platformsAvailable;
    public List<JsonTriggerableActions> m_triggerableActionsGroup;

    public static string GetSaveAsJson(JsonConfiguration configuation)
    {
        return JsonUtility.ToJson(configuation);
    }
}


[System.Serializable]
public class JsonPlatform
{
    public string m_name;
    public JsonPlatformId m_macId;
}
[System.Serializable]
public class JsonPlatformId
{
    public string m_macAddress;

}

[System.Serializable]
public class JsonTriggerableActions
{
    public string m_oneWordName;
    public string m_description;
    public List<JsonPinToTrigger> m_actions;
}

[System.Serializable]
public class JsonPinToTrigger
{

    public JsonPlatformId m_macId;
    public int m_pinIndex;
    public float m_relativeTime;
}


