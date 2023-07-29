using System.Collections;
using System.Collections.Generic;
using ControlFreak2;
using HFPS.Player;
using UnityEngine;
using static HFPS.Systems.Inventory;

[RequireComponent(typeof(InputRig))]
public class InputRigHandler : MonoBehaviour
{

    private GameObject joystick;
    private GameObject jump;
    private GameObject use;
    private GameObject run;
    private GameObject crouch;
    private GameObject prone;
    private GameObject fire;
    private GameObject zoom;
    private GameObject reload;
    private GameObject examine;
    private GameObject rotate;
    private GameObject inventory;
    private GameObject pause;
    private Transform buttons;
    private Transform shortcuts;
    private ExamineManager examineManager;

    private void Awake()
    {
        var rigPanel = CF2Input.activeRig.transform.GetChild(0).GetChild(0);
        joystick = rigPanel.Find("Joystick-Region").gameObject;

        buttons = rigPanel.GetChild(2);
        jump = buttons.Find("Jump-Button").gameObject;
        use = buttons.Find("InteractControl1").gameObject;
        run = buttons.Find("Run").gameObject;
        crouch = buttons.Find("Crouch-Button").gameObject;
        prone = buttons.Find("Prone-Button").gameObject;
        fire = buttons.Find("Fire-Button").gameObject;
        zoom = buttons.Find("Zoom-Button").gameObject;
        reload = buttons.Find("Reload-Button").gameObject;
        examine = buttons.Find("Examine-Button").gameObject;
        rotate = buttons.Find("Rotate").gameObject;
        inventory = buttons.Find("Inventory").gameObject;
        pause = buttons.Find("Pause-Button").gameObject;

        shortcuts = buttons.Find("Shortcuts");
    }

    private void Start()
    {
        examineManager = ExamineManager.Instance;
    }

    internal void UpdateShortcutKeys(List<ShortcutModel> shortcutModels)
    {
        for (int i = 0; i < shortcuts.childCount; i++)
        {
            shortcuts.GetChild(i).gameObject.SetActive(false);
        }

        if (examineManager.isExamining)
        {
            return;
        }

        foreach (var model in shortcutModels)
        {
            switch (model.shortcut)
            {
                case "UseItem1":
                    shortcuts.GetChild(0).gameObject.SetActive(true);
                    break;
                case "UseItem2":
                    shortcuts.GetChild(1).gameObject.SetActive(true);
                    break;
                case "UseItem3":
                    shortcuts.GetChild(2).gameObject.SetActive(true);
                    break;
                case "UseItem4":
                    shortcuts.GetChild(3).gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    internal void ToggleRotation(bool enabled)
    {
        rotate.SetActive(enabled);
    }

    internal void ToggleInteract(bool enabled)
    {
        use.SetActive(enabled);
    }

    internal void TogglePlayerControls(bool enabled, bool interact)
    {
        joystick.SetActive(enabled);
        jump.SetActive(enabled);
        crouch.SetActive(enabled);
        prone.SetActive(enabled);
        run.SetActive(enabled);
        fire.SetActive(enabled);
        reload.SetActive(enabled);
        inventory.SetActive(enabled);
        pause.SetActive(enabled);

        use.SetActive(interact);
    }

    internal void ToggleExamineManager(bool enabled)
    {
        if (enabled)
        {
            joystick.SetActive(false);
            jump.SetActive(false);
            crouch.SetActive(false);
            prone.SetActive(false);
        }
        else
        {
            joystick.SetActive(true);
            jump.SetActive(true);
            crouch.SetActive(true);
            prone.SetActive(true);
        }
    }
}
