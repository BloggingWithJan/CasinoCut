using System;
using CasinoCut.Movement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace CasinoCut.Control
{
    public class PlayerController : MonoBehaviour
    {
        CustomInputActions inputActions;

        private bool isMoveHeld = false;

        [SerializeField]
        private LayerMask terrainLayerMask = -1; // Default to all layers, set in inspector

        void Awake()
        {
            inputActions = new CustomInputActions();
            AssignInputActions();
        }

        void Update()
        {
            if (InteractWithMovement())
            {
                return;
            }
        }

        private bool InteractWithMovement()
        {
            var (hasHit, hit) = GetMouseRayHit();

            if (hasHit)
            {
                if (isMoveHeld)
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        }

        private (bool hasHit, RaycastHit hit) GetMouseRayHit()
        {
            Ray ray = GetMouseRay();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit, Mathf.Infinity, terrainLayerMask);
            return (hasHit, hit);
        }

        void OnEnable()
        {
            inputActions.Enable();
        }

        void OnDisable()
        {
            inputActions.Disable();
        }

        void AssignInputActions()
        {
            inputActions.Player.Move.started += ctx =>
            {
                isMoveHeld = true;
            };
            inputActions.Player.Move.performed += ctx =>
            {
                isMoveHeld = true;
            };
            inputActions.Player.Move.canceled += ctx =>
            {
                isMoveHeld = false;
            };
        }
    }
}
