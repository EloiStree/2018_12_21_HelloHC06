using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Actions : MonoBehaviour
{

    public GameObject m_prefab;
    public Transform m_root;


    public void SetValue(List<JsonTriggerableActions> groupOfActions) {
        Clear();
        for (int i = 0; i < groupOfActions.Count; i++)
        {
            GameObject gamo = GameObject.Instantiate(m_prefab, m_root);
            UI_TriggerableAction rec = gamo.GetComponent<UI_TriggerableAction>();
            rec.SetActions(groupOfActions[i]);


        }
    }


    public void ClearAndRefresh()
    {
        Clear();
    }
   

    public void Clear()
    {
        int childCount = m_root.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(m_root.GetChild(i).gameObject);
        }
    }
}
