using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    [SerializeField] private bool autoload = false;
    [SerializeField] private int autoloadSceneIndex = 0;

    private const string PersistenceScene = "Persistence";

    private void Start()
    {
        if (!SceneManager.GetSceneByName(PersistenceScene).isLoaded)
        {
            SceneManager.LoadScene(PersistenceScene, LoadSceneMode.Additive);
        }

        if (autoload && gameObject.scene.buildIndex != autoloadSceneIndex)
        {
            SceneManager.LoadScene(autoloadSceneIndex, LoadSceneMode.Additive);
        }
    }
}