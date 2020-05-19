using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour{
    public ResourceSO resourceData;

    private void Start()
    {
        this.gameObject.tag = resourceData.resourceTag.ToString();
    }
}
