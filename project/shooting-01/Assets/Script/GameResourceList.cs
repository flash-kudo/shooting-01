using System.Collections;
using System.Collections.Generic;

public class GameResourceList
{
    public static Dictionary<string, string> PathList = new Dictionary<string, string>()
    {
        {"bullet1".ToLower(), "EnemyBullet"},
        {"bullet2".ToLower(), "EnemyBullet2"},
        {"muzzle".ToLower(), "BulletMuzzle"},
        {"muzzle2".ToLower(), "BulletMuzzle2"},

        {"meteor01".ToLower(), "Enemy_00_innseki_01"},
        {"meteor02".ToLower(), "Enemy_00_innseki_02"},
        {"charge01".ToLower(), "Enemy_01_totugeki"},
        {"shooter01".ToLower(), "Enemy_02_syageki_01" },
        {"shooter02".ToLower(), "Enemy_02_syageki_02" },
        {"fortress".ToLower(), "Enemy_03_yousai"},
        {"mega_meteor".ToLower(), "Enemy_04_big_innseki" },

        {"ExploadSmall".ToLower(), "ExplosionSmall" },
        {"ExploadBig".ToLower(), "ExplosionBig" },
        {"ExploadBullet".ToLower(), "BulletOffset" },
        {"HitMark".ToLower(), "HitMark" },
        {"Reflect".ToLower(), "ShotRefrect" },
        
    };
}


