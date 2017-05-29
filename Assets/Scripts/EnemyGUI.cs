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
    private float buttonWidth;
    private float buttonHeight;

    private bool isEnabled;

    public bool IsEnabled
    {
        get { return isEnabled; }
        set { isEnabled = value; }
    }

    // Use for initialisation.
    void Start()
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

        buttonWidth = 75.0f;
        buttonHeight = 30.0f;

        float buttonOffset = 100.0f;
        buttonOneOffset = new Vector2(-buttonOffset, -(buttonHeight * 0.5f));
        buttonTwoOffset = new Vector2(buttonOffset - buttonWidth, -(buttonHeight * 0.5f));

        isEnabled = false;
    }

    // Update is called once per frame.
    void Update()
    {
        enemyScreenLocation = Camera.main.WorldToScreenPoint(transform.position);

        //GUISelectionLine.SetPosition(0, transform.position);

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //Physics.Raycast(ray, out hit, 100);


        //Vector3 cursorLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //GUISelectionLine.SetPosition(1, hit.point);
}

    // Called for rendering and handling GUI events.
    void OnGUI()
    {
        if (isEnabled)
        {
            GUI.skin = enemyGUISkin;

            // Draw the player option buttons.
            GUI.Button(new Rect(enemyScreenLocation.x + buttonOneOffset.x, (Screen.height - enemyScreenLocation.y) + buttonOneOffset.y, buttonWidth, buttonHeight), "Eliminate");
            GUI.Button(new Rect(enemyScreenLocation.x + buttonTwoOffset.x, (Screen.height - enemyScreenLocation.y) + buttonTwoOffset.y, buttonWidth, buttonHeight), "Subdue");
        }
    }
}