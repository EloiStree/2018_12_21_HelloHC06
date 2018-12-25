using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

internal class FireworkHC06Mono : MonoBehaviour
{
    public Text m_text;
    public void OnEnable()
    {
       // FireworkHC06.Load();
       // m_text.text = FireworkHC06.GetRecoredPlatform();
    }

    public void OnDisable()
    {
       // FireworkHC06.Save();
    }

    public void DeactivateAll() {
        FireworkHC06.DeactivateAllPins();
    }
    public void ActivateAll()
    {
        FireworkHC06.ActivateAllPins();

    }

    public void SavePlatforms()
    {
        FireworkHC06.Save();

    }
}

internal class FireworkHC06
{

    public static RecorderPlatform m_recored = new RecorderPlatform();
    public static Dictionary<string, AutoConnect> m_autoConnectBluetooth = new Dictionary<string, AutoConnect>();

    
    public static void Save()
    {
       
            //File.WriteAllText(GetRecoredPlatform(), JsonUtility.ToJson(m_recored));
            //Debug.Log("Save:" + GetRecoredPlatform());
        
    }

  

    public static void Load()
    {
        //if (File.Exists(GetRecoredPlatform()))
        //{
        //    string json = File.ReadAllText(GetRecoredPlatform());
        //    m_recored = JsonUtility.FromJson<RecorderPlatform>(json);
        //    Debug.Log("Load:" + GetRecoredPlatform());
        //}

    }

  

    public static string GetRecoredPlatform()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/RecordedPlatform.json";
#else
        Directory.CreateDirectory("/storage/emulated/0/Firework");
        return "/storage/emulated/0/Firework/RecordedPlatform.json";
#endif
    }
    public static void AutoConnectTo(string macAddress)
    {
        AutoConnect connect = null;
        if (m_autoConnectBluetooth.ContainsKey(macAddress)) {
            connect = m_autoConnectBluetooth[macAddress];

        }
        if (connect == null)
        {
            m_autoConnectBluetooth[macAddress]= connect= CreateAutoConnect(macAddress);
            if (connect != null)
            {

                connect.StartConnectionWithMicroController(macAddress);
            }
        }
        
    }

    public static AutoConnect GetConnection(string macAddress) {
        AutoConnect connect = null;
        if (m_autoConnectBluetooth.ContainsKey(macAddress))
        {
            connect = m_autoConnectBluetooth[macAddress];

        }
        return connect;
    }
  

    public static void DisconnectOf(string macAdress)
    {
        AutoConnect auto = GetConnection(macAdress);
        if(auto)
        auto.disconnect();
        
    }

    private static AutoConnect CreateAutoConnect(string macAddress)
    {
        GameObject connectGamo = new GameObject("#AutoConnect");
        AutoConnect connect = connectGamo.AddComponent<AutoConnect>();
        return connect;
    }

    internal static void SetTo(bool isOn, string mac, int pinIndex)
    {
        AutoConnect connection = GetConnection(mac);
        if (connection)
        {
            connection.SetPin(isOn, pinIndex);
        }
  
    }

    internal static void SetTo(bool isOn, string mac, int pinIndex, float delay)
    {
        AutoConnect connection = GetConnection(mac);
        if (connection)
        {
            connection.SetPinWithDelay(isOn, pinIndex, delay);
        }
    }

    internal static void SetPinOnOff(string mac, int pinIndex, float offDelay)
    {
        AutoConnect connection = GetConnection(mac);
        if (connection)
        {
            connection.SetPinOnOff( pinIndex, offDelay);
        }
    }
    //internal static void SetTo(bool isOn, string mac, int pinIndex, float time = 0.5f)
    //{
    //    AutoConnect connection = GetConnection(mac);
    //    if (connection)
    //    {
    //        connection.SetPin(isOn, pinIndex, 0.5f);
    //    }
    //}

    internal static void AddPlatform(string name, string macAddress)
    {
        if (m_recored.m_platformsRecored.Where(k => k.m_macAddress == macAddress).Count() <= 0) {
            m_recored.m_platformsRecored.Add(new Platform() { m_name = name, m_macAddress = macAddress });
        }

    }
    internal static void RemovePlatform(string name, string macAddress)
    {
        m_recored.m_platformsRecored.RemoveAll(k => k.m_macAddress == macAddress);
    }

    internal static void DeactivateAllPins()
    {
        if (m_recored != null)
            foreach (var p in m_recored.m_platformsRecored)
        {
            for (int i = 0; i < 10; i++)
            {
                FireworkHC06.SetTo(false, p.m_macAddress, i);
                FireworkHC06.SetTo(false, p.m_macAddress, 9 - i);

            }
        }
    }

    internal static void ActivateAllPins()
    {
        if(m_recored!=null)
        foreach (var p in m_recored.m_platformsRecored)
        {
            for (int i = 0; i < 10; i++)
            {
                FireworkHC06.SetTo(true,  p.m_macAddress, i);
                FireworkHC06.SetTo(true,  p.m_macAddress, 10 - i);
                FireworkHC06.SetTo(false, p.m_macAddress, i, 2f);
                FireworkHC06.SetTo(false, p.m_macAddress, 9 - i, 2f);

            }
        }
    }
    public void DeactivateAll()
    {
       
    }
    public void ActivateAll()
    {
       

    }
}

[System.Serializable]
public class RecorderPlatform
{

    public List<Platform> m_platformsRecored = new List<Platform>();
}

[System.Serializable]
public class Platform
{
    public string m_name;
    public string m_macAddress;
}