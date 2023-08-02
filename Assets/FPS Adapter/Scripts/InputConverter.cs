using System.Collections;
using System.Collections.Generic;
using ThunderWire.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using ControlFreak2;

public class InputConverter
{
    public static readonly bool isInputRigged = true;

    public static bool EnableShortcutBinding
    {
        get
        {
            return isInputRigged == false;
        }
    }

    public static Vector3 MousePosition
    {
        get
        {
            if (isInputRigged)
            {
                return Input.touchCount > 0 ? new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 0) : CF2Input.mousePosition;
            }
            else
            {
                return InputHandler.ReadInput<Vector2>("MousePosition", "PlayerExtra");
            }
        }
    }

    public static Vector2 Look
    {
        get
        {
            if (isInputRigged)
            {
                return new Vector2(ControlFreak2.CF2Input.GetAxis("Mouse X"), ControlFreak2.CF2Input.GetAxis("Mouse Y"));
            }
            else
            {
                return InputHandler.ReadInput<Vector2>("Look", "PlayerExtra");
            }
        }
    }

    public static Vector2 Move
    {
        get
        {
            if (isInputRigged)
            {
                return new Vector2(ControlFreak2.CF2Input.GetAxis("Horizontal"), ControlFreak2.CF2Input.GetAxis("Vertical"));
            }
            else
            {
                return InputHandler.ReadInput<Vector2>("Move");
            }
        }
    }

    public static Vector2 Scroll
    {
        get
        {
            if (isInputRigged)
            {
                return new Vector2(0, ControlFreak2.CF2Input.GetAxis("Mouse ScrollWheel"));
            }
            else
            {
                return InputHandler.ReadInput<Vector2>("Scroll", "PlayerExtra");
            }
        }
    }

    public static bool ReadButton(string ActionName, string ActionMap = "Default")
    {
        if (isInputRigged)
        {
            return ControlFreak2.CF2Input.GetButton(ActionName);
        }
        else
        {

            return InputHandler.ReadButton(ActionName, ActionMap);
        }
    }

    public static bool ReadButtonOnce(MonoBehaviour Instance, string ActionName, string ActionMap = "Default")
    {
        if (isInputRigged)
        {
            return ControlFreak2.CF2Input.GetButtonDown(ActionName);
        }
        else
        {

            return InputHandler.ReadButtonOnce(Instance, ActionName, ActionMap);
        }
    }
}
