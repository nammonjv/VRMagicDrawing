using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using UnityEngine.UI;

public class VRGestureController : MonoBehaviour
{
    public InputActionReference selectAction; // Reference to the Select action
    public InputActionReference positionAction; // Reference to the Position action
    private DollarRecognizer recognizer;
    private List<Vector2> userGesture = new List<Vector2>();
    private bool isTracking = false;

    public Transform wandTip;

    public Text text;



    private List<Vector2> recordedPoints = new List<Vector2>();
    private bool recording = false;

    public InputActionReference RecordAction;

    private void OnEnable()
    {
        selectAction.action.performed += OnSelectPerformed;
        selectAction.action.canceled += OnSelectCanceled;
        positionAction.action.performed += OnPositionPerformed;

        RecordAction.action.performed += OnRecordClick;
    }

    private void OnDisable()
    {
        selectAction.action.performed -= OnSelectPerformed;
        selectAction.action.canceled -= OnSelectCanceled;
        positionAction.action.performed -= OnPositionPerformed;

        RecordAction.action.performed -= OnRecordClick;
    }

    void Start()
    {
        recognizer = new DollarRecognizer();
        InitializePatterns();
    }

    private void InitializePatterns()
    {
        List<Vector2> circlePattern = new List<Vector2>
        {
            new Vector2(-0.11f, 0.97f),
            new Vector2(-0.10f, 0.99f),
            new Vector2(-0.09f, 1.00f),
            new Vector2(-0.09f, 1.01f),
            new Vector2(-0.09f, 1.01f),
            new Vector2(-0.08f, 1.01f),
            new Vector2(-0.08f, 1.01f),
            new Vector2(-0.09f, 1.01f),
            new Vector2(-0.09f, 1.00f),
            new Vector2(-0.09f, 1.00f),
            new Vector2(-0.09f, 1.00f),
            new Vector2(-0.09f, 1.00f),
            new Vector2(-0.10f, 0.99f),
            new Vector2(-0.10f, 0.98f),
            new Vector2(-0.11f, 0.97f),
            new Vector2(-0.12f, 0.96f),
            new Vector2(-0.12f, 0.95f),
            new Vector2(-0.13f, 0.94f),
            new Vector2(-0.13f, 0.94f),
            new Vector2(-0.13f, 0.93f),
            new Vector2(-0.14f, 0.93f),
            new Vector2(-0.14f, 0.92f),
            new Vector2(-0.15f, 0.91f),
            new Vector2(-0.15f, 0.91f),
            new Vector2(-0.15f, 0.90f),
            new Vector2(-0.15f, 0.90f),
            new Vector2(-0.16f, 0.89f),
            new Vector2(-0.16f, 0.88f),
            new Vector2(-0.17f, 0.87f),
            new Vector2(-0.18f, 0.86f),
            new Vector2(-0.18f, 0.85f),
            new Vector2(-0.19f, 0.85f),
            new Vector2(-0.19f, 0.85f),
            new Vector2(-0.18f, 0.85f),
            new Vector2(-0.18f, 0.86f),
            new Vector2(-0.17f, 0.87f),
            new Vector2(-0.17f, 0.88f),
            new Vector2(-0.16f, 0.89f),
            new Vector2(-0.16f, 0.91f),
            new Vector2(-0.15f, 0.92f),
            new Vector2(-0.14f, 0.93f),
            new Vector2(-0.14f, 0.94f),
            new Vector2(-0.13f, 0.95f),
            new Vector2(-0.13f, 0.95f),
            new Vector2(-0.13f, 0.96f),
            new Vector2(-0.13f, 0.96f),
            new Vector2(-0.13f, 0.96f),
            new Vector2(-0.13f, 0.96f),
            new Vector2(-0.13f, 0.96f),
            new Vector2(-0.13f, 0.95f),
            new Vector2(-0.13f, 0.95f),
            new Vector2(-0.13f, 0.95f),
            new Vector2(-0.13f, 0.95f),
            new Vector2(-0.13f, 0.95f),
            new Vector2(-0.13f, 0.95f),
            new Vector2(-0.13f, 0.96f),
            new Vector2(-0.12f, 0.97f),
            new Vector2(-0.11f, 0.98f),
            new Vector2(-0.11f, 0.98f),
            new Vector2(-0.11f, 0.99f),
            new Vector2(-0.10f, 0.99f),
            new Vector2(-0.10f, 1.00f),
            new Vector2(-0.10f, 1.00f),
            new Vector2(-0.10f, 1.00f),
            new Vector2(-0.09f, 1.01f),
            new Vector2(-0.10f, 1.01f)

        };
        recognizer.SavePattern("Circle", circlePattern);

        List<Vector2> circlePattern2 = new List<Vector2>
        {
            new Vector2(-0.80f, 0.72f),
            new Vector2(-0.80f, 0.72f),
            new Vector2(-0.80f, 0.72f),
            new Vector2(-0.80f, 0.72f),
            new Vector2(-0.80f, 0.72f),
            new Vector2(-0.80f, 0.73f),
            new Vector2(-0.80f, 0.73f),
            new Vector2(-0.80f, 0.72f),
            new Vector2(-0.80f, 0.72f),
            new Vector2(-0.80f, 0.72f),
            new Vector2(-0.80f, 0.72f),
            new Vector2(-0.80f, 0.71f),
            new Vector2(-0.80f, 0.71f),
            new Vector2(-0.81f, 0.71f),
            new Vector2(-0.81f, 0.71f),
            new Vector2(-0.81f, 0.71f),
            new Vector2(-0.81f, 0.71f),
            new Vector2(-0.81f, 0.70f),
            new Vector2(-0.81f, 0.70f),
            new Vector2(-0.82f, 0.70f),
            new Vector2(-0.82f, 0.69f),
            new Vector2(-0.82f, 0.69f),
            new Vector2(-0.82f, 0.69f),
            new Vector2(-0.82f, 0.69f),
            new Vector2(-0.82f, 0.69f),
            new Vector2(-0.81f, 0.70f),
            new Vector2(-0.81f, 0.69f),
            new Vector2(-0.81f, 0.69f),
            new Vector2(-0.81f, 0.69f),
            new Vector2(-0.80f, 0.69f),
            new Vector2(-0.81f, 0.68f),
            new Vector2(-0.81f, 0.68f),
            new Vector2(-0.81f, 0.67f),
            new Vector2(-0.81f, 0.67f),
            new Vector2(-0.81f, 0.66f),
            new Vector2(-0.81f, 0.66f),
            new Vector2(-0.81f, 0.65f),
            new Vector2(-0.81f, 0.65f),
            new Vector2(-0.82f, 0.65f),
            new Vector2(-0.82f, 0.64f),
            new Vector2(-0.82f, 0.64f),
            new Vector2(-0.81f, 0.64f),
            new Vector2(-0.81f, 0.64f),
            new Vector2(-0.80f, 0.64f),
            new Vector2(-0.80f, 0.65f),
            new Vector2(-0.79f, 0.65f),
            new Vector2(-0.79f, 0.65f),
            new Vector2(-0.78f, 0.66f),
            new Vector2(-0.77f, 0.66f),
            new Vector2(-0.77f, 0.67f),
            new Vector2(-0.76f, 0.67f),
            new Vector2(-0.76f, 0.67f),
            new Vector2(-0.76f, 0.67f),
            new Vector2(-0.76f, 0.68f),
            new Vector2(-0.76f, 0.68f),
            new Vector2(-0.75f, 0.68f),
            new Vector2(-0.75f, 0.69f),
            new Vector2(-0.74f, 0.69f),
            new Vector2(-0.74f, 0.70f),
            new Vector2(-0.73f, 0.70f),
            new Vector2(-0.73f, 0.70f),
            new Vector2(-0.73f, 0.71f),
            new Vector2(-0.73f, 0.71f),
            new Vector2(-0.72f, 0.72f),
            new Vector2(-0.71f, 0.73f),
            new Vector2(-0.71f, 0.73f),
            new Vector2(-0.71f, 0.73f),
            new Vector2(-0.72f, 0.73f),
            new Vector2(-0.72f, 0.72f),
            new Vector2(-0.73f, 0.72f),
            new Vector2(-0.73f, 0.73f),
            new Vector2(-0.72f, 0.73f),
            new Vector2(-0.73f, 0.73f),
            new Vector2(-0.74f, 0.72f),
            new Vector2(-0.75f, 0.72f),
            new Vector2(-0.76f, 0.71f),
            new Vector2(-0.76f, 0.72f),
            new Vector2(-0.75f, 0.72f),
            new Vector2(-0.74f, 0.73f),
            new Vector2(-0.74f, 0.73f),
            new Vector2(-0.75f, 0.72f),
            new Vector2(-0.76f, 0.72f),
            new Vector2(-0.76f, 0.72f),
            new Vector2(-0.76f, 0.72f),
            new Vector2(-0.76f, 0.72f),
            new Vector2(-0.76f, 0.72f),
            new Vector2(-0.76f, 0.72f),
            new Vector2(-0.76f, 0.73f),
            new Vector2(-0.76f, 0.72f),
            new Vector2(-0.76f, 0.72f),
            new Vector2(-0.76f, 0.72f),
            new Vector2(-0.76f, 0.72f),
            new Vector2(-0.76f, 0.72f),
            new Vector2(-0.76f, 0.72f),
            new Vector2(-0.76f, 0.72f),
            new Vector2(-0.76f, 0.73f)

        };
        recognizer.SavePattern("Circle", circlePattern2);

        List<Vector2> circlePattern3 = new List<Vector2>
        {
            new Vector2(-0.21f, 0.79f),
            new Vector2(-0.20f, 0.80f),
            new Vector2(-0.20f, 0.81f),
            new Vector2(-0.19f, 0.82f),
            new Vector2(-0.18f, 0.82f),
            new Vector2(-0.18f, 0.83f),
            new Vector2(-0.18f, 0.83f),
            new Vector2(-0.17f, 0.84f),
            new Vector2(-0.17f, 0.84f),
            new Vector2(-0.17f, 0.85f),
            new Vector2(-0.16f, 0.86f),
            new Vector2(-0.15f, 0.86f),
            new Vector2(-0.15f, 0.87f),
            new Vector2(-0.14f, 0.88f),
            new Vector2(-0.14f, 0.88f),
            new Vector2(-0.14f, 0.88f),
            new Vector2(-0.13f, 0.89f),
            new Vector2(-0.13f, 0.89f),
            new Vector2(-0.13f, 0.90f),
            new Vector2(-0.12f, 0.91f),
            new Vector2(-0.12f, 0.91f),
            new Vector2(-0.11f, 0.91f),
            new Vector2(-0.11f, 0.91f),
            new Vector2(-0.11f, 0.91f),
            new Vector2(-0.11f, 0.91f),
            new Vector2(-0.12f, 0.91f),
            new Vector2(-0.12f, 0.91f),
            new Vector2(-0.12f, 0.91f),
            new Vector2(-0.12f, 0.90f),
            new Vector2(-0.12f, 0.90f),
            new Vector2(-0.13f, 0.89f),
            new Vector2(-0.13f, 0.88f),
            new Vector2(-0.14f, 0.88f),
            new Vector2(-0.14f, 0.87f),
            new Vector2(-0.14f, 0.86f),
            new Vector2(-0.15f, 0.86f),
            new Vector2(-0.15f, 0.84f),
            new Vector2(-0.16f, 0.83f),
            new Vector2(-0.17f, 0.82f),
            new Vector2(-0.17f, 0.81f),
            new Vector2(-0.18f, 0.80f),
            new Vector2(-0.18f, 0.80f),
            new Vector2(-0.18f, 0.80f),
            new Vector2(-0.18f, 0.79f),
            new Vector2(-0.19f, 0.79f),
            new Vector2(-0.19f, 0.78f),
            new Vector2(-0.20f, 0.78f),
            new Vector2(-0.20f, 0.78f),
            new Vector2(-0.20f, 0.78f),
            new Vector2(-0.20f, 0.78f),
            new Vector2(-0.20f, 0.78f),
            new Vector2(-0.20f, 0.78f),
            new Vector2(-0.20f, 0.78f),
            new Vector2(-0.20f, 0.78f),
            new Vector2(-0.20f, 0.78f),
            new Vector2(-0.20f, 0.78f),
            new Vector2(-0.20f, 0.79f),
            new Vector2(-0.20f, 0.79f),
            new Vector2(-0.20f, 0.79f),
            new Vector2(-0.20f, 0.80f),
            new Vector2(-0.20f, 0.80f),
            new Vector2(-0.20f, 0.81f),
            new Vector2(-0.20f, 0.82f),
            new Vector2(-0.20f, 0.83f),
            new Vector2(-0.20f, 0.84f),
            new Vector2(-0.20f, 0.85f),
            new Vector2(-0.20f, 0.86f),
            new Vector2(-0.20f, 0.87f),
            new Vector2(-0.20f, 0.88f),
            new Vector2(-0.19f, 0.88f)

        };
        recognizer.SavePattern("Circle", circlePattern3);

        List<Vector2> circlePattern4 = new List<Vector2>
        {
            new Vector2(-0.15f, 0.84f),
            new Vector2(-0.14f, 0.85f),
            new Vector2(-0.13f, 0.87f),
            new Vector2(-0.12f, 0.88f),
            new Vector2(-0.11f, 0.89f),
            new Vector2(-0.11f, 0.90f),
            new Vector2(-0.10f, 0.91f),
            new Vector2(-0.09f, 0.92f),
            new Vector2(-0.09f, 0.92f),
            new Vector2(-0.09f, 0.92f),
            new Vector2(-0.09f, 0.92f),
            new Vector2(-0.09f, 0.91f),
            new Vector2(-0.09f, 0.91f),
            new Vector2(-0.09f, 0.92f),
            new Vector2(-0.09f, 0.92f),
            new Vector2(-0.08f, 0.92f),
            new Vector2(-0.09f, 0.92f),
            new Vector2(-0.09f, 0.91f),
            new Vector2(-0.09f, 0.91f),
            new Vector2(-0.10f, 0.90f),
            new Vector2(-0.11f, 0.89f),
            new Vector2(-0.12f, 0.87f),
            new Vector2(-0.13f, 0.85f),
            new Vector2(-0.14f, 0.84f),
            new Vector2(-0.14f, 0.83f),
            new Vector2(-0.15f, 0.82f),
            new Vector2(-0.15f, 0.82f),
            new Vector2(-0.15f, 0.82f),
            new Vector2(-0.15f, 0.82f),
            new Vector2(-0.15f, 0.83f),
            new Vector2(-0.14f, 0.83f),
            new Vector2(-0.14f, 0.84f),
            new Vector2(-0.13f, 0.86f),
            new Vector2(-0.12f, 0.87f),
            new Vector2(-0.12f, 0.88f),
            new Vector2(-0.12f, 0.89f),
            new Vector2(-0.11f, 0.89f),
            new Vector2(-0.11f, 0.90f),
            new Vector2(-0.11f, 0.90f),
            new Vector2(-0.11f, 0.89f),
            new Vector2(-0.12f, 0.89f),
            new Vector2(-0.12f, 0.89f),
            new Vector2(-0.12f, 0.90f),
            new Vector2(-0.11f, 0.91f),
            new Vector2(-0.10f, 0.92f),
            new Vector2(-0.11f, 0.93f),
            new Vector2(-0.11f, 0.93f),
            new Vector2(-0.11f, 0.93f),
            new Vector2(-0.11f, 0.93f),
            new Vector2(-0.11f, 0.93f)

        };
        recognizer.SavePattern("Circle", circlePattern4);

        List<Vector2> circlePattern5 = new List<Vector2>
        {
            new Vector2(-0.09f, 0.95f),
            new Vector2(-0.09f, 0.95f),
            new Vector2(-0.09f, 0.95f),
            new Vector2(-0.08f, 0.95f),
            new Vector2(-0.08f, 0.96f),
            new Vector2(-0.07f, 0.97f),
            new Vector2(-0.06f, 0.99f),
            new Vector2(-0.06f, 0.99f),
            new Vector2(-0.06f, 0.99f),
            new Vector2(-0.06f, 0.99f),
            new Vector2(-0.07f, 0.98f),
            new Vector2(-0.07f, 0.97f),
            new Vector2(-0.07f, 0.97f),
            new Vector2(-0.07f, 0.96f),
            new Vector2(-0.07f, 0.96f),
            new Vector2(-0.08f, 0.95f),
            new Vector2(-0.08f, 0.95f),
            new Vector2(-0.09f, 0.94f),
            new Vector2(-0.09f, 0.93f),
            new Vector2(-0.10f, 0.92f),
            new Vector2(-0.10f, 0.91f),
            new Vector2(-0.11f, 0.90f),
            new Vector2(-0.12f, 0.89f),
            new Vector2(-0.12f, 0.88f),
            new Vector2(-0.13f, 0.87f),
            new Vector2(-0.13f, 0.86f),
            new Vector2(-0.13f, 0.85f),
            new Vector2(-0.13f, 0.85f),
            new Vector2(-0.14f, 0.84f),
            new Vector2(-0.13f, 0.84f),
            new Vector2(-0.13f, 0.85f),
            new Vector2(-0.13f, 0.86f),
            new Vector2(-0.12f, 0.88f),
            new Vector2(-0.11f, 0.89f),
            new Vector2(-0.10f, 0.91f),
            new Vector2(-0.10f, 0.92f),
            new Vector2(-0.09f, 0.93f),
            new Vector2(-0.09f, 0.94f),
            new Vector2(-0.09f, 0.94f),
            new Vector2(-0.09f, 0.94f),
            new Vector2(-0.09f, 0.93f),
            new Vector2(-0.09f, 0.94f),
            new Vector2(-0.09f, 0.94f),
            new Vector2(-0.09f, 0.95f),
            new Vector2(-0.08f, 0.96f),
            new Vector2(-0.08f, 0.97f),
            new Vector2(-0.08f, 0.96f),
            new Vector2(-0.08f, 0.97f),
            new Vector2(-0.08f, 0.96f),
            new Vector2(-0.09f, 0.96f),
            new Vector2(-0.09f, 0.95f),
            new Vector2(-0.09f, 0.95f),
            new Vector2(-0.09f, 0.95f)

        };
        recognizer.SavePattern("Circle", circlePattern5);

        List<Vector2> circlePattern6 = new List<Vector2>
        {
            new Vector2(-0.08f, 0.97f),
            new Vector2(-0.08f, 0.97f),
            new Vector2(-0.07f, 0.98f),
            new Vector2(-0.07f, 0.99f),
            new Vector2(-0.06f, 1.00f),
            new Vector2(-0.06f, 1.00f),
            new Vector2(-0.05f, 1.01f),
            new Vector2(-0.05f, 1.01f),
            new Vector2(-0.05f, 1.00f),
            new Vector2(-0.06f, 0.99f),
            new Vector2(-0.06f, 0.99f),
            new Vector2(-0.06f, 0.99f),
            new Vector2(-0.06f, 0.98f),
            new Vector2(-0.07f, 0.97f),
            new Vector2(-0.07f, 0.96f),
            new Vector2(-0.08f, 0.95f),
            new Vector2(-0.08f, 0.94f),
            new Vector2(-0.09f, 0.94f),
            new Vector2(-0.09f, 0.93f),
            new Vector2(-0.10f, 0.93f),
            new Vector2(-0.10f, 0.93f),
            new Vector2(-0.10f, 0.92f),
            new Vector2(-0.11f, 0.91f),
            new Vector2(-0.12f, 0.89f),
            new Vector2(-0.12f, 0.88f),
            new Vector2(-0.13f, 0.87f),
            new Vector2(-0.13f, 0.87f),
            new Vector2(-0.12f, 0.87f),
            new Vector2(-0.12f, 0.88f),
            new Vector2(-0.11f, 0.90f),
            new Vector2(-0.10f, 0.92f),
            new Vector2(-0.09f, 0.94f),
            new Vector2(-0.08f, 0.95f),
            new Vector2(-0.08f, 0.96f),
            new Vector2(-0.07f, 0.97f),
            new Vector2(-0.07f, 0.97f),
            new Vector2(-0.07f, 0.97f),
            new Vector2(-0.07f, 0.98f),
            new Vector2(-0.07f, 0.98f),
            new Vector2(-0.07f, 0.98f),
            new Vector2(-0.07f, 0.98f),
            new Vector2(-0.07f, 0.98f),
            new Vector2(-0.08f, 0.98f),
            new Vector2(-0.08f, 0.97f),
            new Vector2(-0.08f, 0.97f),
            new Vector2(-0.08f, 0.97f),
            new Vector2(-0.08f, 0.98f),
            new Vector2(-0.08f, 0.98f),
            new Vector2(-0.08f, 0.98f),
            new Vector2(-0.08f, 0.98f),
            new Vector2(-0.08f, 0.98f)

        };
        recognizer.SavePattern("Circle", circlePattern6);

        List<Vector2> circlePattern7 = new List<Vector2>
        {
            new Vector2(-0.06f, 1.00f),
            new Vector2(-0.05f, 1.01f),
            new Vector2(-0.05f, 1.01f),
            new Vector2(-0.04f, 1.02f),
            new Vector2(-0.04f, 1.02f),
            new Vector2(-0.04f, 1.02f),
            new Vector2(-0.04f, 1.02f),
            new Vector2(-0.05f, 1.00f),
            new Vector2(-0.06f, 0.99f),
            new Vector2(-0.06f, 0.98f),
            new Vector2(-0.07f, 0.97f),
            new Vector2(-0.07f, 0.96f),
            new Vector2(-0.08f, 0.96f),
            new Vector2(-0.08f, 0.95f),
            new Vector2(-0.09f, 0.94f),
            new Vector2(-0.09f, 0.93f),
            new Vector2(-0.09f, 0.93f),
            new Vector2(-0.10f, 0.93f),
            new Vector2(-0.10f, 0.93f),
            new Vector2(-0.10f, 0.93f),
            new Vector2(-0.10f, 0.92f),
            new Vector2(-0.11f, 0.91f),
            new Vector2(-0.11f, 0.89f),
            new Vector2(-0.12f, 0.88f),
            new Vector2(-0.12f, 0.87f),
            new Vector2(-0.12f, 0.87f),
            new Vector2(-0.11f, 0.88f),
            new Vector2(-0.10f, 0.89f),
            new Vector2(-0.10f, 0.91f),
            new Vector2(-0.08f, 0.93f),
            new Vector2(-0.08f, 0.95f),
            new Vector2(-0.07f, 0.97f),
            new Vector2(-0.06f, 0.98f),
            new Vector2(-0.06f, 0.99f),
            new Vector2(-0.06f, 0.99f),
            new Vector2(-0.06f, 1.00f),
            new Vector2(-0.05f, 1.00f),
            new Vector2(-0.05f, 1.00f),
            new Vector2(-0.06f, 1.00f),
            new Vector2(-0.06f, 1.00f),
            new Vector2(-0.06f, 0.99f),
            new Vector2(-0.07f, 0.98f),
            new Vector2(-0.08f, 0.97f),
            new Vector2(-0.08f, 0.97f),
            new Vector2(-0.08f, 0.97f),
            new Vector2(-0.08f, 0.97f),
            new Vector2(-0.08f, 0.97f),
            new Vector2(-0.08f, 0.96f),
            new Vector2(-0.08f, 0.96f),
            new Vector2(-0.09f, 0.96f),
            new Vector2(-0.09f, 0.96f)

        };
        recognizer.SavePattern("Circle", circlePattern7);

        List<Vector2> circlePattern8 = new List<Vector2>
        {
            new Vector2(-0.06f, 0.99f),
            new Vector2(-0.06f, 0.99f),
            new Vector2(-0.06f, 0.99f),
            new Vector2(-0.06f, 0.99f),
            new Vector2(-0.06f, 0.98f),
            new Vector2(-0.06f, 0.96f),
            new Vector2(-0.07f, 0.95f),
            new Vector2(-0.07f, 0.94f),
            new Vector2(-0.07f, 0.94f),
            new Vector2(-0.07f, 0.93f),
            new Vector2(-0.07f, 0.92f),
            new Vector2(-0.07f, 0.91f),
            new Vector2(-0.07f, 0.89f),
            new Vector2(-0.08f, 0.88f),
            new Vector2(-0.08f, 0.86f),
            new Vector2(-0.09f, 0.84f),
            new Vector2(-0.09f, 0.83f),
            new Vector2(-0.09f, 0.82f),
            new Vector2(-0.09f, 0.81f),
            new Vector2(-0.09f, 0.81f),
            new Vector2(-0.09f, 0.80f),
            new Vector2(-0.09f, 0.79f),
            new Vector2(-0.10f, 0.77f),
            new Vector2(-0.11f, 0.74f),
            new Vector2(-0.12f, 0.72f),
            new Vector2(-0.13f, 0.70f),
            new Vector2(-0.13f, 0.69f),
            new Vector2(-0.13f, 0.69f),
            new Vector2(-0.13f, 0.70f),
            new Vector2(-0.12f, 0.72f),
            new Vector2(-0.11f, 0.75f),
            new Vector2(-0.10f, 0.77f),
            new Vector2(-0.09f, 0.79f),
            new Vector2(-0.09f, 0.81f),
            new Vector2(-0.09f, 0.82f),
            new Vector2(-0.09f, 0.82f),
            new Vector2(-0.09f, 0.82f),
            new Vector2(-0.08f, 0.83f),
            new Vector2(-0.08f, 0.84f),
            new Vector2(-0.08f, 0.85f),
            new Vector2(-0.07f, 0.86f),
            new Vector2(-0.07f, 0.87f),
            new Vector2(-0.07f, 0.88f),
            new Vector2(-0.07f, 0.88f),
            new Vector2(-0.07f, 0.89f),
            new Vector2(-0.06f, 0.90f),
            new Vector2(-0.05f, 0.93f),
            new Vector2(-0.05f, 0.94f),
            new Vector2(-0.05f, 0.94f),
            new Vector2(-0.05f, 0.93f),
            new Vector2(-0.06f, 0.93f),
            new Vector2(-0.06f, 0.93f),
            new Vector2(-0.06f, 0.93f),
            new Vector2(-0.06f, 0.93f),
            new Vector2(-0.06f, 0.92f),
            new Vector2(-0.06f, 0.92f),
            new Vector2(-0.07f, 0.92f)

        };
        recognizer.SavePattern("Circle", circlePattern8);

        List<Vector2> circlePattern9 = new List<Vector2>
        {
            new Vector2(-0.16f, 0.82f),
            new Vector2(-0.16f, 0.83f),
            new Vector2(-0.15f, 0.84f),
            new Vector2(-0.14f, 0.84f),
            new Vector2(-0.14f, 0.85f),
            new Vector2(-0.13f, 0.86f),
            new Vector2(-0.12f, 0.86f),
            new Vector2(-0.11f, 0.87f),
            new Vector2(-0.11f, 0.89f),
            new Vector2(-0.10f, 0.90f),
            new Vector2(-0.09f, 0.91f),
            new Vector2(-0.08f, 0.92f),
            new Vector2(-0.08f, 0.92f),
            new Vector2(-0.08f, 0.92f),
            new Vector2(-0.08f, 0.92f),
            new Vector2(-0.09f, 0.92f),
            new Vector2(-0.09f, 0.91f),
            new Vector2(-0.09f, 0.91f),
            new Vector2(-0.09f, 0.91f),
            new Vector2(-0.09f, 0.91f),
            new Vector2(-0.09f, 0.91f),
            new Vector2(-0.09f, 0.91f),
            new Vector2(-0.09f, 0.91f),
            new Vector2(-0.10f, 0.90f),
            new Vector2(-0.10f, 0.89f),
            new Vector2(-0.11f, 0.87f),
            new Vector2(-0.12f, 0.86f),
            new Vector2(-0.12f, 0.84f),
            new Vector2(-0.12f, 0.84f),
            new Vector2(-0.13f, 0.83f),
            new Vector2(-0.13f, 0.82f),
            new Vector2(-0.13f, 0.81f),
            new Vector2(-0.14f, 0.81f),
            new Vector2(-0.14f, 0.80f),
            new Vector2(-0.15f, 0.79f),
            new Vector2(-0.15f, 0.79f),
            new Vector2(-0.15f, 0.79f),
            new Vector2(-0.14f, 0.80f),
            new Vector2(-0.14f, 0.81f),
            new Vector2(-0.13f, 0.83f),
            new Vector2(-0.13f, 0.84f),
            new Vector2(-0.12f, 0.85f),
            new Vector2(-0.12f, 0.87f),
            new Vector2(-0.11f, 0.87f),
            new Vector2(-0.12f, 0.86f),
            new Vector2(-0.12f, 0.86f),
            new Vector2(-0.13f, 0.86f),
            new Vector2(-0.13f, 0.86f),
            new Vector2(-0.12f, 0.87f),
            new Vector2(-0.12f, 0.88f),
            new Vector2(-0.12f, 0.88f),
            new Vector2(-0.13f, 0.88f)

        };
        recognizer.SavePattern("Circle", circlePattern9);
        // Add more patterns as needed
        List <Vector2> starPattern = new List<Vector2>
    {
        new Vector2(0, 1),     // Top point
        new Vector2(0.5878f, 0.809f), // Upper right point
        new Vector2(0.9511f, 0.309f), // Lower right point
        new Vector2(0.9511f, -0.309f), // Lower left point
        new Vector2(0.5878f, -0.809f), // Upper left point
        new Vector2(0, -1),    // Bottom point
        new Vector2(-0.5878f, -0.809f), // Upper left point
        new Vector2(-0.9511f, -0.309f), // Lower left point
        new Vector2(-0.9511f, 0.309f), // Lower right point
        new Vector2(-0.5878f, 0.809f), // Upper right point
        new Vector2(0, 1)      // Back to top point to close the path
    };
        recognizer.SavePattern("Star", starPattern);
    }

