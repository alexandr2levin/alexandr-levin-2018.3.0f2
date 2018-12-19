using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Router : MonoBehaviour
    {
        
        public void ToMenu()
        {
            SceneManager.LoadScene("Game/Menu/Menu");
        }

        public void ToGameplay()
        {
            SceneManager.LoadScene("Game/Gameplay/Gameplay");
        }
    }
}