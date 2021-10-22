using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class FlashLight : MonoBehaviour
{
    Light m_light;
    public bool drainOverTime;
    public float maxBrightness;
    public float minBrightness;
    public float drainRate;
    // Start is called before the first frame update
    void Start()
    {
        m_light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(drainOverTime == true && m_light.enabled == true){
            m_light.intensity = Mathf.Clamp(m_light.intensity,maxBrightness,minBrightness);
            if(m_light.intensity>maxBrightness){
                m_light.intensity = Time.deltaTime*(drainRate/1000);
            }
        }
        if(TCKInput.GetAction("fireBtn",EActionEvent.Down)){
            m_light.enabled=!m_light.enabled;
        }
    }
    public void ReplaceBattery(float amount){
        m_light.intensity += amount;
    }
}
