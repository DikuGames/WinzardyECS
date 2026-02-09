using UnityEngine;

namespace Code.Gameplay.Cameras.Provider
{
  public class CameraProvider : ICameraProvider
  {
    public Camera MainCamera { get; private set; }

    public float WorldScreenHeight { get; private set; }
    public float WorldScreenWidth { get; private set; }

    public void SetMainCamera(Camera camera)
    {
      MainCamera = camera;

      RefreshBoundaries();
    }

    private void RefreshBoundaries()
    {
      Plane gameplayPlane = new Plane(Vector3.up, Vector3.zero);
      Vector3 bottomLeft = ViewportPointOnPlane(0f, 0f, gameplayPlane);
      Vector3 topRight = ViewportPointOnPlane(1f, 1f, gameplayPlane);

      WorldScreenWidth = Mathf.Abs(topRight.x - bottomLeft.x);
      WorldScreenHeight = Mathf.Abs(topRight.z - bottomLeft.z);
    }

    private Vector3 ViewportPointOnPlane(float viewportX, float viewportY, Plane plane)
    {
      Ray ray = MainCamera.ViewportPointToRay(new Vector3(viewportX, viewportY, 0f));
      return plane.Raycast(ray, out float distance)
        ? ray.GetPoint(distance)
        : MainCamera.transform.position;
    }
  }
}
