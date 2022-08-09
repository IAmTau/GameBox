using UnityEngine;
using UnityEngine.UI;

public class ExperienceController : MonoBehaviour
{
    public static int counterXP;
    private Text _text;

    private void Start()
    {
        counterXP = 0;
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        _text.text = counterXP.ToString();
    }
}
