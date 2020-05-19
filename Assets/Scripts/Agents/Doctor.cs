using UnityEngine;

public class Doctor
    : GAgent {

    new void Start() {

        // Call base Start method
        base.Start();
        // Set goal so that it can't be removed so the nurse can repeat this action
        SubGoal s1 = new SubGoal("doPaperwork", 1, false);
        goals.Add(s1, 1);

        // Resting goal
        SubGoal s2 = new SubGoal("rested", 1, false);
        goals.Add(s2, 3);
        // Toilet  goal
        SubGoal s3 = new SubGoal("emptyBladder", 1, false);
        goals.Add(s3, 5);

        // Call the GetTired() method for the first time
        Invoke(nameof(GetTired), Random.Range(10.0f, 20.0f));
        Invoke(nameof(NeedToPee), Random.Range(40.0f, 80.0f));
    }

    void GetTired() {

        beliefs.ModifyState("exhausted", 1);
        //call the get tired method over and over at random times to make de doctor
        //get tired again
        Invoke(nameof(GetTired), Random.Range(10.0f, 20.0f));
    }
    
    void NeedToPee() {

        beliefs.ModifyState("needToPee", 1);
        //call the get tired method over and over at random times to make de doctor
        //get tired again
        Invoke(nameof(NeedToPee), Random.Range(60f, 100.0f));
    }

}