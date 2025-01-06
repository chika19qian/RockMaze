using UnityEngine;

public class PanelReferenceSetter : MonoBehaviour
{
    void Start()
    {
        ShowPanel showPanelScript = GetComponent<ShowPanel>();
        if (showPanelScript != null && PanelManager.Instance != null)
        {
            showPanelScript.panel1 = PanelManager.Instance.panel1;
        }
        else
        {
            Debug.LogError("PanelManager or ShowPanel script not found!");
        }
    }
}
