using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
using DG.Tweening;
using Penwyn.Game;

namespace Penwyn.Tools
{
    public class CameraController : MonoBehaviour
    {
        protected CinemachineVirtualCamera _camComponent;

        protected virtual void Awake()
        {
            _camComponent = GetComponent<CinemachineVirtualCamera>();
        }
        public virtual void FollowPlayer()
        {
            _camComponent.Follow = PlayerManager.Instance.Player.transform;
            _camComponent.LookAt = PlayerManager.Instance.Player.transform;
        }

        public virtual void ConnectEvents()
        {
            PlayerManager.PlayerSpawned += FollowPlayer;
        }

        public virtual void DisconnectEvents()
        {
            PlayerManager.PlayerSpawned -= FollowPlayer;

        }

        protected virtual void OnEnable()
        {
            ConnectEvents();
        }

        protected virtual void OnDisable()
        {
            DisconnectEvents();
        }
    }
}

