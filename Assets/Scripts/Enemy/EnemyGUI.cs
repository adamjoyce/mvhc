using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[RequireComponent(typeof(LineRenderer))]
public class EnemyGUI : MonoBehaviour
{
    public GUISkin enemyGUISkin;

    //private LineRenderer GUISelectionLine;

    private Vector3 enemyScreenLocation;

    private Vector2 buttonOneOffset;
    private Vector2 buttonTwoOffset;
    private Rect leftButtonRect;
    private Rect rightButtonRect;
    private float buttonWidth;
    private float buttonHeight;

    private bool isEnabled;
    private bool hoverLeftButton;
    private bool hoverRightButton;

    public bool IsEnabled
    {
        get { return isEnabled; }
        set { isEnabled = value; }
    }

    public bool HoverLeftButton
    {
        get { return hoverLeftButton; }
    }

    public bool HoverRightButton
    {
        get { return hoverRightButton; }
    }

    /* Use for initialisation. */
    private void Start()
    {
        if (!enemyGUISkin)
        {
            Debug.LogError("Error: enemyGUISkin reference missing - attempting to load manually from path");
            enemyGUISkin = (GUISkin)AssetDatabase.LoadAssetAtPath("Assets/GUI/EnemyGUISkin.guiskin", typeof(GUISkin));
            if (enemyGUISkin)
            {
                Debug.Log("enemyGUISkin successfully manually loaded");
            }
            else
            {
                Debug.LogError("Error: enemyGUISkin could not be loaded");
            }
        }

        //GUISelectionLine = GetComponent<LineRenderer>();
        //GUISelectionLine.startWidth = 0.2f;
        //GUISelectionLine.endWidth = 0.2f;
        //GUISelectionLine.SetVertexCount(2);
        //GUISelectionLine.enabled = true;

        buttonWidth = 200.0f;
        buttonHeight = 200.0f;

        float buttonOffset = 220.0f;
        buttonOneOffset = new Vector2(-buttonOffset, -(buttonHeight * 0.5f));
        buttonTwoOffset = new Vector2(buttonOffset - buttonWidth, -(buttonHeight * 0.5f));
        leftButtonRect = new Rect();
        rightButtonRect = new Rect();

        isEnabled = false;
        hoverLeftButton = false;
        hoverRightButton = false;
    }

    /* Update is called once per frame. */
    private void Update()
    {
        enemyScreenLocation = Camera.main.WorldToScreenPoint(transform.position);

        if (isEnabled)
        {
            // Get the mouse position in GUI coordinates.
            Vector2 GUIMousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

            // Check if the mouse is hovering over the left button option.
            if (!hoverLeftButton && leftButtonRect.Contains(GUIMousePosition))
            {
                hoverLeftButton = true;
                //Debug.Log("HOVER LEFT");
            }
            else if (hoverLeftButton && !(leftButtonRect.Contains(GUIMousePosition)))
            {
                hoverLeftButton = false;
            }

            // Check if the mouse is hovering over the right button option.
            if (!hoverRightButton && rightButtonRect.Contains(GUIMousePosition))
            {
                hoverRightButton = true;
                //Debug.Log("HOVER RIGHT");
            }
            else if(hoverRightButton && !(rightButtonRect.Contains(GUIMousePosition)))
            {
                hoverRightButton = false;
            }
        }
    }

    /* Called for rendering and handling GUI events. */
    private void OnGUI()
    {
        if (isEnabled)
        {
            GUI.skin = enemyGUISkin;

            // Draw the left option button.
            leftButtonRect = new Rect(enemyScreenLocation.x + buttonOneOffset.x, (Screen.height - enemyScreenLocation.y) + buttonOneOffset.y, buttonWidth, buttonHeight);
            GUI.skin.button.alignment = TextAnchor.MiddleRight;
            GUI.Button(leftButtonRect, "Eliminate");

            // Draw the right option button.
            rightButtonRect = new Rect(enemyScreenLocation.x + buttonTwoOffset.x, (Screen.height - enemyScreenLocation.y) + buttonTwoOffset.y, buttonWidth, buttonHeight);
            GUI.skin.button.alignment = TextAnchor.MiddleLeft;
            if (!GetComponent<EnemyHealth>().IsSubdued)
            {
                GUI.Button(rightButtonRect, "Subdue");
            }
            else
            {
                GUI.Button(rightButtonRect, "Amalgamate");
            }
        }
    }
}