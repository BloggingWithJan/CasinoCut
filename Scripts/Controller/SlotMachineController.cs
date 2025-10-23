using System;
using UnityEngine;

namespace CasinoCut.Controller
{
    public class SlotMachineController : MonoBehaviour
    {
        public event Action onExit;
        CustomInputActions inputActions;

        void Awake()
        {
            inputActions = new CustomInputActions();
            AssignInputActions();
        }

        void StartSpin()
        {
            // Logic to start the slot machine spin
            Debug.Log("Slot Machine Spin Started!");
        }

        void Exit()
        {
            onExit?.Invoke();
        }

        void AssignInputActions()
        {
            inputActions.SlotMachine.Spin.performed += ctx =>
            {
                StartSpin();
            };
            inputActions.SlotMachine.Exit.performed += ctx =>
            {
                Exit();
            };
        }

        void OnEnable()
        {
            inputActions.Enable();
        }

        void OnDisable()
        {
            inputActions.Disable();
        }
    }
}
