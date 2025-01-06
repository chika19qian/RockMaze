using UnityEngine;

public class ShowPanel : MonoBehaviour
{
    public GameObject panel1;

    void OnTriggerEnter(Collider other)
    {
        if (panel1 != null)
        {
            panel1.SetActive(true);
            Debug.Log("Panel is activated");
        }
        else
        {
            Debug.LogError("Panel reference is null in OnTriggerEnter.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (panel1 != null)
        {
            panel1.SetActive(false);
            Debug.Log("Panel is deactivated");
        }
        else
        {
            Debug.LogError("Panel reference is null in OnTriggerExit.");
        }
    }
}
