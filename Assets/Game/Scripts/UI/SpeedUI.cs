using System;

using UnityEngine;
using UnityEngine.UI;

public class SpeedUI : ComponentBehavior
{
    public Sprite DoubleSpeedSprite;
    public Sprite RegularSpeedSprite;
    [SerializeField] private Button speedBtn;
    [SerializeField] private Image speedImg;
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (speedBtn == null) speedBtn = transform.GetComponent<Button>();
        if (speedImg == null) speedImg = transform.GetComponent<Image>();
    }

    private void OnEnable()
    {
        speedBtn.onClick.AddListener(() =>
        {
            if (Math.Abs(Time.timeScale - 1) < 0.01)
            {
                speedImg.sprite = DoubleSpeedSprite;
                GameManager.Instance.GameSpeed = 2;
            }
            else
            {
                speedImg.sprite = RegularSpeedSprite;
                GameManager.Instance.GameSpeed = 1;
            }
        });
    }
}
