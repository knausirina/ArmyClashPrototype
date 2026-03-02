public class BattleResultService
{
    public Team? LastWinner { get; set; }

    public bool IsWin()
    {
        return LastWinner == Team.Player;
    }
}