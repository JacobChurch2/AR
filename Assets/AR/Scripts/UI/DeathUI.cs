using UnityEngine;

public class DeathUI : MonoBehaviour
{
    [SerializeField] private GameObject uiPrefab;
    private GameObject uiInstance;

    public void ToggleOrInstantiateUI()
    {
        if (uiInstance == null) uiInstance = Instantiate(uiPrefab, transform);
        else uiInstance.SetActive(!uiInstance.activeSelf);
    }
}
