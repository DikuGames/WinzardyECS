using UnityEngine;

namespace Code.Gameplay.Input.Service
{
  public class StandaloneInputService : IInputService
  {
    public bool HasAxisInput() => GetHorizontalAxis() != 0 || GetVerticalAxis() != 0;
    
    public float GetVerticalAxis() => UnityEngine.Input.GetAxis("Vertical");
    public float GetHorizontalAxis() => UnityEngine.Input.GetAxis("Horizontal");
  }
}