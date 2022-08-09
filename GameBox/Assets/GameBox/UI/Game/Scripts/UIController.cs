using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static int countHealth = 10;

    [SerializeField] private Sprite[] _health;
    [SerializeField] private Image _imageHealth;

    private int _startHeal;

    private void Start()
    {
        _startHeal = new Constant().startNumberOfHealth;

        if (_health != null && _imageHealth != null)
        {
            countHealth = _startHeal;
        }
    }

    private void Update()
    {
        if (countHealth < 0)
        {
            countHealth = 0;
        }
        if (countHealth > _startHeal)
        {
            countHealth = _startHeal;
        }

        _imageHealth.sprite = _health[countHealth];
    }
}
