using UnityEngine;

public class GraphicsFollow : MonoBehaviour
{
    public Transform playerObject;
    public ThirdPersonController tpController;
    public Vector3 followPosition;

    void Update ()
    {
        followPosition.x = playerObject.position.x;
        followPosition.z = playerObject.position.z;
        followPosition.y = playerObject.position.y;

        if (tpController.isCrouch || tpController.controller.height == 2.8f)
        {
            followPosition.y -= 1.45f;

            if (!tpController.isMove)
            {
                followPosition.y +=0.0f;
            }
        }
        else
        {
            followPosition.y -= 1.8681002f;
        }

        //transform.position = playerObject.position;
        transform.position = followPosition;
        transform.rotation = playerObject.rotation;
    }
}
