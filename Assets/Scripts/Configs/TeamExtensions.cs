public static class TeamExtensions
{
    public static Team GetOpposite(this Team team)
    {
        return team == Team.Player ? Team.Enemy : Team.Player;
    }
    
    public static bool IsPlayer(this Team team)
    {
        return team == Team.Player;
    }
}