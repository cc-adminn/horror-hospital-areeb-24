using UnityEngine;

/// <summary>
/// This is a demo script just to debug some behaviours
/// </summary>
public class VisibilityCheck : MonoBehaviour
{
#if UNITY_EDITOR
    void Start()
    {
        Debug.Log(gameObject.name + "Started");
    }

    private void OnEnable()
    {
        Debug.Log(gameObject.name + "OnEnable");
    }

    private void OnDisable()
    {
        Debug.Log(gameObject.name + "OnDisable");
    }

    private void OnDestroy()
    {
        Debug.Log(gameObject.name + "OnDestroy");
    }
#endif
}
