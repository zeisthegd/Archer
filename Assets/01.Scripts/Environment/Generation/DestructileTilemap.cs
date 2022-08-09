using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using Penwyn.Tools;
namespace Penwyn.Game
{
    public class DestructileTilemap : MonoBehaviour
    {
        public List<string> TargetTags;
        public LayerMask TargetLayer;
        public LayerMask ContactPointMaskLayer;
        protected Tilemap _tilemap;
        public event UnityAction<Vector3> TileDestroyed;

        protected virtual void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
        }

        public virtual void OnTriggerEnter2D(Collider2D collider)
        {
            if (ObjectIsInTag(collider.gameObject) && TargetLayer.Contains(collider.gameObject.layer))
            {
                Vector3 colPoint = collider.transform.position;
                Vector3 hitPoint = GetHitPoint(colPoint);
                _tilemap.SetTile(_tilemap.WorldToCell(hitPoint), null);
                TileDestroyed?.Invoke(_tilemap.CellToWorld(_tilemap.WorldToCell(hitPoint)));
            }
        }

        public virtual void OnCollisionEnter2D(Collision2D collision2D)
        {
            if (ObjectIsInTag(collision2D.gameObject) && TargetLayer.Contains(collision2D.gameObject.layer))
            {
                Vector3 hitPosition = Vector3.zero;
                foreach (ContactPoint2D hit in collision2D.contacts)
                {
                    hitPosition.x = hit.point.x + 0.5F * hit.normal.x;
                    hitPosition.y = hit.point.y + 0.5F * hit.normal.y;
                    _tilemap.SetTile(_tilemap.WorldToCell(hitPosition), null);

                }
            }
        }

        public Vector3 GetHitPoint(Vector3 origin)
        {
            Vector3 hitPoint = Vector3.zero;
            float minDst = 1000;
            RaycastHit2D hit = new RaycastHit2D();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    hit = Physics2D.Raycast(origin, new Vector2(i, j), 1, ContactPointMaskLayer);
                    if (hit)
                    {
                        if (Vector2.Distance(origin, hit.point) < minDst)
                        {
                            minDst = Vector2.Distance(origin, hit.point);
                            hitPoint = hit.point;
                        }
                    }
                }
            }
            Debug.DrawRay(origin, Vector3.up * 2, Color.red, 1);
            Debug.DrawRay(hitPoint, Vector3.one * 2, Color.blue, 1);

            Vector3 dir = (hitPoint - origin).normalized * 0.5F;

            if (_tilemap.HasTile(new Vector3Int((int)(hitPoint.x + dir.x), (int)(hitPoint.y), 0)))
                hitPoint = new Vector3(hitPoint.x + dir.x, hitPoint.y, 0);//Check horizontal tiles.
            else if (_tilemap.HasTile(new Vector3Int((int)(hitPoint.x), (int)(hitPoint.y + dir.y), 0)))
                hitPoint = new Vector3(hitPoint.x, hitPoint.y + dir.y, 0);//Check vertical tiles.
            else if (_tilemap.HasTile(new Vector3Int((int)(hitPoint.x + dir.x), (int)(hitPoint.y + dir.y), 0)))
                hitPoint = new Vector3(hitPoint.x + dir.x, hitPoint.y + dir.y, 0);//Check diagonal tiles.

            Debug.DrawRay(hitPoint, Vector3.up * 2, Color.green, 1);
            return hitPoint;
        }

        protected virtual bool ObjectIsInTag(GameObject gameObject)
        {
            foreach (string tag in TargetTags)
            {
                if (gameObject.gameObject.CompareTag(tag))
                    return true;
            }
            return false;
        }

    }
}


