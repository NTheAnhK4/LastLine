
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public int levelNumber;

    public void OnClick()
    {
        GameManager.Instance.SelectLevel(levelNumber);
    }
}
