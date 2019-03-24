using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Utils
{
    public class SceneLoader : MonoBehaviour
    {
        [Tooltip("Name of the scene to load.")]
        public string sceneName;
        
        public void LoadScene(float _delay = 0.0f)
        {
            if (_delay < 0.0f) _delay = 0.0f;
            
            Invoke(nameof(Load), _delay);
        }
        
        private void Load()
        {          
            SceneManager.LoadScene(sceneName);
        }
    }
}