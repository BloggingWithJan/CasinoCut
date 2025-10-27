using System;
using System.Collections;
using UnityEngine;

namespace CasinoCut.Controller
{
    public class SlotMachineController : MonoBehaviour
    {
        public event Action onExit;
        CustomInputActions inputActions;        

        [SerializeField]
        GameObject[] slotMachineReels;

        [SerializeField]
        float spinDuration = 2f;

        [SerializeField]
        float[] reelSpeeds = { 360f, 420f, 480f }; // Different speeds for each reel (degrees per second)

        [SerializeField]
        float[] randomSpeedVariation = { 50f, 75f, 100f }; // Random speed variation for each reel

        [SerializeField]
        float[] possibleStopAngles =
        {
            0f,
            30f,
            60f,
            90f,
            120f,
            150f,
            180f,
            210f,
            240f,
            270f,
            300f,
            330f,
        }; // Possible stop positions

        private bool isSpinning = false;

        void Awake()
        {
            inputActions = new CustomInputActions();
            AssignInputActions();
        }

        void StartSpin()
        {
            if (isSpinning)
                return; // Prevent multiple spins at once

            // Logic to start the slot machine spin
            Debug.Log("Slot Machine Spin Started!");
            StartCoroutine(SpinReels());
        }

        IEnumerator SpinReels()
        {
            isSpinning = true;
            float elapsedTime = 0f;

            // Store initial rotations and calculate random target angles for each reel
            Vector3[] initialRotations = new Vector3[slotMachineReels.Length];
            float[] targetAngles = new float[slotMachineReels.Length];
            float[] currentSpeeds = new float[slotMachineReels.Length];

            for (int i = 0; i < slotMachineReels.Length; i++)
            {
                if (slotMachineReels[i] != null)
                {
                    initialRotations[i] = slotMachineReels[i].transform.eulerAngles;

                    // Pick a random target angle from possible stop angles
                    targetAngles[i] = possibleStopAngles[
                        UnityEngine.Random.Range(0, possibleStopAngles.Length)
                    ];

                    // Add random variation to the base speed for this spin
                    float baseSpeed = reelSpeeds[i % reelSpeeds.Length];
                    float speedVariation = randomSpeedVariation[i % randomSpeedVariation.Length];
                    currentSpeeds[i] =
                        baseSpeed + UnityEngine.Random.Range(-speedVariation, speedVariation);
                }
            }

            // Spin the reels for the specified duration
            while (elapsedTime < spinDuration)
            {
                float deltaTime = Time.deltaTime;
                elapsedTime += deltaTime;

                // Calculate deceleration factor (slow down towards the end)
                float decelerationFactor = 1f;
                if (elapsedTime > spinDuration * 0.7f) // Start slowing down at 70% of spin duration
                {
                    float remainingTime = spinDuration - elapsedTime;
                    decelerationFactor = remainingTime / (spinDuration * 0.3f);
                    decelerationFactor = Mathf.Clamp01(decelerationFactor);
                }

                // Rotate each reel at its designated speed with deceleration
                for (int i = 0; i < slotMachineReels.Length; i++)
                {
                    if (slotMachineReels[i] != null)
                    {
                        // Apply speed with deceleration
                        float adjustedSpeed = currentSpeeds[i] * decelerationFactor;

                        // Rotate around X-axis
                        slotMachineReels[i]
                            .transform.Rotate(adjustedSpeed * deltaTime, 0f, 0f, Space.Self);
                    }
                }

                yield return null; // Wait for next frame
            }

            // Snap to the predetermined random target angles
            for (int i = 0; i < slotMachineReels.Length; i++)
            {
                if (slotMachineReels[i] != null)
                {
                    Vector3 currentRotation = slotMachineReels[i].transform.eulerAngles;
                    slotMachineReels[i].transform.eulerAngles = new Vector3(
                        targetAngles[i],
                        currentRotation.y,
                        currentRotation.z
                    );
                }
            }

            isSpinning = false;
            Debug.Log("Slot Machine Spin Completed!");
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
