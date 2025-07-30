using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    public void StartGame() 
    {
        GameManager.Instance.StartGame();
        SceneManager.LoadScene("GameScene");
    }
}