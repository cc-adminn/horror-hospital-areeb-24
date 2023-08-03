using System;
using System.Collections.Generic;
using ControlFreak2;
using HFPS.Player;
using HFPS.Systems;
using UnityEngine;
using static HFPS.Systems.HFPS_GameManager;
using static HFPS.Systems.Inventory;
//var btn = shortcuts.GetChild(0).gameObject.GetComponent<TouchButton>();
//btn.pressBinding.axisList[0].SetAxis("UseItem2", true);
[RequireComponent(typeof(InputRig))]
public class InputRigHandler : MonoBehaviour
{
    [Serializable]
    public sealed class ShortcutSettings
    {
        public bool enableShortcuts = true;
        public Sprite flashlightSprite, lanternSprite, axeSprite, pistolSprite, shotgunSprite;
    }

    [Serializable]
    public struct GamePanels
    {
        public GameObject InteractPanel;
        public GameObject HelpKeysPanel;
    }

    public static bool IsMobileMode => CF2Input.IsInMobileMode();

    [Header("UI References")]
    public InteractUI interactUI = new();
    public HelpPanelUI helpUI = new();
    public GamePanels gamePanels = new();

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

    private HFPS_GameManager gameManager;

    [SerializeField]
    private ShortcutSettings shortcutSettings = new();
    
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
        gameManager = HFPS_GameManager.Instance;
        gameManager.gamePanels.HelpKeysPanel = gamePanels.HelpKeysPanel;
        gameManager.gamePanels.InteractPanel = gamePanels.InteractPanel;
        gameManager.interactUI = interactUI;
        gameManager.helpUI = helpUI;
    }

    private int visibleShortcutCount = 0;
    internal void UpdateShortcutKeys(List<ShortcutModel> shortcutModels)
    {
        if (!shortcutSettings.enableShortcuts)
        {
            return;
        }

        if (gameManager.isExamining)
        {
            for (int i = 0; i < shortcuts.childCount; i++)
            {
                shortcuts.GetChild(i).gameObject.SetActive(false);
            }
            visibleShortcutCount = 0;
            return;
        }

        if (visibleShortcutCount == shortcutModels.Count)
        {
            return;
        }
        visibleShortcutCount = shortcutModels.Count;

        for (int i = 0; i < shortcuts.childCount; i++)
        {
            shortcuts.GetChild(i).gameObject.SetActive(false);
        }

        foreach (var model in shortcutModels)
        {
            switch (model.shortcut)
            {
                case "UseItem1":
                    shortcuts.GetChild(0).gameObject.SetActive(true);
                    var sprite = shortcuts.GetChild(0).GetChild(0).GetComponent<TouchButtonSpriteAnimator>();
                    sprite.SetStateSprite(TouchButtonSpriteAnimator.ControlState.Neutral, GetShortcutSprite(model.item.Title));
                    break;
                case "UseItem2":
                    shortcuts.GetChild(1).gameObject.SetActive(true);
                    var sprite2 = shortcuts.GetChild(1).GetChild(0).GetComponent<TouchButtonSpriteAnimator>();
                    sprite2.SetStateSprite(TouchButtonSpriteAnimator.ControlState.Neutral, GetShortcutSprite(model.item.Title));
                    break;
                case "UseItem3":
                    shortcuts.GetChild(2).gameObject.SetActive(true);
                    var sprite3 = shortcuts.GetChild(2).GetChild(0).GetComponent<TouchButtonSpriteAnimator>();
                    sprite3.SetStateSprite(TouchButtonSpriteAnimator.ControlState.Neutral, GetShortcutSprite(model.item.Title));
                    break;
                case "UseItem4":
                    shortcuts.GetChild(3).gameObject.SetActive(true);
                    var sprite4 = shortcuts.GetChild(3).GetChild(0).GetComponent<TouchButtonSpriteAnimator>();
                    sprite4.SetStateSprite(TouchButtonSpriteAnimator.ControlState.Neutral, GetShortcutSprite(model.item.Title));
                    break;
                default:
                    break;
            }
        }
    }

    Sprite GetShortcutSprite(string Title)
    {
        return Title switch
        {
            "Flashlight" => shortcutSettings.flashlightSprite,
            "Shotgun" => shortcutSettings.shotgunSprite,
            "Pistol" => shortcutSettings.pistolSprite,
            "Axe" => shortcutSettings.axeSprite,
            "Oil Lamp" => shortcutSettings.lanternSprite,
            _ => null,
        };
    }

    internal void ToggleRotation(bool enabled)
    {
        rotate.SetActive(enabled);
    }

    internal void ToggleInteract(bool enabled)
    {
        use.SetActive(enabled);
    }

    internal void TogglePlayerControls(bool enabled)
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

    internal Sprite GetSprite(string BindingPath, string ControlName)
    {
        
        if (BindingPath == "<Keyboard>/1" || BindingPath == "<Keyboard>/2" || BindingPath == "<Keyboard>/3" || BindingPath == "<Keyboard>/4")
        {
            return ControlName switch
            {
                "TO TURN ON OR OFF " => shortcutSettings.flashlightSprite,
                "EQUIP SHOTGUN" => shortcutSettings.shotgunSprite,
                "EQUIP PISTOL" => shortcutSettings.pistolSprite,
                "EQUIP AXE" => shortcutSettings.axeSprite,
                "LIGHT UP OIL LAMP" => shortcutSettings.lanternSprite,
                _ => null,
            };
        }
        else
        {
            //string path = BindingPath + ControlName;
            return null;
        }
    }
}
