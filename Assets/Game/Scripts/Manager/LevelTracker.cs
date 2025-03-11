
using UnityEngine;

public class LevelTracker : Singleton<LevelTracker>
{
    [SerializeField] private LevelButton[] m_LevelButtons;

    private void LoadComponent()
    {
        m_LevelButtons = transform.GetComponentsInChildren<LevelButton>();
    }

    protected override void Awake()
    {
        base.Awake();
        LoadComponent();
    }

    private void Reset()
    {
        LoadComponent();
    }

    private void Start()
    {
        LevelProgress levelProgress = JsonSaveSystem.LoadData<LevelProgress>();
        if (levelProgress == null || levelProgress.LevelStates == null)
        {
            for(int i = 0; i < m_LevelButtons.Length; ++i) m_LevelButtons[i].HideFlag();
            SetLevelTrackerInfor(0,0);
            return;
        }
        
        for (int i = 0; i < m_LevelButtons.Length; ++i)
        {
            LevelState levelState = levelProgress.LevelStates[i];
            if(levelState != null) SetLevelTrackerInfor(levelState.LevelIndex,levelState.Stars);
            else m_LevelButtons[i].HideFlag();
        }
    }

    public void SetLevelTrackerInfor(int level, int numberStar)
    {
        if (level >= m_LevelButtons.Length || level < 0)
        {
            Debug.Log("Level index is wrong");
            return;
        }
        
        if (numberStar >= 0)
        {
            m_LevelButtons[level].NumberStar = numberStar;
            m_LevelButtons[level].ShowFlag();
        }
        //unlock level
        else m_LevelButtons[level].HideFlag();
        
    }
    
}
