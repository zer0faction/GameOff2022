using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour, IOnStartScene
{
    [SerializeField] private TilemapRenderer cameraBorderTilemapRenderer;
    [SerializeField] private TilemapRenderer solidTilemapRenderer;

    //ToDo: replace this shit
    private void Start()
    {
        OnStartScene();
    }

    public void OnStartScene()
    {
        cameraBorderTilemapRenderer.enabled = false;
        solidTilemapRenderer.enabled = false;
    }
}
