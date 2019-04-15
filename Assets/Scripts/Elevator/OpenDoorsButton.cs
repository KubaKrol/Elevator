using System;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Elevator {

    /// <summary>
    /// Button responsible for opening the door from inside;
    /// </summary>
    [Serializable]
    public sealed class OpenDoorsButton : MonoBehaviour, IClickable
    {

        #region Public Types

        [Serializable]
        public class Settings
        {
            public AudioClip ElevatorButtonSound;
        }

        #endregion Public Types


        #region Public Methods

        [Inject]
        public void Construct(
            Settings settings,
            Elevator elevator) 
        {
            _Settings = settings;
            _Elevator = elevator;
        }

        [PublicAPI]
        public void OnClick()
        {
            if (_Elevator.Doors[_Elevator.CurrentFloor].CurrentState != ElevatorDoors.State.Closed)
                return;

            _MyAudio.PlayOneShot(_Settings.ElevatorButtonSound);
            _Elevator.Doors[_Elevator.CurrentFloor].Open();
        }

        #endregion Public Methods


        #region Unity Methods

        private void Awake() 
        {
            _MyAudio = GetComponent<AudioSource>();
        }

        private void Start() {
            _MyAudio.clip = _Settings.ElevatorButtonSound;
        }

        #endregion Unity Methods


        #region Private Variables

        private Settings _Settings;
        private Elevator _Elevator;
        private Material _MyMaterial;
        private AudioSource _MyAudio;

        #endregion Private Variables
    }
}

