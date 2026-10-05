using UnityEngine;

public class MouseAim : MonoBehaviour
{
    void Update()
    {
        Camera cam = Camera.main;
        Plane plane = new Plane(-cam.transform.forward, transform.position);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out float dist))
            transform.position = ray.GetPoint(dist);
    }
}