using Code.Gameplay;
using Code.Gameplay.Features.Abilities.Factory;
using Code.Gameplay.Features.Hero.Factory;
using Code.Gameplay.Levels;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.Systems;

namespace Code.Infrastructure.States.GameStates
{
  public class BattleEnterState : SimpleState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly ILevelDataProvider _levelDataProvider;
    private readonly IHeroFactory _heroFactory;
    private readonly ISystemFactory _systems;
    private readonly GameContext _gameContext;
    private BattleFeature _battleFeature;
    private IAbilityFactory _abilityFactory;

    public BattleEnterState(
      IGameStateMachine stateMachine, 
      ILevelDataProvider levelDataProvider, 
      IHeroFactory heroFactory, IAbilityFactory abilityFactory)
    {
      _stateMachine = stateMachine;
      _levelDataProvider = levelDataProvider;
      _heroFactory = heroFactory;
      _abilityFactory = abilityFactory;
    }
    
    public override void Enter()
    {
      PlaceHero();  
      
      _stateMachine.Enter<BattleLoopState>();
    }

    private void PlaceHero()
    {
      _heroFactory.Create(_levelDataProvider.StartPoint);
      _abilityFactory.CreateProjectileAbility();
    }
  }
}