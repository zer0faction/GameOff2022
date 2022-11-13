using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackgroundController : MonoBehaviour
{
    [Header("References to GameObjects")]
    [SerializeField] private List<SpriteRenderer> spriteRenderers;

    [Header("Background sprites")]
    [SerializeField] private Sprite bg_night_1;
    [SerializeField] private Sprite bg_day_1;

    public void SetBackgroundNightOne()
    {
        foreach(SpriteRenderer sr in spriteRenderers)
        {
            sr.sprite = bg_night_1;
        }
    }

    public void SetBackgroundDayOne()
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.sprite = bg_day_1;
        }
    }
}
