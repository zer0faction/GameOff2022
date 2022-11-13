using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DayNightWeatherController : MonoBehaviour
{
    [Header("References to gameObjects")]
    [SerializeField] private GameObject mistGo;
    [SerializeField] private Volume volume;
    [SerializeField] private CameraBackgroundController cameraBackgroundController;

    private UnityEngine.Rendering.Universal.ColorAdjustments colorAdjustments;
    private GameObject[] lights;

    private void Start()
    {
        mistGo.SetActive(false);
        GetAllLights();

        //Testing
        SetWeatherEffectsAndTimeOfDay(9, false);
    }

    /// <summary>
    /// TimeHours:
    /// - 0/24
    /// 7:00
    /// 12:00
    /// 13:00
    /// 17:00
    /// 
    /// </summary>
    /// <param name="timeHours"></param>
    public void SetWeatherEffectsAndTimeOfDay(int timeHours, bool mist)
    {
        switch (timeHours)
        {
            case 9:
                Case9();
                break;

            case 20:
                Case20();
                break;

            default:
                //Default = daytime
                Case9();
                break;
        }
        mistGo.SetActive(mist);

    }

    private void GetAllLights()
    {
        lights = GameObject.FindGameObjectsWithTag("Light");
    }

    private void DeactivateLights()
    {
        foreach (GameObject g in lights)
            g.SetActive(false);
    }

    private void Case9()
    {
        cameraBackgroundController.SetBackgroundDayOne();
        volume.profile.TryGet<UnityEngine.Rendering.Universal.ColorAdjustments>(out colorAdjustments);
        colorAdjustments.postExposure.value = -.671f;
        DeactivateLights();
    }

    private void Case20()
    {
        cameraBackgroundController.SetBackgroundNightOne();
        volume.profile.TryGet<UnityEngine.Rendering.Universal.ColorAdjustments>(out colorAdjustments);
        colorAdjustments.postExposure.value = -5.7f;
    }
}
