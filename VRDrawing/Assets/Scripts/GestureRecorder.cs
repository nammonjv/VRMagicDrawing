using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GestureRecorder : MonoBehaviour
{
    private List<Vector2> recordedPoints = new List<Vector2>();
    private bool recording = false;

    public InputActionReference RecordAction;

    private void OnEnable()
    {
        RecordAction.action.performed += OnRecordClick;
    }
    private void OnDisable()
    {
        RecordAction.action.performed -= OnRecordClick;
    }
    void Update()
    {

        // Record gesture movement while recording is active
        if (recording)
        {
            RecordGesture();
        }
        
    }

    void OnRecordClick(InputAction.CallbackContext context)
    {
        if (!recording)
        {
            StartRecording();
        }
        else
        {
            StopRecording();
        }
    }

    void StartRecording()
    {
        Debug.Log("Recording started...");
        recordedPoints.Clear(); // Clear any existing recorded points
        recording = true;
    }

    void StopRecording()
    {
        Debug.Log("Recording stopped.");
        recording = false;

        // Print the recorded points (for debugging purposes)
        PrintRecordedPoints();
    }

    void RecordGesture()
    {
        // Record current mouse position as Vector2 point
        Vector2 point = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // Adjust position as needed (e.g., convert to local space if necessary)
        // Example: point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Add the point to the recorded list
        recordedPoints.Add(point);
    }

    void PrintRecordedPoints()
    {
        // Output the recorded points to the console (for debugging)
        Debug.Log("Recorded Points:");
        foreach (var point in recordedPoints)
        {
            Debug.Log(point);
        }
    }
}
