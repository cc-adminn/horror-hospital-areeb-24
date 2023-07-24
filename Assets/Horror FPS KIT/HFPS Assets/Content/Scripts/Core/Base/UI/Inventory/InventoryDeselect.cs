using UnityEngine;
using UnityEngine.EventSystems;
using HFPS.Systems;

namespace HFPS.UI
{
    public class InventoryDeselect : MonoBehaviour, IPointerClickHandler
    {
        private Inventory inventory;
        private HFPS_GameManager gameManager;

        void Awake()
        {
            inventory = transform.root.GetComponentInChildren<Inventory>();

            if (HFPS_GameManager.HasReference)
            {
                gameManager = HFPS_GameManager.Instance;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            inventory.ResetInventory();

            gameManager.OnInventory();
        }
    }
}