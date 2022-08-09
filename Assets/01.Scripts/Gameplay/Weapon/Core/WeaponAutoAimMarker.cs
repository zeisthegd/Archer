using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;


namespace Penwyn.Game
{
    public class WeaponAutoAimMarker : MonoBehaviour
    {
        public GameObject CornerPrefab;
        public List<GameObject> MarkerCorners = new List<GameObject>();

        protected WeaponAutoAim _weaponAutoAim;
        protected SpriteRenderer targetSR;
        protected Vector3[] _spriteCorners = new Vector3[] { };

        protected virtual void Awake()
        {
            _weaponAutoAim = GetComponent<WeaponAutoAim>();
        }

        protected virtual void Start()
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject corner = Instantiate(CornerPrefab, transform.position, Quaternion.identity);
                MarkerCorners.Add(corner);
                corner.gameObject.SetActive(false);
            }
        }

        protected virtual void Update()
        {
            AimAt();
        }

        public virtual void AimAt()
        {
            if (_weaponAutoAim.Target != null)
            {
                if (!_weaponAutoAim.Target.gameObject.activeInHierarchy)
                {
                    DisableAllCorners();
                    return;
                }
                if (_weaponAutoAim.Target.Find("Model"))
                    targetSR = _weaponAutoAim.Target.Find("Model").GetComponent<SpriteRenderer>();
                if (targetSR == null)
                {
                    DisableAllCorners();
                    return;
                }
                _spriteCorners = SpriteRendererUtil.GetLocalSpriteCorners(targetSR);
                for (int i = 0; i < MarkerCorners.Count; i++)
                {
                    MarkerCorners[i].transform.SetParent(targetSR.transform);
                    MarkerCorners[i].transform.localScale = Vector3.one;
                    MarkerCorners[i].transform.localPosition = _spriteCorners[i];
                    MarkerCorners[i].transform.rotation = Quaternion.Euler(0, 0, 90 * i * targetSR.transform.localScale.x);
                    MarkerCorners[i].gameObject.SetActive(true);
                }
            }
            else
            {
                DisableAllCorners();
            }
        }

        public virtual void DisableAllCorners()
        {
            for (int i = 0; i < MarkerCorners.Count; i++)
            {
                MarkerCorners[i].gameObject.SetActive(false);
            }
        }
    }
}
