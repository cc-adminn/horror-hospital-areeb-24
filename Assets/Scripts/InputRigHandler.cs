using System.Collections;
using System.Collections.Generic;
using ControlFreak2;
using UnityEngine;
using static HFPS.Systems.Inventory;

[RequireComponent(typeof(InputRig))]
public class InputRigHandler : MonoBehaviour
{

    private Transform shortcuts;
    
    private void Awake()
    {
        var rigPanel = CF2Input.activeRig.transform.GetChild(0).GetChild(0);
        var rigButtons = rigPanel.GetChild(2);
        shortcuts = rigButtons.Find("Shortcuts");
    }

    public void UpdateShortcutKeys(List<ShortcutModel> shortcutModels)
    {
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
}
