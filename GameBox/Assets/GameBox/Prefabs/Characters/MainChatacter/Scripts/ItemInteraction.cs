using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int _damageCoinTrap = 1;
    [SerializeField, Range(1, 9)] private int _healthHearth = 1;

    private int _startHealth;

    private void Start()
    {
        _startHealth = UIController.countHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            CoinController.counterCoin += 1;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("CoinTrap"))
        {
            UIController.countHealth -= _damageCoinTrap;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Hearth") && UIController.countHealth < _startHealth)
        {
            UIController.countHealth += _healthHearth;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("BombExplosion"))
        {
            UIController.countHealth -= new Constant().damageBomb;
        }
    }
}
