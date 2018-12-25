using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    public static DebugUI Default;
    public InputField m_debug;

    public static void Log(string text) {
        Default.m_debug.text = text;
    }
}
