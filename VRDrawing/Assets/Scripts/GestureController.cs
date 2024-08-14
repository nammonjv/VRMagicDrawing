using UnityEngine;
using System.Collections.Generic;

public class GestureController : MonoBehaviour
{
    private DollarRecognizer recognizer;
    private List<Vector2> userGesture = new List<Vector2>();

    void Start()
    {
        recognizer = new DollarRecognizer();
        InitializePatterns();
    }

    private void InitializePatterns()
    {
        List<Vector2> circlePattern = new List<Vector2>
        {
            new Vector2(0, 0), new Vector2(1, 1), new Vector2(2, 0), new Vector2(1, -1), new Vector2(0, 0)
        };
        recognizer.SavePattern("Circle", circlePattern);

        // Add more patterns as needed
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            userGesture.Clear();
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            userGesture.Add(point);
            Debug.Log("Point added: " + point);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Gesture completed with " + userGesture.Count + " points");
            var result = recognizer.Recognize(userGesture);
            if (result.Match != null)
            {
                Debug.Log("Recognized Gesture: " + result.Match.Name + " with score: " + result.Score);
                HandleGestureRecognition(result);
            }
            else
            {
                Debug.Log("No gesture recognized.");
            }
        }
    }

    void HandleGestureRecognition(DollarRecognizer.Result result)
    {
        switch (result.Match.Name)
        {
            case "Circle":
                // Handle circle gesture
                Debug.Log("Circle gesture recognized.");
                break;
                // Add more cases for other gestures
        }
    }
}
