using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyGUI : MonoBehaviour
{
    public GUISkin enemyGUISkin;

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
    }

    // Called for rendering and handling GUI events.
    void OnGUI()
    {
        if (isEnabled)
        {
            GUI.skin = enemyGUISkin;
            //GUI.Label(new Rect(enemyScreenLocation.x + buttonOneOffset.x, (Screen.height - enemyScreenLocation.y) + buttonOneOffset.y, buttonWidth, buttonHeight), "Eliminate");
            //GUI.Label(new Rect(enemyScreenLocation.x + buttonTwoOffset.x, (Screen.height - enemyScreenLocation.y) + buttonTwoOffset.y, buttonWidth, buttonHeight), "Subdue");
            GUI.Button(new Rect(enemyScreenLocation.x + buttonOneOffset.x, (Screen.height - enemyScreenLocation.y) + buttonOneOffset.y, buttonWidth, buttonHeight), "Eliminate");
            GUI.Button(new Rect(enemyScreenLocation.x + buttonTwoOffset.x, (Screen.height - enemyScreenLocation.y) + buttonTwoOffset.y, buttonWidth, buttonHeight), "Subdue");
        }
    }
}