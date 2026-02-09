using UnityEngine;

namespace Code.Gameplay.Input.Service
{
  public class StandaloneInputService : IInputService
  {
    private const float DeadZone = 0.01f;
    
    public bool HasAxisInput()
    {
      float horizontal = GetHorizontalAxis();
      float vertical = GetVerticalAxis();
      return horizontal * horizontal + vertical * vertical > DeadZone * DeadZone;
    }
    
    public float GetVerticalAxis() => UnityEngine.Input.GetAxisRaw("Vertical");
    public float GetHorizontalAxis() => UnityEngine.Input.GetAxisRaw("Horizontal");
  }
}
