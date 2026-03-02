    using System.Collections.Generic;

    public interface IFormationStrategy
    {
        List<DeploymentCommand> GetDeployment(int count);
    }