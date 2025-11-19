using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
	
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
        }
    }
}
