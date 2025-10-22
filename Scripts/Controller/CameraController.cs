using UnityEngine;
using UnityEngine.InputSystem;

namespace CasinoCut.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        Transform target;

        CustomInputActions inputActions;

        Vector3 initialOffset;

        void Awake()
        {
            inputActions = new CustomInputActions();
        }

        void Start()
        {
            initialOffset = transform.position - target.position;
        }

        void LateUpdate()
        {
            transform.position = target.position + initialOffset;
        }

        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }
    }
}
