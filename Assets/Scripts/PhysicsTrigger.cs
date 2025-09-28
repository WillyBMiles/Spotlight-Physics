using UnityEngine;

public class PhysicsTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (PhysicsObject.PhysicsObjects.TryGetValue(other.gameObject, out PhysicsObject obj))
            obj.UnPause();
    }

    private void OnTriggerExit(Collider other)
    {
        if (PhysicsObject.PhysicsObjects.TryGetValue(other.gameObject, out PhysicsObject obj))
            obj.Pause();
    }
}
