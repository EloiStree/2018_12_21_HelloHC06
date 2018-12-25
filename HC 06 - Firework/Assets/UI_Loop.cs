using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Loop : MonoBehaviour
{

    public bool m_loop = false;
    public float m_delay=5f;
    void Start()
    {
        InvokeRepeating("Blink",0,m_delay);
    }

    // Update is called once per frame
    void Blink()
    {
        if(m_loop)
            FireworkHC06.ActivateAllPins();   
    }

    public void Switch() {
        m_loop = !m_loop;
    }
}
