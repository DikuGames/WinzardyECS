namespace Code.Gameplay.Windows
{
  public interface IWindowService
  {
    BaseWindow Open(WindowId windowId);
    void Close(WindowId windowId);
  }
}
