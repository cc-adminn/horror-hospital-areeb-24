using System;
using System.Collections.Generic;
using ControlFreak2;
using HFPS.Player;
using HFPS.Systems;
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField]
    private Sprite ZoomSprite, ThrowSprite, TouchSprite, JumpSprite;

    [Serializable]
    public struct GamePanels
    {
        public GameObject InteractPanel;
        public GameObject HelpKeysPanel;
    }

    [Serializable]
    public struct MobileControls
    {
        public GameObject joystick;
        public GameObject inventory;
        public GameObject reload;
        public GameObject attack;
        public GameObject zoom;
        public GameObject jump;
        public GameObject run;
        public GameObject crouch;
        public GameObject prone;
        public GameObject shortcuts;
        public GameObject rotate;
        public GameObject pause;

    }

    public static bool IsMobileMode => CF2Input.IsInMobileMode();

    [Header("UI References")]
    public InteractUI interactUI = new();
    public HelpPanelUI helpUI = new();
    public GamePanels gamePanels = new();

    private HFPS_GameManager gameManager;
    private PlayerController controller;

    [SerializeField]
    private ShortcutSettings shortcutSettings = new();
    [SerializeField]
    private MobileControls mobileControls = new();

    private bool IsPlayerControlsEnabled = true;

    private void Start()
    {
        gameManager = HFPS_GameManager.Instance;
        controller = PlayerController.Instance;

        gameManager.gamePanels.HelpKeysPanel = gamePanels.HelpKeysPanel;
        gameManager.gamePanels.InteractPanel = gamePanels.InteractPanel;
        gameManager.interactUI = interactUI;
        gameManager.helpUI = helpUI;

        mobileControls.jump.SetActive(controller.controllerFeatures.enableJump);
        mobileControls.prone.SetActive(controller.controllerFeatures.enableProne);
        mobileControls.run.SetActive(controller.controllerFeatures.enableRun);
        mobileControls.crouch.SetActive(controller.controllerFeatures.enableCrouch);
    }

    void SetControlVisibility(bool enabled, bool visible, GameObject control)
    {
        if (enabled)
        {
            control.SetActive(visible);
        }
    }

    private int visibleShortcutCount = 0;
    internal void UpdateShortcutKeys(List<ShortcutModel> shortcutModels)
    {
        if (!shortcutSettings.enableShortcuts)
        {
            return;
        }

        Transform shortcuts = mobileControls.shortcuts.transform;

        if (shortcutModels == null)
        {
            shortcuts.gameObject.SetActive(false);
        }

        if (gameManager.isExamining || gameManager.isHeld)
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

    internal void SetAttackControls(bool enabled, bool isFireArm = true, string bullets = null, string magazines = null)
    {
        mobileControls.attack.SetActive(enabled && IsPlayerControlsEnabled);
        
        Transform bulletsTransform = mobileControls.attack.transform.GetChild(1);
        if (enabled && IsPlayerControlsEnabled && isFireArm && bullets != null && magazines != null)
        {
            bulletsTransform.gameObject.SetActive(true);
            var bulletsText = bulletsTransform.GetChild(0).GetComponent<Text>();
            var megazinesText = bulletsTransform.GetChild(2).GetComponent<Text>();
            bulletsText.text = bullets;
            megazinesText.text = magazines;

            if (magazines == "0")
            {
                mobileControls.reload.SetActive(false);
                megazinesText.gameObject.SetActive(false);
                bulletsTransform.GetChild(1).gameObject.SetActive(false);

                if (bullets == "0")
                {
                    bulletsText.text = "Empty";
                }
            }
            else
            {
                mobileControls.reload.SetActive(true);
                bulletsTransform.GetChild(1).gameObject.SetActive(true);
                megazinesText.gameObject.SetActive(true);
            }
        }
        else
        {
            bulletsTransform.gameObject.SetActive(false);
            mobileControls.reload.SetActive(false);
        }
        
    }

    internal void ToggleRotation(bool enableObjectRotation, bool objectHeld)
    {
        mobileControls.rotate.SetActive(enableObjectRotation && objectHeld && IsPlayerControlsEnabled);
        mobileControls.zoom.SetActive(!objectHeld && IsPlayerControlsEnabled);
    }

    internal void ToggleInventory(bool enabled)
    {
        mobileControls.inventory.SetActive(enabled && IsPlayerControlsEnabled && !gameManager.isHeld);
    }

    internal void TogglePlayerControls(bool enabled)
    {
        IsPlayerControlsEnabled = enabled;

        mobileControls.joystick.SetActive(enabled);

        mobileControls.pause.SetActive(enabled);

        mobileControls.zoom.SetActive(enabled);

        if (controller == null)
        {
            controller = PlayerController.Instance;
        }

        mobileControls.jump.SetActive(controller.controllerFeatures.enableJump && enabled);
        mobileControls.prone.SetActive(controller.controllerFeatures.enableProne && enabled);
        mobileControls.run.SetActive(controller.controllerFeatures.enableRun && enabled);
        mobileControls.crouch.SetActive(controller.controllerFeatures.enableCrouch && enabled);
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
        else if (BindingPath == "Zoom")
        {
            return ZoomSprite;
        }
        else if (BindingPath+ControlName == "<Mouse>/rightButtonThrow")
        {
            return ThrowSprite;
        }
        else if (BindingPath + ControlName == "<Mouse>/rightButtonInteract")
        {
            return TouchSprite;
        }
        else if (BindingPath + ControlName == "<Keyboard>/spaceExit Ladder")
        {
            return JumpSprite;
        }
        else
        {
            //string path = BindingPath + ControlName;
            return null;
        }
    }
}
