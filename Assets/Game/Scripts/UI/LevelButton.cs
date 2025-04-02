
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int levelNumber;
    private Image m_LevelImage;
    
    [Header("Star")]
    public int NumberStar;
    public Sprite goldStarSprite;
    public Sprite emptyStarSprite;
    public List<Image> Stars;

    public Image LevelImage
    {
        get
        {
            if (m_LevelImage == null) m_LevelImage = transform.GetComponent<Image>();
            return m_LevelImage;
        }
    }

    public void OnClick()
    {
        GameManager.Instance.SelectLevel(levelNumber);
    }

    public void ShowFlag(Sprite FinishLevel, Sprite UnFinishLevel)
    {
        transform.gameObject.SetActive(true);
        if (NumberStar > 0)
        {
            LevelImage.sprite = FinishLevel;
            for (int i = 0; i < NumberStar; ++i) Stars[i].sprite = goldStarSprite;
            for (int i = NumberStar; i < 3; ++i) Stars[i].sprite = emptyStarSprite;
            for(int i = 0; i < 3; ++i) Stars[i].gameObject.SetActive(true);
        }
        else
        {
            LevelImage.sprite = UnFinishLevel;
            for(int i = 0; i < 3; ++i) Stars[i].gameObject.SetActive(false);
        }
        
    }

    public void HideFlag()
    {
        transform.gameObject.SetActive(false);
    }
}
