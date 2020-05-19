using UnityEngine;

public class GetPatient : GAction {

    // Resource in this case = cubicle
    GameObject resource;

    public override bool PrePerform() {

        // Set our target patient and remove them from the Queue
        target = GWorld.Instance.GetResourceQueue("Patients").RemoveResourse();
        // Check that we did indeed get a patient
        if (target == null)
            // No patient so return false
            return false;
        // Grab a free cubicle and remove it from the list
        resource = GWorld.Instance.GetResourceQueue("Cubicles").RemoveResourse();
        GWorld.Instance.GetWorld().ModifyState("freeCubicle", -1);
        // Test did we get one?
        if (resource != null) {

            // Yes we have a cubicle
            inventory.AddItem(resource);
        } else {

            // No free cubicles so release the patient
            GWorld.Instance.GetResourceQueue("Patients").AddResourse(target);
            target = null;
            return false;
        }
        
        return true;
    }

    public override bool PostPerform() {

        // Remove a patient from the world
        GWorld.Instance.GetWorld().ModifyState("Waiting", -1);
        if (target) {

            target.GetComponent<GAgent>().inventory.AddItem(resource);
        }
        return true;
    }
}
