public class TargetingService : ITargetingService
{
    private readonly UnitStorage _unitStorage;

    public TargetingService(UnitStorage unitStorage)
    {
        _unitStorage = unitStorage;
    }

    public Unit FindEnemy(Unit me)
    {
        var enemies = _unitStorage.GetTeam(me.Team.GetOpposite());
    
        Unit nearest = null;
        var minSqrDistance = float.MaxValue;
        var myPos = me.transform.position;

        for (var i = 0; i < enemies.Count; i++)
        {
            var target = enemies[i];
            if (target == null || target.IsDead)
                continue;

            var sqrDist = (target.transform.position - myPos).sqrMagnitude;
            if (sqrDist < minSqrDistance)
            {
                minSqrDistance = sqrDist;
                nearest = target;
            }
        }
        return nearest;
    }
}