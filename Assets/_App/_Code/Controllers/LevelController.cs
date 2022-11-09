using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private InputManager inputManager;
    private void Start()
    {
        inputManager.SetCurrentInputReader(player);
    }
}
