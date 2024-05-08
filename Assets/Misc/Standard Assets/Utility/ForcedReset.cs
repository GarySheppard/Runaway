using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Image))]
public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}
