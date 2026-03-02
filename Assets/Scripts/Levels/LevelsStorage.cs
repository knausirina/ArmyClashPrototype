using System;

public class LevelsStorage
{
    private readonly LevelsConfig _config;
    
    private int _currentLevelIndex = 0;
    private int _maxUnlockedLevel = 0;

    public LevelsStorage(LevelsConfig config)
    {
        _config = config;
    }

    public LevelConfig GetCurrentLevel()
    {
        return _config.Levels [_currentLevelIndex];
    }

    public bool IsLastLevel()
    {
        return _currentLevelIndex >= _config.Levels.Count - 1;
    }

    public void SetNext()
    {
        if (!IsLastLevel())
            _currentLevelIndex++;
        _maxUnlockedLevel = Math.Max(_maxUnlockedLevel, _currentLevelIndex);
    }

    public void ResetToStart()
    {
        _currentLevelIndex = 0;
    }
}