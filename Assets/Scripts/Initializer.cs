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
            SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SceneManagerOnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals(PersistenceScene))
        {
            return;
        }
        
        SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
        
        SceneManager.SetActiveScene(scene);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}