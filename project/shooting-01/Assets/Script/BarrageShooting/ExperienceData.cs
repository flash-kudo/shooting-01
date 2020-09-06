using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ExperienceData : ScriptableObject
{
    public int HitExp;
    public int ReflectExp;
    public int WallExp;
    public int MissShot;
    public int EarthDamage;

    public enum ExpType
    {
        HIT,
        REFLECT,
        WALL,
        MISS_SHOT,
        EARTH_DMG,
    }

    public int GetExpPoint(ExpType type)
    {
        switch (type)
        {
            case ExpType.HIT: return HitExp;
            case ExpType.REFLECT: return ReflectExp;
            case ExpType.WALL: return WallExp;
            case ExpType.MISS_SHOT: return MissShot;
            case ExpType.EARTH_DMG: return EarthDamage;
        }
        return 0;
    }
}

