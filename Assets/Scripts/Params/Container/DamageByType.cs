using UnityEngine;

[System.Serializable]
public class DamageByType
{
    [SerializeField] private DamageType damageType;

    [Range(0,10000)]
    [SerializeField] private float value;

    public DamageType DamageType
    {
        get
        {
            return damageType;
        }
    }

    public float Value
    {
        get
        {
            return value;
        }
    }
}

public enum DamageType
{
    Magic, Physic, Mental, Other
}