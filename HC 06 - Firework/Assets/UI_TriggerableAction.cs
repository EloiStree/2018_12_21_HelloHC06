using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_TriggerableAction : MonoBehaviour
{
    public Text m_name;
    public JsonTriggerableActions m_actions;
    public float m_timer;
    public bool m_checkForActions;
    public void SetActions(JsonTriggerableActions actions)
    {
        m_name.text = actions.m_oneWordName +": "+actions.m_description ;
        m_actions = actions;

    }
    public void ExecuteAction() {
        Debug.Log("Play:"+m_actions.m_description);
        m_checkForActions = true;
        m_timer = 0f;
    }

    public void Update()
    {
        if (m_checkForActions) {

        float oldTime = m_timer;
        m_timer += Time.deltaTime;
        float newTime = m_timer;

           List <JsonPinToTrigger> toActivate = m_actions.m_actions.Where(k => k.m_relativeTime >= oldTime && k.m_relativeTime < newTime).ToList();
            foreach (JsonPinToTrigger go in toActivate)
            {
                Debug.Log(string.Format("Exe({2}): {0} , {1}", go.m_macId.m_macAddress, go.m_pinIndex, m_timer));
                FireworkHC06.SetPinOnOff( go.m_macId.m_macAddress, go.m_pinIndex,2f);
            }
        }

    }
}
