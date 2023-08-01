using HFPS.Player;
using HFPS.Systems;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HFPS.UI
{
    public class DeselectPaper : MonoBehaviour, IPointerClickHandler
    {
        private ExamineManager examineManager;
        private HFPS_GameManager gameManager;

        void Awake()
        {
            if (ExamineManager.HasReference)
            {
                examineManager = ExamineManager.Instance;
            }

            if (HFPS_GameManager.HasReference)
            {
                gameManager = HFPS_GameManager.Instance;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            examineManager.CancelExamine();
        }
    }
}