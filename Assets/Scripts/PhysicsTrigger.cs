using UnityEngine;

public class PhysicsTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