    private void OnSelectPerformed(InputAction.CallbackContext context)
    {
        userGesture.Clear();
        isTracking = true;
    }

    private void OnSelectCanceled(InputAction.CallbackContext context)
    {
        isTracking = false;
        RecognizeGesture();
    }

    private void OnPositionPerformed(InputAction.CallbackContext context)
    {
        if (isTracking)
        {
            Vector3 position = context.ReadValue<Vector3>();

            // Convert the 3D position to 2D (e.g., ignoring the Z axis or projecting onto a plane)
            Vector2 point = new Vector2(position.x, position.y);
            userGesture.Add(point);
            Debug.Log("Point added: " + point);
        }
    }
    private void Update()
    {
        if (isTracking)
        {
            Vector3 position = wandTip.position;

            // Convert the 3D position to 2D (e.g., ignoring the Z axis or projecting onto a plane)
            //Vector2 point = new Vector2(position.x, position.y);
            Vector2 point = Camera.main.ScreenToWorldPoint(position);
            userGesture.Add(point);
            //Debug.Log("Point added: " + point);
        }
        /*
        if (recording)
        {
            RecordGesture();
        }*/
    }
    void RecognizeGesture()
    {
        Debug.Log("Gesture completed with " + userGesture.Count + " points");
        string pointstr = "";
        for(int i = 0; i < userGesture.Count; i++)
        {
            pointstr += userGesture[i].ToString()+',';
        }
        Debug.Log(pointstr);
        var result = recognizer.Recognize(userGesture);
        if (result.Match != null)
        {
            Debug.Log("Recognized Gesture: " + result.Match.Name + " with score: " + result.Score);
            text.text = "Recognized Gesture: " + result.Match.Name + " with score: " + result.Score;
            HandleGestureRecognition(result);
        }
        else
        {
            text.text = "No gesture recognized.";
            Debug.Log("No gesture recognized.");
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
