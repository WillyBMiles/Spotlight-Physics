using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public static IReadOnlyDictionary<GameObject, PhysicsObject> PhysicsObjects => physicsObjects;
    readonly static Dictionary<GameObject, PhysicsObject> physicsObjects = new();

    [SerializeField]
    LineRenderer moveDirection;
    [SerializeField]
    LineRenderer impulseDirection;

    public List<ContactPoint> storedContacts = new();

    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pause();
        physicsObjects[gameObject] = this;
    }
    private void OnDestroy()
    {
        physicsObjects.Remove(gameObject);
    }

    bool currentlyPaused = true;

    private void LateUpdate()
    {


        if (rb.isKinematic && !currentlyPaused)
        {
            UnpauseInternal();
        }
        else if (!rb.isKinematic && currentlyPaused)
        {
            PauseInternal();
        }

        if (currentlyPaused)
        {
            const float VELOCITY_SCALING = 1 / 5f;
            moveDirection.SetPosition(0, transform.position);
            moveDirection.SetPosition(1, transform.position + velocity * VELOCITY_SCALING);

            Vector3 contactDir = new();
            foreach (ContactPoint contactPoint in storedContacts)
            {
                contactDir -= contactPoint.impulse;
            }
            impulseDirection.SetPosition(0, transform.position);
            impulseDirection.SetPosition(1, transform.position + contactDir * VELOCITY_SCALING);


        }
        else
        {
            moveDirection.SetPosition(0, transform.position);
            moveDirection.SetPosition(1, transform.position);
            impulseDirection.SetPosition(0, transform.position);
            impulseDirection.SetPosition(1, transform.position);
        }
    }

    Vector3 velocity;
    Vector3 angularVelocity;
    public void Pause()
    {
        currentlyPaused = true;
    }
    void PauseInternal()
    {
        if (rb.isKinematic)
            return;
        velocity = rb.linearVelocity;
        angularVelocity = rb.angularVelocity;
        rb.linearVelocity = new();
        rb.angularVelocity = new();

        rb.isKinematic = true;
    }

    public void UnPause()
    {
        currentlyPaused = false;
    }

    void UnpauseInternal()
    {
        if (!rb.isKinematic)
            return;
        rb.isKinematic = false;
        rb.linearVelocity = velocity;
        rb.angularVelocity = angularVelocity;

        foreach (var contact in storedContacts)
        {
            rb.AddForceAtPosition(-contact.impulse, contact.point, ForceMode.Impulse);
        }
        storedContacts.Clear();
    }

    List<ContactPoint> tempContacts = new();
    private void OnCollisionEnter(Collision collision)
    {
        if (rb.isKinematic || !PhysicsObject.PhysicsObjects.TryGetValue(collision.gameObject, out PhysicsObject obj))
            return;

        
        if (obj.rb.isKinematic)
        {
            tempContacts.Clear();
            collision.GetContacts(tempContacts);
            obj.storedContacts.AddRange(tempContacts);
        }
    }

    public void Fire()
    {
        Destroy(this);
    }

    public bool Launch(Vector3 direction, float amount)
    {
        Vector3 vel = rb.isKinematic ? velocity : rb.linearVelocity;

        if (Vector3.Dot(vel,direction) <= 0f) //aka we're not already going in that direction
        {
            if (rb.isKinematic)
            {
                velocity += direction * amount;
            }
            else
            {
                rb.linearVelocity += direction * amount;
            }
            return true;
        }
        return false;
    }
}
