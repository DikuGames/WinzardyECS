using System.Collections.Generic;
using Code.Gameplay.Common.Collisions;
using UnityEngine;

namespace Code.Gameplay.Common.Physics
{
  public class PhysicsService : IPhysicsService
  {
    private const float OverlapPointRadius = 0.01f;

    private static readonly RaycastHit[] Hits = new RaycastHit[128];
    private static readonly Collider[] OverlapHits = new Collider[128];
    
    private readonly ICollisionRegistry _collisionRegistry;

    public PhysicsService(ICollisionRegistry collisionRegistry)
    {
      _collisionRegistry = collisionRegistry;
    }

    public IEnumerable<GameEntity> RaycastAll(Vector3 worldPosition, Vector3 direction, int layerMask)
    {
      int hitCount = UnityEngine.Physics.RaycastNonAlloc(worldPosition, direction, Hits, Mathf.Infinity, layerMask);

      for (int i = 0; i < hitCount; i++)
      {
        RaycastHit hit = Hits[i];
        Collider collider = hit.collider;
        if (collider == null)
          continue;

        GameEntity entity = _collisionRegistry.Get<GameEntity>(collider.GetInstanceID());
        if (entity == null)
          continue;

        yield return entity;
      }
    }

    public GameEntity Raycast(Vector3 worldPosition, Vector3 direction, int layerMask)
    {
      int hitCount = UnityEngine.Physics.RaycastNonAlloc(worldPosition, direction, Hits, Mathf.Infinity, layerMask);

      for (int i = 0; i < hitCount; i++)
      {
        RaycastHit hit = Hits[i];
        Collider collider = hit.collider;
        if (collider == null)
          continue;

        GameEntity entity = _collisionRegistry.Get<GameEntity>(collider.GetInstanceID());
        if (entity == null)
          continue;

        return entity;
      }

      return null;
    }

    public GameEntity LineCast(Vector3 start, Vector3 end, int layerMask)
    {
      Vector3 delta = end - start;
      float distance = delta.magnitude;
      if (distance <= 0f)
        return null;

      int hitCount = UnityEngine.Physics.RaycastNonAlloc(start, delta.normalized, Hits, distance, layerMask);

      for (int i = 0; i < hitCount; i++)
      {
        RaycastHit hit = Hits[i];
        Collider collider = hit.collider;
        if (collider == null)
          continue;

        GameEntity entity = _collisionRegistry.Get<GameEntity>(collider.GetInstanceID());
        if (entity == null)
          continue;

        return entity;
      }

      return null;
    }
    
    public IEnumerable<GameEntity> CircleCast(Vector3 position, float radius, int layerMask) 
    {
      int hitCount = OverlapCircle(position, radius, OverlapHits, layerMask);

      DrawDebug(position, radius, 1f, Color.red);
      
      for (int i = 0; i < hitCount; i++)
      {
        Collider collider = OverlapHits[i];
        if (collider == null)
          continue;

        GameEntity entity = _collisionRegistry.Get<GameEntity>(collider.GetInstanceID());
        if (entity == null)
          continue;

        yield return entity;
      }
    }

    public int CircleCastNonAlloc(Vector3 position, float radius, int layerMask, GameEntity[] hitBuffer) 
    {
      int hitCount = OverlapCircle(position, radius, OverlapHits, layerMask);

      DrawDebug(position, radius, 1f, Color.green);

      int resolvedCount = 0;
      for (int i = 0; i < hitCount; i++)
      {
        Collider collider = OverlapHits[i];
        if (collider == null)
          continue;

        GameEntity entity = _collisionRegistry.Get<GameEntity>(collider.GetInstanceID());
        if (entity == null)
          continue;

        if (resolvedCount < hitBuffer.Length)
          hitBuffer[resolvedCount] = entity;

        resolvedCount++;
      }

      for (int i = resolvedCount; i < hitBuffer.Length; i++)
        hitBuffer[i] = null;

      return resolvedCount;
    }

    public TEntity OverlapPoint<TEntity>(Vector3 worldPosition, int layerMask) where TEntity : class
    {
      int hitCount = UnityEngine.Physics.OverlapSphereNonAlloc(worldPosition, OverlapPointRadius, OverlapHits, layerMask);

      for (int i = 0; i < hitCount; i++)
      {
        Collider hit = OverlapHits[i];
        if (hit == null)
          continue;

        TEntity entity = _collisionRegistry.Get<TEntity>(hit.GetInstanceID());
        if (entity == null)
          continue;

        return entity;
      }

      return null;
    }

    public int OverlapCircle(Vector3 worldPos, float radius, Collider[] hits, int layerMask) =>
      UnityEngine.Physics.OverlapSphereNonAlloc(worldPos, radius, hits, layerMask);
    
    private static void DrawDebug(Vector3 worldPos, float radius, float seconds, Color color)
    {
      Debug.DrawRay(worldPos, radius * Vector3.forward, color, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.back, color, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.left, color, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.right, color, seconds);
    }
  }
}
