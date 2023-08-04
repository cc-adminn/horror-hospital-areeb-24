using UnityEngine;
using UnityEngine.UI;

public class ResetHorizontalGroup : MonoBehaviour
{
    private HorizontalLayoutGroup group;

    void Start()
    {
        group = GetComponent<HorizontalLayoutGroup>();
    }

    void Update()
    {
        group.enabled = false;
        group.enabled = true;
    }
}
