using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputKeys
{
    None = -1,
    Left,
    Right,
    Up,
    Jump,
    Max
}

public static class InputManager
{
    static Dictionary<InputKeys, KeyCode> keyCollections;

    static InputManager()
    {
        keyCollections = new Dictionary<InputKeys, KeyCode>
        {
            { InputKeys.Left, KeyCode.A },
            { InputKeys.Right, KeyCode.D },
            { InputKeys.Up, KeyCode.W },
            { InputKeys.Jump, KeyCode.Space }
        };
    }

    public static bool GetKey(InputKeys key)
    {
        if (!keyCollections.ContainsKey(key)) return false;

        return Input.GetKey(keyCollections[key]);
    }

    public static bool GetKeyDown(InputKeys key)
    {
        if (!keyCollections.ContainsKey(key)) return false;

        return Input.GetKeyDown(keyCollections[key]);
    }

    public static bool GetKeyUp(InputKeys key)
    {
        if (!keyCollections.ContainsKey(key)) return false;

        return Input.GetKeyUp(keyCollections[key]);
    }
}
