using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("LevelCompelete") < SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("LevelCompelete", SceneManager.GetActiveScene().buildIndex);
            }
            LvlConroller.instance.IsEndGame();
        }
    }
}
