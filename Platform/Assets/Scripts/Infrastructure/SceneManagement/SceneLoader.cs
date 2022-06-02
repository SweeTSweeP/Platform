using UnityEngine.SceneManagement;

namespace Infrastructure.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public void LoadScene(string sceneName) => 
            SceneManager.LoadSceneAsync(sceneName);
    }
}