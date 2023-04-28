using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChargeGame
{
    public class GameInitializer : MonoBehaviour
    {
        private void Awake()
        {
            //here can be more methods for initialization in future
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene(1);
        }
    }
}

