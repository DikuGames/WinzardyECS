using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Windows
{
  public class WindowService : IWindowService
  {
    private readonly IWindowFactory _windowFactory;

    private readonly List<BaseWindow> _openedWindows = new();

    public WindowService(IWindowFactory windowFactory) =>
      _windowFactory = windowFactory;

    public BaseWindow Open(WindowId windowId)
    {
      BaseWindow window = _windowFactory.CreateWindow(windowId);
      _openedWindows.Add(window);
      return window;
    }

    public void Close(WindowId windowId)
    {
      BaseWindow window = _openedWindows.Find(x => x.Id == windowId);
      if (window == null)
        return;
      
      _openedWindows.Remove(window);
      
      Object.Destroy(window.gameObject);
    }
  }
}
