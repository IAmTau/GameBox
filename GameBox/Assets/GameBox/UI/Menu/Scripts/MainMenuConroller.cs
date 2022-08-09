using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuConroller : MonoBehaviour
{
    [SerializeField] private Button _level1;
    [SerializeField] private Button _level2;

    [SerializeField] private Text[] _counterCoin;
    [SerializeField] private Text[] _counterXP;

    private int _levelComplete;
    private int _countLvl = 2;

    private void Start()
    {
        _levelComplete = PlayerPrefs.GetInt("LevelComplete");
        _level2.interactable = false;

        switch (_levelComplete)
        {
            case 1:
                _level2.interactable = true;
                break;
        }

        for (int i = 0; i < _counterCoin.Length; i++)
        {
            if (PlayerPrefs.HasKey($"CoinLvl{i + 1}"))
            {
                _counterCoin[i].text = PlayerPrefs.GetInt($"CoinLvl{i + 1}").ToString();
            }
        }

        for (int i = 0; i < _counterXP.Length; i++)
        {
            if (PlayerPrefs.HasKey($"XPLvl{i + 1}"))
            {
                _counterXP[i].text = PlayerPrefs.GetInt($"XPLvl{i + 1}").ToString();
            }
        }
    }

    public void LoadTo(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Reset()
    {
        _level2.interactable = false;

        for (int i = 0; i < 2; i++)
        {
            if (PlayerPrefs.HasKey($"CoinLvl{i + 1}"))
            {
                _counterCoin[i].text = "0";
            }
        }

        for (int i = 0; i < 2; i++)
        {
            if (PlayerPrefs.HasKey($"XPLvl{i + 1}"))
            {
                _counterXP[i].text = "0";
            }
        }

        PlayerPrefs.DeleteAll();
    }
}