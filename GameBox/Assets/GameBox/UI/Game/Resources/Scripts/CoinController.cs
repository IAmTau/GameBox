using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    public static int counterCoin;
    private Text _text;

    private void Start()
    {
        counterCoin = 0;
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        _text.text = counterCoin.ToString();
    }
}
