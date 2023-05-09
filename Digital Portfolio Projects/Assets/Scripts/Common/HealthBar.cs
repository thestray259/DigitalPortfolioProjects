using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject healthGuy;
    [SerializeField] Image fillImage;
    public float currentHealth;
    public float maxHealth;
    public float fillValue;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        if (healthGuy.activeInHierarchy)
        {
            currentHealth = healthGuy.GetComponentInChildren<Health>().health;
            maxHealth = healthGuy.GetComponentInChildren<Health>().maxHealth;
        }
        else
        {
            currentHealth = 0;
        }

        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        else if (!fillImage.enabled && slider.value > slider.minValue)
        {
            fillImage.enabled = true;
        }


        fillValue = currentHealth / maxHealth;
        slider.value = fillValue;
    }
}
