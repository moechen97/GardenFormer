using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;

    [SerializeField] private LightPreset Preset;

    [SerializeField,Range(0,24)] private float TimeOfDay;
    [SerializeField] private float Speed;

    private void OnValidate()
    {
        if(DirectionalLight != null)
            return;
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (var light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
                    
            }
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);
        Camera.main.backgroundColor = Preset.CameraBackGroundColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            //DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3(timePercent * 360f-90f,-100,0));
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Preset == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime*Speed;
            TimeOfDay %= 24;
            UpdateLighting(TimeOfDay/24f);
        }
        else
        {
            UpdateLighting(TimeOfDay/24f);
        }
    }

    public void adjustTimeSpeed(float x)
    {
        Speed = x;
    }
}