using ThunderWire.Input;
using UnityEngine;
using ControlFreak2;

public class InputConverter
{
    public static Vector3 MousePosition
    {
        get
        {
            if (InputRigHandler.IsMobileMode)
            {
                return Input.touchCount > 0 ? new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 0) : InputHandler.ReadInput<Vector2>("MousePosition", "PlayerExtra");
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
            if (InputRigHandler.IsMobileMode)
            {
                return new Vector2(CF2Input.GetAxis("Mouse X"), CF2Input.GetAxis("Mouse Y"));
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
            if (InputRigHandler.IsMobileMode)
            {
                return new Vector2(CF2Input.GetAxis("Horizontal"), CF2Input.GetAxis("Vertical"));
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
            if (InputRigHandler.IsMobileMode)
            {
                float y = CF2Input.GetAxis("Mouse ScrollWheel");
                float up = CF2Input.GetAxis("Mouse ScrollWheel Up");
                float down = CF2Input.GetAxis("Mouse ScrollWheel Down");

                if (down > 0)
                {
                    y = -1;
                }
                else if (up < 0)
                {
                    y = 1;
                }

                
                return new Vector2(0, y);
            }
            else
            {
                return InputHandler.ReadInput<Vector2>("Scroll", "PlayerExtra");
            }
        }
    }

    public static bool ReadButton(string ActionName, string ActionMap = "Default")
    {
        if (InputRigHandler.IsMobileMode)
        {
            return CF2Input.GetButton(ActionName);
        }
        else
        {

            return InputHandler.ReadButton(ActionName, ActionMap);
        }
    }

    public static bool ReadButtonOnce(MonoBehaviour Instance, string ActionName, string ActionMap = "Default")
    {
        if (InputRigHandler.IsMobileMode)
        {
            return CF2Input.GetButtonDown(ActionName);
        }
        else
        {

            return InputHandler.ReadButtonOnce(Instance, ActionName, ActionMap);
        }
    }
}
