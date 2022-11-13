using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class DevHelper : MonoBehaviour
{
    private bool helperIsActive;

    private int historyIndex = 0;

    [SerializeField] private GameObject canvasGameObject;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Transform textParentObject;
    [SerializeField] private GameObject textGo;

    private List<string> commandHistory;

    public static DevHelper Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }

    private void Start()
    {
        commandHistory = new List<string>();
        canvasGameObject.SetActive(false);
        helperIsActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ActivateOrDeactivateHelper();
        }

        if (Input.GetKeyDown(KeyCode.Return) && helperIsActive)
        {
            string input = inputField.text;
            string[] inputs = input.Split(" ");

            commandHistory.Add(input);

            string command = Command(inputs);
            GameObject g = Instantiate(textGo, textParentObject);
            g.GetComponent<TextMeshProUGUI>().text = command;
            inputField.text = "";
            inputField.ActivateInputField();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            historyIndex++;
            if(historyIndex > commandHistory.Count)
            {
                historyIndex--;
            }
            inputField.text = commandHistory[commandHistory.Count -historyIndex];
            inputField.MoveToEndOfLine(false, false);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            historyIndex--;
            if (historyIndex < 1)
            {
                historyIndex++;
            }
            inputField.text = commandHistory[commandHistory.Count - historyIndex];
            inputField.MoveToEndOfLine(false, false);
        }
    }

    private void ActivateOrDeactivateHelper()
    {
        helperIsActive = !helperIsActive;
        canvasGameObject.SetActive(helperIsActive);
        if (helperIsActive)
        {
            inputField.ActivateInputField();
            inputField.text = "";
        }
    }

    /// <summary>
    /// 
    /// Available commands:
    /// 1. loadscene {scenename}
    /// 2. spawnobject {objectname} {posx} {posy}
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="extra"></param>

    private string Command(string[] inputs)
    {
        historyIndex = 0;

        try
        {
            switch (inputs[0])
            {
                case "loadscene":
                    LoadScene(inputs[1]);
                    return "Loading scene " + inputs[1] + ".";

                case "spawnobject":
                    SpawnObject(inputs[1], Int32.Parse(inputs[2]), Int32.Parse(inputs[3]));
                    return "Object " + inputs[1] + " spawned at position: " + inputs[2] + "," + inputs[3] + ".";

                default:
                    return "Command unknown.";
            }
        }
        catch (Exception ex)
        {
            return "An error occured with one of the commands: " + ex.Message + ".";
        }
    }

    private void LoadScene(string sceneNumber)
    {
        //CustomSceneManager.Instance.LoadSceneAsync(Int32.Parse(sceneNumber));
    }

    private void SpawnObject(string objectname, int posX, int posY)
    {
        Debug.Log("SpawnObject: " + objectname);
    }
}
