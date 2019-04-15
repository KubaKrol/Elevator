using System;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Elevator {

    /// <summary>
    /// Script for debugging elevator mechanism.
    /// </summary>

    [Serializable]
    public sealed class ElevatorDebug : MonoBehaviour
    { 
        #region Public Methods

        [Inject]
        public void Construct(
            Elevator elevator)
        {
            _Elevator = elevator;
        }

        [PublicAPI]
        public void PushGetButtonUp(int floor)
        {
            _Elevator.GetButtonsUp[floor].OnClick();
        }

        [PublicAPI]
        public void PushGetButtonDown(int floor) {
            _Elevator.GetButtonsDown[floor].OnClick();
        }

        [PublicAPI]
        public void PushSetButton(int floor) {
            _Elevator.SetButtons[floor].OnClick();
        }

        #endregion Public Methods


        #region Private Variables

        private Elevator _Elevator;

        #endregion Private Variables
    }
}

