using UnityEngine;
using Zenject;

    public class Installer : MonoInstaller
    {
        [SerializeField] private ParticleSystem _damageParticleSystem;
        [SerializeField] private GameObject _unitStatsUiPrefab;

        private const int UnitUIPoolSize = 20;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.Bind<IUnitFactory>().To<UnitFactory>().AsSingle();
            Container.Bind<ITargetingService>().To<TargetingService>().AsSingle();
            Container.Bind<EffectsService>().AsSingle().WithArguments(_damageParticleSystem);
            Container.Bind<BattleResultService>().AsSingle();

            Container.BindInterfacesAndSelfTo<BattleManager>().AsSingle();
            
            Container.Bind<UnitStorage>().AsSingle();
            Container.Bind<UnitStatsStorage>().AsSingle();
            
            Container.Bind<UnitUIManager>().FromComponentInHierarchy().AsSingle();
            
            Container.BindMemoryPool<UnitStatsView, UnitStatsViewPool>()
                .WithInitialSize(UnitUIPoolSize)
                .FromComponentInNewPrefab(_unitStatsUiPrefab)
                .UnderTransformGroup("UI_Pool_UnitStats");
            
            Container.Bind<LevelNavigationService>().AsSingle();
            Container.Bind<LevelsStorage>().AsSingle();

            Container.DeclareSignal<StartBattleSignal>();
            Container.DeclareSignal<RandomizeArmiesSignal>();

            RegisterStates();
        }

        private void RegisterStates()
        {
            Container.BindInterfacesAndSelfTo<MainMenuState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LobbyState>().AsSingle();
            Container.BindInterfacesAndSelfTo<BattleState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResultState>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
    
            Container.DeclareSignal<GameStateChangedSignal>();
            Container.DeclareSignal<NextLevelRequestedSignal>();
            Container.DeclareSignal<ChangeStateRequestSignal>();
        }
    }
