using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Environments { HighGrass, SwampWater, DeepWater, Patio, Mud }

[Serializable]
public class Case
{
    public Environment _Environment;

    public Environments caseEnvironment;

    public bool visible;
    public bool conductive;
    public bool inPath;
    public int speedValue;
    public int stealthValue; public int StealthValue { get => _Environment.baseCases[caseEnvironment].stealthValue; set => stealthValue = value; }
    public bool guarded; public bool Guarded { get => _Environment.baseCases[caseEnvironment].guarded; set => guarded = value; }
    public string caseName; public string CaseName { get => _Environment.baseCases[caseEnvironment].caseName; set => caseName = value; }
}

[CreateAssetMenu(fileName = "New Environment Data", menuName = "Environment Scriptable Object")]
public class Environment : SerializedScriptableObject
{
    public Dictionary<Environments, Case> baseCases = new Dictionary<Environments, Case>();
}
