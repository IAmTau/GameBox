using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlConroller : MonoBehaviour
{
    public static LvlConroller instance = null;
    private int _sceneIndex;

    private int _countLvls = 2;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void IsEndGame()
    {
        if (PlayerPrefs.GetInt($"CoinLvl{_sceneIndex}") < CoinController.counterCoin || !PlayerPrefs.HasKey($"CoinLvl{_sceneIndex}"))
        {
            PlayerPrefs.SetInt($"CoinLvl{_sceneIndex}", CoinController.counterCoin);
        }
        if (PlayerPrefs.GetInt($"XPLvl{_sceneIndex}") < CoinController.counterCoin || !PlayerPrefs.HasKey($"XPLvl{_sceneIndex}"))
        {
            PlayerPrefs.SetInt($"XPLvl{_sceneIndex}", ExperienceController.counterXP);
        }

        CoinController.counterCoin = 0;
        ExperienceController.counterXP = 0;

        if (_sceneIndex == _countLvls)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            PlayerPrefs.SetInt("LevelComplete", _sceneIndex);
            SceneManager.LoadScene(_sceneIndex + 1);
        }
    }
}
