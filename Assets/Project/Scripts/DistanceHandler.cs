using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DistanceHandler : MonoBehaviour
{
    #region PRIVATE_VARS
    [SerializeField] private Transform redCube;
    [SerializeField] private Transform greenCube;
    [SerializeField] private GameObject[] spheres;
    [SerializeField] private Text distanceText;
    [SerializeField] private float distanceToActiveSpheres = 2f;
    [SerializeField] private float distanceToChangeScene = 1f;

    private float distance;
    private bool sceneLoaded = false;
    #endregion

    #region UNITY_CALLBACKS
    void Update()
    {
        distance = Vector3.Distance(redCube.position, greenCube.position);
        distanceText.text = $"Distance: {distance:F2}m";

        if(distance < distanceToActiveSpheres && !spheres[0].activeSelf)
        {
            ToggleSpheresVisiblity(true);
        }
        else if(distance >= distanceToActiveSpheres && spheres[0].activeSelf)
        {
            ToggleSpheresVisiblity(false);
        }

        if (distance < distanceToChangeScene && !sceneLoaded)
        {
            sceneLoaded = true;
            SceneManager.LoadScene(1);
        }
    }
    #endregion

    #region PRIVATE_METHODS
    private void ToggleSpheresVisiblity(bool active)
    {
        foreach(var sphere in spheres)
        {
            sphere.SetActive(active);
        }
    }
    #endregion
}
