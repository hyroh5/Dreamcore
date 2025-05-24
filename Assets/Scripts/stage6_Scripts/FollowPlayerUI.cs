using UnityEngine;

public class FollowPlayerUI : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0.5f, 0);

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + offset);
        transform.position = screenPos;
    }


}
