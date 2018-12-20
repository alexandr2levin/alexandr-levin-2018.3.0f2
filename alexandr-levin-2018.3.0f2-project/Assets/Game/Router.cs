using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Router : MonoBehaviour
    {
        
        public void ToMenu()
        {
            Debug.Log("To menu");
            //SceneManager.LoadScene("Game/Menu/Menu");
        }

        public void ToGameplay()
        {
            Debug.Log("To gameplay");
            //SceneManager.LoadScene("Game/Gameplay/Gameplay");
        }
    }
}