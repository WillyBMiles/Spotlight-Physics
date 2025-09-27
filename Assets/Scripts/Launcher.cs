using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField]
    float force;

    Animator animator;
    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PhysicsObject.PhysicsObjects.TryGetValue(other.gameObject, out PhysicsObject physicsObject))
        {
            if (physicsObject.Launch(transform.up, force))
            {
                animator.SetTrigger("Boing");
            }
        }
    }
}
