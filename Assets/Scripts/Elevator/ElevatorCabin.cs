using System;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Elevator {

    /// <summary>
    /// This script is responsible for moving the elevator cabin and playing moving sounds.
    ///
    /// "PlayMovingSound" and "PlayStoppingSound" are separate methods due to being assigned to an event action (cannot pass parameters when subscribing to a method)
    /// </summary>

    [Serializable]
    public sealed class ElevatorCabin : MonoBehaviour {

        #region Public Types

        [Serializable]
        public class Settings
        {
            public AudioClip CabinMovingSound;
            public AudioClip CabinStopSound;
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
        public void MoveCabin(float speed)
        {
            switch (_Elevator.CurrentState) {

                case Elevator.State.GoingUp:
                    speed *= 1f;
                    break;

                case Elevator.State.GoingDown:
                    speed *= -1f;
                    break;

                case Elevator.State.Stop:
                    speed *= 0f;
                    return;

                case Elevator.State.Idle:
                    speed *= 0f;
                    return;

                default:
                    throw new InvalidEnumArgumentException();
            }

            _Body.MovePosition(transform.position + transform.up * Time.deltaTime * speed);
        }

        [PublicAPI]
        public void PlayMovingSound()
        {
            _Audio.clip = _Settings.CabinMovingSound;
            _Audio.loop = true;
            _Audio.Play();
        }

        [PublicAPI]
        public void PlayStoppingSound()
        {
            _Audio.clip = _Settings.CabinStopSound;
            _Audio.loop = false;
            _Audio.Play();
        }

        #endregion Public Methods


        #region Unity Methods

        private void Awake()
        {
            _Elevator.OnMove += PlayMovingSound;
            _Elevator.OnStop += PlayStoppingSound;
            _Body = GetComponent<Rigidbody>();
            _Audio = GetComponent<AudioSource>();
        }

        private void FixedUpdate()
        {
            MoveCabin(_Elevator.ElevatorSpeed);
        }

        #endregion Unity Methods


        #region Private Variables

        private Settings _Settings;
        private Elevator _Elevator;
        private Rigidbody _Body;
        private AudioSource _Audio;

        #endregion Private Variables
    }
}

