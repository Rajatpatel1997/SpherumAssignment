using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovementHandler : MonoBehaviour
{
    #region PRIVATE_VARS
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool swapControls;
    #endregion

    #region UNITY_CALLBACKS
    private void OnEnable()
    {
        if (swapControls)
        {
            InputManager.OnArrowsInputChanged += MoveCube;
        }
        else
        {
            InputManager.OnWASDInputChanged += MoveCube;
        }
    }
    private void OnDisable()
    {
        if (swapControls)
        {
            InputManager.OnArrowsInputChanged -= MoveCube;
        }
        else
        {
            InputManager.OnWASDInputChanged -= MoveCube;
        }
    }
    #endregion

    #region PUBLIC_METHODS
    private void MoveCube(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    #endregion
}
