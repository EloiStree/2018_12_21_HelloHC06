using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AddPlatform : MonoBehaviour
{
    public InputField m_name;
    public InputField m_macAddress;
    public void AddPlatform()
    {
        FireworkHC06.AddPlatform(m_name.text, m_macAddress.text);

    }
    public void RemovePlatform()
    {
        FireworkHC06.RemovePlatform(m_name.text, m_macAddress.text);

    }
}
