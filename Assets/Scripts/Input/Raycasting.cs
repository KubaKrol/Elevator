using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Input
{
    public class Raycasting
    {
        #region Public Types

        [Serializable]
        public class Settings {

            public Camera PlayerCamera;

            public float RaycastLength = 2f;
        }

        #endregion Public Types


        #region Public Methods

        [UsedImplicitly]
        public Raycasting(
            Settings settings) {
            _Settings = settings;
        }

        [PublicAPI]
        public void CastRay() {

            var ray = _Settings.PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (!Physics.Raycast(ray, out var hit, _Settings.RaycastLength)) return;

            var hitObject = hit.collider.GetComponent<IClickable>();

            hitObject?.OnClick();
        }

        #endregion Public Methods


        #region Private Variables

        private readonly Settings _Settings;

        #endregion Private Variables
    }
}

