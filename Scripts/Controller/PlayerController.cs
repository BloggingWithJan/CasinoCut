using System;
using CasinoCut.Interactable;
using CasinoCut.Movement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace CasinoCut.Controller
{
    public class PlayerController : MonoBehaviour
    {
        CustomInputActions inputActions;

        private bool isMoveHeld = false;

        [SerializeField]
        private LayerMask terrainLayerMask = -1; // Default to all layers, set in inspector

        [SerializeField]
        private LayerMask interactableLayerMask = -1; // Default to all layers, set in inspector

        [SerializeField]
        private float interactionDistance = 2f;

        private IInteractable currentInteractable;

        void Awake()
        {
            inputActions = new CustomInputActions();
            AssignInputActions();
        }

        void Update()
        {
            ClearHighlight();
            if (InteractWithInteractable())
            {
                return;
            }
            if (InteractWithMovement())
            {
                return;
            }
        }

        private bool InteractWithMovement()
        {
            var (hasHit, hit) = GetMouseRayHit(Mathf.Infinity, terrainLayerMask);

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

        private bool InteractWithInteractable()
        {
            var (hasHit, hit) = GetMouseRayHit(interactionDistance, interactableLayerMask);

            if (hasHit)
            {
                var interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    Highlight(interactable);
                    if (inputActions.Player.Interact.WasPressedThisFrame())
                    {
                        interactable.Interact(gameObject);
                    }
                    return true;
                }
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        }

        private (bool hasHit, RaycastHit hit) GetMouseRayHit(float maxDistance, LayerMask layerMask)
        {
            Ray ray = GetMouseRay();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit, maxDistance, layerMask);
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

        private void Highlight(IInteractable newInteractable)
        {
            // Store the current target
            currentInteractable = newInteractable;
            newInteractable.Highlight();

            // Display prompt (replace with UI logic in production)
            Debug.Log("Interaction Prompt: " + newInteractable.GetInteractionPrompt());

            // You could also add visual highlighting (e.g., change material color) here
        }

        private void ClearHighlight()
        {
            if (currentInteractable != null)
            {
                // Clear prompt/visuals if no longer looking at it
                currentInteractable.UnHighlight();
                currentInteractable = null;
            }
        }
    }
}
