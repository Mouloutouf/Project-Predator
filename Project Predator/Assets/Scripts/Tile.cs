using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Environments { None, HighGrass, SwampWater, DeepWater, Patio, Mud }

public class Tile : MonoBehaviour
{
    public Environments environment;

    public bool visible;
    public bool noise;
    public bool path;
    public bool traces;
    
    public GameObject tileObject;

    public GameObject selectable;
    public GameObject notSelectable;

    public bool selectionState;
}