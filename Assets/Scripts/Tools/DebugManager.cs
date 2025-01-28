using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public KeyCode StartGameInput;
    public KeyCode ResetInput;

    void Update()
    {
        if (Input.GetKeyDown(StartGameInput))
        {
            GameManager.Instance.StartGame();
        }

        if (Input.GetKeyDown(ResetInput))
        {
            GameManager.Instance.RestartGame();
        }
    }
}
