using UnityEngine;
using Zenject;

public class ResultState : IState
{
    private readonly BattleManager _battleManager;

    public ResultState(BattleManager battleManager)
    {
        _battleManager = battleManager;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
        _battleManager.ClearArmies();
    }
}
