using CasinoCut.Controller;
using CasinoCut.Core;
using UnityEngine;

namespace CasinoCut.Interactable
{
    public class SlotMachineInteractable : BaseInteractable
    {
        [SerializeField]
        GameObject slotMachineHUD;

        [SerializeField]
        Cinemachine.CinemachineVirtualCamera slotMachineCamera;
        private bool isPlaying = false;

        private GameObject player;
        private SlotMachineController slotMachineController;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            slotMachineController = GetComponent<SlotMachineController>();
            slotMachineController.onExit += StopPlaying;
            slotMachineController.enabled = false;
            slotMachineHUD.SetActive(false);
        }

        public override void Interact(GameObject interactor)
        {
            if (!isPlaying)
                StartPlaying();
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
            slotMachineController.enabled = true;
            slotMachineHUD.SetActive(true);
            // Add logic to start the slot machine
        }

        private void StopPlaying()
        {
            isPlaying = false;
            slotMachineCamera.Priority = 10;
            slotMachineController.enabled = false;
            player.GetComponent<PlayerController>().enabled = true;
            slotMachineHUD.SetActive(false);
            // Add logic to stop the slot machine
        }
    }
}
