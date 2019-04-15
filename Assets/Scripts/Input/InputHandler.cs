using JetBrains.Annotations;
using Zenject;

namespace Assets.Scripts.Input
{
    /// <summary>
    /// This script is responsible for handling input - mouse button presses.
    /// 
    /// Walking input is provided by Unity3D standard assets - FPSController
    /// </summary>
    public class InputHandler : ITickable {

        #region Public Methods

        [UsedImplicitly]
        public InputHandler(
            Raycasting rayCasting,
            IGameInput gameInput) {
            _Input = gameInput;
            _RayCasting = rayCasting;
        }

        [UsedImplicitly]
        public void Tick() {
            HandleInput();
        }

        #endregion Public Methods


        #region Private Methods

        private void HandleInput() {
            if (_Input.LeftMouseButtonPressed()) {
                _RayCasting.CastRay();
            }
        }

        #endregion Private Methods


        #region Private Variables

        private readonly IGameInput _Input;
        private readonly Raycasting _RayCasting;

        #endregion Private Variables

    }
}
