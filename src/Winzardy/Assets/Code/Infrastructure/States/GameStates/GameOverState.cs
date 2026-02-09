using Code.Gameplay.Windows;
using Code.Gameplay.Windows.Views;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class GameOverState : SimpleState
  {
    private readonly IWindowService _windowService;
    private readonly IGameStateMachine _stateMachine;
    private GameOverView _gameOverView;

    public GameOverState(IWindowService windowService, IGameStateMachine stateMachine)
    {
      _windowService = windowService;
      _stateMachine = stateMachine;
    }
    
    public override void Enter()
    {
      _gameOverView = _windowService.Open(WindowId.GameOverWindow) as GameOverView;
      if (_gameOverView == null)
      {
        Debug.LogError("Window with id GameOverWindow must have GameOverView component.");
        return;
      }

      _gameOverView.RestartClicked += OnRestartClicked;
    }

    protected override void Exit()
    {
      if (_gameOverView != null)
        _gameOverView.RestartClicked -= OnRestartClicked;

      _windowService.Close(WindowId.GameOverWindow);
      _gameOverView = null;
    }

    private void OnRestartClicked()
    {
      _stateMachine.Enter<BattleEnterState>();
    }
  }
}
