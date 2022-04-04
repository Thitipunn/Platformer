using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]private float followSpeed = 2f;
    public float yOffset = 1f;
    public Transform target;

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CameraFollow();
    }

    void CameraFollow()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
