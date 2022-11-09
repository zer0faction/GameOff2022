using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Input settings")]
    [SerializeField] private float controllerAxisMin = .35f;

    private IInputReader currentInputReader;

    public static InputManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void SetCurrentInputReader(IInputReader inputReader)
    {
        currentInputReader = inputReader;
    }

    private void Update()
    {
        currentInputReader.InputData(GetInputDataThisFrame());
    }

    private InputData GetInputDataThisFrame()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        int right = 0;
        int left = 0;
        int up = 0;
        int down = 0;

        if (inputX > controllerAxisMin) { right = 1; }
        if (inputX < -controllerAxisMin) { left = 1; }
        if (inputY > controllerAxisMin) { up = 1; }
        if (inputY < -controllerAxisMin) { down = 1; }

        InputData inputData = new InputData
        {
            jumpButtonPressed = Input.GetButtonDown("Jump"),
            jumpButtonReleased = Input.GetButtonUp("Jump"),
            x = (right - left),
            y = (up - down)
        };
        return inputData;
    }
}
