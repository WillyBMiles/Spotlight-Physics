using System.Collections.Generic;
using UnityEngine;

public class DeliveryTrigger : MonoBehaviour
{
    [SerializeField]
    AudioSource sound;
    [SerializeField]
    AudioSource win;

    private void OnEnable()
    {
        triggerHit = false;
        RegisterTrigger(this);
    }

    private void OnDisable()
    {
        triggers.Remove(this);
    }
    public static int numTriggers;
    public static List<DeliveryTrigger> triggers = new();

    static void RegisterTrigger(DeliveryTrigger trigger)
    {
        triggers.Add(trigger);
        numTriggers = triggers.Count;
    }
    static bool HitTrigger(DeliveryTrigger trigger)
    {

        triggers.Remove(trigger);
        if (triggers.Count == 0)
        {
            FinishLevel();
            return true;
        }
        return false;

    }
    static void FinishLevel()
    {
        Canvas.SetWin();
        
    }

    bool triggerHit = false;
    private void OnTriggerEnter(Collider other)
    {
        if (triggerHit)
            return;
        if (PhysicsObject.PhysicsObjects.TryGetValue(other.gameObject, out PhysicsObject obj))
        {
            if (HitTrigger(this))
            {
                win.Play();
            }
            triggerHit = true;
            sound.Play();

        }
            
    }
}
