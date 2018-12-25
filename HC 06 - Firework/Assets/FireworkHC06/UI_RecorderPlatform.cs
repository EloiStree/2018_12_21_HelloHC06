using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;



public class UI_RecorderPlatform : MonoBehaviour
{
    public InputField m_name;
    public InputField m_macAddress;
    public float m_timeToStayOn=2f;

    public void SetValue(string name, string mac)
    {
        m_name.text = name;
        m_macAddress.text = mac;
        FireworkHC06.AutoConnectTo(mac);
    }

    public void TriggerPin(int pinIndex) {
        FireworkHC06.SetPinOnOff( m_macAddress.text,  pinIndex, m_timeToStayOn);
    }

    public void RemovePlatform() {
        FireworkHC06.RemovePlatform(m_name.text, m_macAddress.text);
    }

    public void AutoConnect() {
        FireworkHC06.AutoConnectTo(m_macAddress.text);
    }

    public void DeactivateAll()
    {
        for (int i = 0; i < 10; i++)
        {
            FireworkHC06.SetTo(false, m_macAddress.text, i);
            FireworkHC06.SetTo(false, m_macAddress.text, 9-i);

        }
    }
    public void ActivateAll()
    {
        for (int i = 0; i < 10; i++)
        {
            FireworkHC06.SetTo(true, m_macAddress.text, i);
            FireworkHC06.SetTo(true, m_macAddress.text, 10-i);
            FireworkHC06.SetTo(false, m_macAddress.text, i, 2f);
            FireworkHC06.SetTo(false, m_macAddress.text, 9-i, 2f);

        }

    }

    public void Blink() {
      AutoConnect auto =  FireworkHC06.GetConnection(m_macAddress.text);
      if(auto)
            auto.Blink();

    }

    public void Disconnect() {
        FireworkHC06.DisconnectOf(m_macAddress.text);
        
    }
}
