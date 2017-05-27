using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;           // For player character transform information.
    private Vector3 offset;             // Distance between the player and the camera.

    // Use this for initialization.
    void Start()
    {
        if (!player)
        {
            player = GameObject.Find("Player");
        }

        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame.
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
