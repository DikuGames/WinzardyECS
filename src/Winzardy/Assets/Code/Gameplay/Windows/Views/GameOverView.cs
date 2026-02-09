using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.Windows.Views
{
  public class GameOverView : BaseWindow
  {
    [SerializeField] private Button _restartButton;

    public event Action RestartClicked;

    protected override void OnAwake()
    {
      Id = WindowId.GameOverWindow;
    }

    protected override void SubscribeUpdates()
    {
      _restartButton.onClick.AddListener(OnRestartClicked);
    }

    protected override void UnsubscribeUpdates()
    {
      _restartButton.onClick.RemoveListener(OnRestartClicked);
    }

    private void OnRestartClicked()
    {
      RestartClicked?.Invoke();
    }
  }
}
