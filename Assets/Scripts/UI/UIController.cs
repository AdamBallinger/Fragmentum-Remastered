using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance { get; private set; }

        [SerializeField]
        [Tooltip("All objects in this list will be kept loaded through scene changes.")]
        private List<GameObject> keepLoaded;

        private void Start()
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
            
            foreach(var obj in keepLoaded)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }
}
