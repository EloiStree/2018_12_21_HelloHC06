using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CreateMicroControllers : MonoBehaviour
{
    public GameObject m_prefab;
    public Transform m_root;
    // Start is called before the first frame update
    //private IEnumerator Start()
    //{
    //    yield return new WaitForSeconds(1);
    //    FireworkHC06.Load();
    //    Clear();
    //    Refresh();
    //}
    public void ClearAndRefresh() {
        Clear();
        Refresh();
    }
    public void Refresh()
    {
        foreach (var plat in FireworkHC06.m_recored.m_platformsRecored)
        {
            GameObject gamo = GameObject.Instantiate(m_prefab, m_root);
            UI_RecorderPlatform  rec  = gamo.GetComponent<UI_RecorderPlatform>();
            rec.SetValue(plat.m_name, plat.m_macAddress);
        }
        
    }
    
    public void Clear()
    {
        int childCount = m_root.childCount;
       for (int i = 0; i< childCount; i++) {
           Destroy(m_root.GetChild(i).gameObject);
        }
    }
}
