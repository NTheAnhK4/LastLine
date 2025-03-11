
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int levelNumber;
    public int NumberStar;
    public Sprite goldStarSprite;
    public Sprite emptyStarSprite;
    public List<Image> Stars;

    public void OnClick()
    {
        GameManager.Instance.SelectLevel(levelNumber);
    }

    public void ShowFlag()
    {
        transform.gameObject.SetActive(true);
        if (NumberStar > 0)
        {
            for (int i = 0; i < NumberStar; ++i) Stars[i].sprite = goldStarSprite;
            for (int i = NumberStar; i < 3; ++i) Stars[i].sprite = emptyStarSprite;
            for(int i = 0; i < 3; ++i) Stars[i].gameObject.SetActive(true);
        }
        else
        {
            for(int i = 0; i < 3; ++i) Stars[i].gameObject.SetActive(false);
        }
        
    }

    public void HideFlag()
    {
        transform.gameObject.SetActive(false);
    }
}
