using UnityEngine;
using Zenject;

    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "ArmyClash/Installers/SettingsInstaller")]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        [SerializeField] private GameConfig _gameConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_gameConfig).AsSingle();
            Container.BindInstance(_gameConfig.UnitConfig).AsSingle();
            Container.BindInstance(_gameConfig.LevelsConfig).AsSingle();
        }
    }