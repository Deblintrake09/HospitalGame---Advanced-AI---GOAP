using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ResourceData",menuName="ResourceData", order = 0)]
public class ResourceSO : ScriptableObject{
    public ResourceTypes resourceTag;
    public ResourceQueueEnum resourceQueue;
    public ResourceStates resourceState;
}
