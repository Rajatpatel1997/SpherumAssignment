using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region EVENTS
    public static event Action<Vector3> OnWASDInputChanged;
    public static event Action<Vector3> OnArrowsInputChanged;
    #endregion

    #region PRIVATE_VARS
    private bool isInputActive;
    public bool IsInputActive { get => isInputActive; set => isInputActive = value; }

    private string horizontalAxis;
    private string verticalAxis;
    private string horizontalArrowAxis;
    private string verticalArrowAxis;
    #endregion

    #region UNITY_CALLBACKS
    private void Start()
    {
        isInputActive = true;
        horizontalAxis = "Horizontal";
        verticalAxis = "Vertical";
        horizontalArrowAxis = "HorizontalArrow";
        verticalArrowAxis = "VerticalArrow";
    }
    void Update()
    {
        if (!isInputActive)
            return;

        Vector3 wasdInput = new Vector3(Input.GetAxis(horizontalAxis), 0, Input.GetAxis(verticalAxis));
        OnWASDInputChanged?.Invoke(wasdInput.normalized);

        Vector3 arrowsInput = new Vector3(Input.GetAxis(horizontalArrowAxis), 0, Input.GetAxis(verticalArrowAxis));
        OnArrowsInputChanged?.Invoke(arrowsInput.normalized);
    }
    #endregion
}
