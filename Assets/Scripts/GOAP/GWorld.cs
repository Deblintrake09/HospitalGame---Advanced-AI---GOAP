using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using System.Linq;

public class ResourceQueue
{
    private Queue<GameObject> _queue = new Queue<GameObject>();
    public ResourceTypes tag;
    public ResourceStates modState;

    public ResourceQueue(ResourceTypes tag, ResourceStates modState, WorldStates w)
    {
        this.tag = tag;
        this.modState = modState;
        if (tag != ResourceTypes.Untagged)
        {
            GameObject[] r = GameObject.FindGameObjectsWithTag(tag.ToString());
            foreach (var VARIABLE in r)
            {
                _queue.Enqueue(VARIABLE);
            }
        }

        if (modState != ResourceStates.nullState)
        {
            w.ModifyState(modState.ToString(),_queue.Count);
        }
    }

    public void AddResourse(GameObject g)
    {
        _queue.Enqueue(g);
    }

    public GameObject RemoveResourse()
    {
        if (_queue.Count == 0) return null;
        return _queue.Dequeue();
    }

    public void RemoveResourse(GameObject g)
    {        // replaces the queue with a new one made of all objects that are no the gameobject to remove
        _queue = new Queue<GameObject>(_queue.Where(p=>p!=g));
    }
    
}
public sealed class GWorld {

    // Our GWorld instance
    // Our world states
    private static WorldStates world;
    // Queue of patients
    private static ResourceQueue patients;
    // Queue of cubicles
    private static ResourceQueue cubicles;
    private static ResourceQueue _offices;
    private static ResourceQueue _toilets;
    private static ResourceQueue _puddles;
    private static Dictionary<string, ResourceQueue> _resources =new Dictionary<string, ResourceQueue>();

    static GWorld() {

        // Create our world
        world = new WorldStates();
        
        patients = new ResourceQueue(ResourceTypes.Patient,ResourceStates.nullState,world);
        _resources.Add(ResourceQueueEnum.Patients.ToString(),patients);
        cubicles = new ResourceQueue(ResourceTypes.Cubicle,ResourceStates.freeCubicle,world);
        _resources.Add(ResourceQueueEnum.Cubicles.ToString(),cubicles);
        _offices = new ResourceQueue(ResourceTypes.Office,ResourceStates.freeOffice, world);
        _resources.Add(ResourceQueueEnum.Offices.ToString(),_offices);
        _toilets=new ResourceQueue(ResourceTypes.Toilet,ResourceStates.freeToilet,world);
        _resources.Add(ResourceQueueEnum.Toilets.ToString(),_toilets);
        _puddles=new ResourceQueue(ResourceTypes.Puddle,ResourceStates.puddles,world);
        _resources.Add(ResourceQueueEnum.Puddles.ToString(),_puddles);
        // Set the time scale in Unity
        Time.timeScale = 5.0f;
    }

    public ResourceQueue GetResourceQueue(string type)
    {
        return _resources[type];
    }

    private GWorld() {

    }
    
    
    public static GWorld Instance { get; } = new GWorld();

    public WorldStates GetWorld() {

        return world;
    }
}
