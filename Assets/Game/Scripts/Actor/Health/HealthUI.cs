
using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthUI : ComponentBehavior
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Transform healthHolder;
    [SerializeField] private float timeHide = 7;
    [SerializeField] private float timer = 0;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (healthHolder == null) healthHolder = transform.Find("Health");
        if (healthBar == null) healthBar = healthHolder.Find("Mask").GetComponent<Image>();
        healthBar.fillAmount = 1;
        
        //Hide the health bar initially
        healthHolder.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        healthBar.fillAmount = 1;
        timer = timeHide;
    }

    public void OnHpChange(float healthRate)
    {
        // hire health ui when timer >= timeHide
        if(timer >= timeHide) healthHolder.gameObject.SetActive(true);
        healthBar.fillAmount = healthRate;
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeHide) healthHolder.gameObject.SetActive(false);
    }
}
