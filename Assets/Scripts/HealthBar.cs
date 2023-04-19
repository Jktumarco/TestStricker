using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IBarHealthable
{
    [SerializeField] private Image barImage;

    private void OnEnable()
    {
        GameController.OnRestartGame += HealthDefault;
    }
    public void Change( float damage)
    {
        if (barImage.fillAmount > 0f)
        {
            damage =  damage / 100;
            Debug.Log(damage);
            barImage.fillAmount -= damage;
            
        }
    }
    void HealthDefault() { barImage.fillAmount = 1; }
    private void OnDisable()
    {
        GameController.OnRestartGame -= HealthDefault;
    }
}
