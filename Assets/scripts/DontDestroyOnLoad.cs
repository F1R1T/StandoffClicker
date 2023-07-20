using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static bool persistentCanvasExists = false;

    private void Awake()
    {
        if (!persistentCanvasExists)
        {
            DontDestroyOnLoad(gameObject);
            persistentCanvasExists = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
