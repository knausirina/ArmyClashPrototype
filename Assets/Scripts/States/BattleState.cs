using UnityEngine;

public class BattleState : IState
{
    private readonly BattleManager _battleManager;

    public BattleState(BattleManager battleManager)
    {
        _battleManager = battleManager;
    }

    public void Enter()
    {
        _battleManager.StartBattle();
    }

    public void Exit()
    {
    }
}