using System.Collections.Generic;
using UnityEngine;

public class SplitFormationStrategy : IFormationStrategy
{
    private readonly SplitFormationConfig _config;

    public SplitFormationStrategy(SplitFormationConfig config)
    {
        _config = config;
    }

    public List<DeploymentCommand> GetDeployment(int count)
    {
        var commands = new List<DeploymentCommand>();

        for (var i = 0; i < count; i++)
        {
            var calculatedPos = CalculatePosition(i, count); 

            commands.Add(new DeploymentCommand {
                Position = calculatedPos
            });
        }
        return commands;
    }

    private Vector3 CalculatePosition(int index, int totalCount)
    {
        var columns = Mathf.Min(_config.SpawnColumns, totalCount);
        if (columns <= 0)
            columns = 1;

        var row = index / columns;
        var col = index % columns;
        var totalWidth = (columns - 1) * _config.UnitSpacing;
        var x = (col * _config.UnitSpacing) - (totalWidth / 2f);
        var z = -(row * _config.UnitSpacing);

        return new Vector3(x, 0, z);
    }
}