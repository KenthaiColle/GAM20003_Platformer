using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowController : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform aimTransform;
    void Start()
    {
         aimTransform = transform.Find("Aim");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition.ToString());
        
        Vector3 mousePosition = GetMouseWorldPosition();

        // Vector with the value of X and Y
        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        // Pass Y and X and return the radians and turn radiants into degree
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        //Debug.Log(angle);
    }




    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;

    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
