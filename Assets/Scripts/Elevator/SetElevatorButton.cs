using System;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Elevator {
    /// <summary>
    /// Button responsible for setting the elevator on desired floor;
    /// </summary>

    [Serializable]
    public sealed class SetElevatorButton : MonoBehaviour, IClickable {

        #region Public Types

        [Serializable]
        public class Settings
        {
            public AudioClip ElevatorButtonSound;
        }

        #endregion Public Types


        #region Public Variables

        public bool IsActive;

        public int Floor;

        #endregion Public Variables


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
            _MyMaterial.color = Color.red;

            _MyAudio.PlayOneShot(_Settings.ElevatorButtonSound);

            _Elevator.FloorsToVisit.Add(Floor);

            IsActive = true;
        }

        [PublicAPI]
        public void TurnOff() 
        {
            if (_Elevator.CurrentFloor != Floor)
                return;

            _MyMaterial.color = Color.white;
            IsActive = false;
        }

        #endregion Public Methods


        #region Unity Methods

        private void Awake()
        {
            _Elevator.OnStop += TurnOff;
            _Elevator.SetButtons.Add(Floor, this);
            _MyMaterial = GetComponent<Renderer>().material;
            _MyAudio = GetComponent<AudioSource>();
        }

        private void Start()
        {
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

