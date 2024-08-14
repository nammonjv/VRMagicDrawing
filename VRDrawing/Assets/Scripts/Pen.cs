using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using static GestureManagerVR.SampleDisplay;

public class Pen : MonoBehaviour
{
    [Header("Pen Properties")]
    public Transform tip;
    public Material drawingMaterial;
    public Material tipMaterial;
    [Range(0.01f, 0.1f)]
    public float penWidth = 0.01f;
    public Color[] penColors;


    [Header("Hands & Grabbable")]
    public XRGrabInteractable grabbable;
    

    private LineRenderer currentDrawing;
    private int index;
    private int currentColorIndex;
    [SerializeField] private InputActionReference primaryButtonRight;
    [SerializeField] private InputActionReference primaryButtonLeft;




    private int stroke_index = 0;
    private List<string> stroke = new List<string>();

    private GameObject starParent;
    
    private void OnEnable()
    {
        starParent = GameObject.Find("star");
        //primaryButtonRight.action.performed += addToStrokeTrail;
    }
    private void Start()
    {/*
        grabbable = this.GetComponent<XRGrabInteractable>();
        currentColorIndex = 0;
        tipMaterial.color = penColors[currentColorIndex];*/
    }
    private void Update()
    {
        /*
        bool isGrabbed = grabbable.isSelected;
        bool isRightHandDrawing = isGrabbed && grabbable.IsSelectedByRight()&&primaryButtonRight.action.IsPressed() ; //&& OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger);
        bool isLeftHandDrawing = isGrabbed && grabbable.IsSelectedByLeft() && primaryButtonLeft.action.IsPressed(); //&& OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
        if (isRightHandDrawing || isLeftHandDrawing)
        {
            Draw();
        }
        else if (currentDrawing != null)
        {
            currentDrawing = null;
        }*/

        if (primaryButtonRight.action.IsPressed())
        {
            addToStrokeTrail();
        }
        else if (!primaryButtonLeft.action.IsPressed()&&stroke_index!=0)
        {
            DeleteStroke();
        }
        
    }

    private void Draw()
    {
        if (currentDrawing == null)
        {
            index = 0;
            currentDrawing = new GameObject().AddComponent<LineRenderer>();
            currentDrawing.material = drawingMaterial;
            currentDrawing.startColor = currentDrawing.endColor = penColors[currentColorIndex];
            currentDrawing.startWidth = currentDrawing.endWidth = penWidth;
            currentDrawing.positionCount = 1;
            currentDrawing.SetPosition(0, tip.position);
        }
        else
        {
            var currentPos = currentDrawing.GetPosition(index);
            if (Vector3.Distance(currentPos, tip.position) > 0.01f)
            {
                index++;
                currentDrawing.positionCount = index + 1;
                currentDrawing.SetPosition(index, tip.position);
            }
        }
    }

    private void SwitchColor()
    {
        if (currentColorIndex == penColors.Length - 1)
        {
            currentColorIndex = 0;
        }
        else
        {
            currentColorIndex++;
        }
        tipMaterial.color = penColors[currentColorIndex];
    }

    public void addToStrokeTrail()//InputAction.CallbackContext ctx)
    {
        //Debug.Log("pressed");
        Vector3 p = tip.position;
        GameObject star_instance = Instantiate(GameObject.Find("star"));
        GameObject star = new GameObject("stroke_" + stroke_index++);
        star_instance.name = star.name + "_instance";
        star_instance.transform.SetParent(star.transform, false);
        System.Random random = new System.Random();
        star.transform.position = new Vector3((float)random.NextDouble() / 80, (float)random.NextDouble() / 80, (float)random.NextDouble() / 80) + p;
        star.transform.rotation = new Quaternion((float)random.NextDouble() - 0.5f, (float)random.NextDouble() - 0.5f, (float)random.NextDouble() - 0.5f, (float)random.NextDouble() - 0.5f).normalized;
        star.transform.rotation.Normalize();
        float star_scale = (float)random.NextDouble() + 0.3f;
        star.transform.localScale = new Vector3(star_scale, star_scale, star_scale);
        
        stroke.Add(star.name);
    }

    public void DeleteStroke()
    {
        foreach(string star in stroke)
        {
            Destroy(GameObject.Find(star));
            stroke_index = 0;
        }
        
    }
}
