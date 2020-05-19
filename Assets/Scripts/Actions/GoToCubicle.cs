public class GoToCubicle : GAction {

    public override bool PrePerform() {

        // Check if we have a cubicle in the inventory
        target = inventory.FindItemWithTag("Cubicle");
        // Check that we did indeed get a cubicle
        if (target == null)
            // No cubicle so return false
            return false;
        // All good
        return true;
    }

    public override bool PostPerform() {

        // Add a new state so the nurse knows how many patients she has treated 
        beliefs.ModifyState("PatientTreated", 1);
        // Give back the cubicle
        GWorld.Instance.GetResourceQueue("Cubicles").AddResourse(target);
        GWorld.Instance.GetWorld().ModifyState("freeCubicle",+1);
        // Remove the cubicle from the list
        inventory.RemoveItem(target);
        return true;
    }
}
