using UnityEngine;
using UnityEngine.UI;

public class ResetHorizontalGroup : MonoBehaviour
{
    private LayoutGroup group;

    void Start()
    {
        group = GetComponent<LayoutGroup>();
    }

    void Update()
    {
        group.enabled = false;
        group.enabled = true;
    }
}
