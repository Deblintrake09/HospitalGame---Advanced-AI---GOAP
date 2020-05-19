using UnityEngine;

public class Patient : GAgent {

    new void Start() {

        // Call the base start
        base.Start();
        // Set up the subgoal "isWaiting"
        SubGoal s1 = new SubGoal("isWaiting", 1, true);
        // Add it to the goals
        goals.Add(s1, 4);

        // Set up the subgoal "isTreated"
        SubGoal s2 = new SubGoal("isTreated", 1, true);
        // Add it to the goals
        goals.Add(s2, 5);

        // Set up the subgoal "isHome"
        SubGoal s3 = new SubGoal("isHome", 1, false);
        // Add it to the goals
        goals.Add(s3, 1);
        SubGoal s4 = new SubGoal("emptyBladder", 1, false);
        goals.Add(s4, 2);
        
        
        Invoke(nameof(NeedToPee), Random.Range(40.0f, 80.0f));
    }
    
    void NeedToPee() {

        beliefs.ModifyState("needToPee", 1);
        //call the get tired method over and over at random times to make de doctor
        //get tired again
        Invoke(nameof(NeedToPee), Random.Range(60f, 100.0f));
    }

}