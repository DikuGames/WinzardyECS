namespace Code.Gameplay.Input.Service
{
  public interface IInputService
  {
    float GetVerticalAxis();
    float GetHorizontalAxis();
    bool HasAxisInput();
  }
}