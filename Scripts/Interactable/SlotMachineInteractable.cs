using CasinoCut.Controller;
using CasinoCut.Core;
using UnityEngine;

namespace CasinoCut.Interactable
{
    public class SlotMachineInteractable : BaseInteractable
    {
        [SerializeField]
        Cinemachine.CinemachineVirtualCamera slotMachineCamera;
        private bool isPlaying = false;

        private GameObject player;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }

        public override void Interact(GameObject interactor)
        {
            if (isPlaying)
            {
                StopPlaying();
            }
            else
            {
                StartPlaying();
            }
        }

        public override string GetInteractionPrompt()
        {
            return isPlaying ? "Stop [E]" : "Play [E]";
        }

        // --- Specific Slot Machine Logic ---
        private void StartPlaying()
        {
            isPlaying = true;
            slotMachineCamera.Priority = 999;
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
            UnHighlight();
            // Add logic to start the slot machine
        }

        private void StopPlaying()
        {
            isPlaying = false;
            slotMachineCamera.Priority = 10;
            // Add logic to stop the slot machine
        }
    }
}
