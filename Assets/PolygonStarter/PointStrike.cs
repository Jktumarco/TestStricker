using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointStrike : MonoBehaviour
{
    [SerializeField] Transform transform;
    [SerializeField] Animator animator;
    Camera camera;
    private void Awake()
    {
        camera = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            var m = Input.mousePosition;
            Ray ray = camera.ScreenPointToRay(m);
            if(Physics.Raycast(ray,out RaycastHit raycastHit))
            {
                transform.position = raycastHit.point;
                animator.Play("shoot");
            }
           
            
        }
    }
}
