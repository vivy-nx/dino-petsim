using UnityEngine;

public class CanvasToggle : MonoBehaviour
{
    [Header("UI Canvas to show/hide")]
    public GameObject canvasUI;

    [Header("Gameplay scripts to disable during menu")]
    public MonoBehaviour[] scriptsToDisable;

    private bool isCanvasOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleCanvas();
        }
    }

    void ToggleCanvas()
    {
        isCanvasOpen = !isCanvasOpen;
        canvasUI.SetActive(isCanvasOpen);

        foreach (var script in scriptsToDisable)
        {
            if (script != null)
                script.enabled = !isCanvasOpen;
        }

        if (isCanvasOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
