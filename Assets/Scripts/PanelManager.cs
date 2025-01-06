using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance;
    public GameObject panel1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
