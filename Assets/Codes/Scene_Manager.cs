using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public static void back()
    {
        SceneManager.LoadScene("Menu");
    }
    public static void playInfinite()
    {
        SceneManager.LoadScene("Game");
    }
        public static void tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public static void playLevel()
    {
        SceneManager.LoadScene("Game");
    }
}
