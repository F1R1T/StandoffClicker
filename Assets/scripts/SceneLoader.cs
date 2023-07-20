using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int scene;
    public void LoadScene()
    {
       SceneManager.LoadScene(scene);
    }
}
