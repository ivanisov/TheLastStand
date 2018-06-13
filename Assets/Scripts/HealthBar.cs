using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public RectTransform healthLine;

    private RectTransform healthBar;
	private float healthBarWidth;
	
    private const float maxHp = 100;
    private const float border = 2;

    void Awake()
    {
        healthBar = GetComponent<RectTransform>();
		healthBarWidth = healthBar.sizeDelta.x - border;
    }

    public void UpdateHealth(float health)
    {
        float widthLine = (healthBarWidth / maxHp) * (int)health;
        healthLine.sizeDelta = new Vector2(widthLine, healthLine.sizeDelta.y);
    }
}
