using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : UnitySingleton<UpdateManager>

{
    public Player player;
    //.................散弹.........////
    public int Grade_Shotgun;
    public int Grade_Missile;
    public int Grade_Track;
    public int Grade_Laser;
    public int Grade_Default;
    public GameObject Grade_Track3Vfx;
    public void UpdateBullet(int index)
    {
        Debug.LogWarning(index);
        switch (index)
        {
            case 0:

                Grade_Default++;
                break;
            case 1:
                Grade_Missile++;
                break;
            case 2:
                Grade_Laser++;
                break;
            case 3:
                Grade_Track++;
                break;
            case 4:
                Grade_Shotgun++;
                break;
            default:
                break;
        }
    }
    void ShotgunUpdate()
    {

        GameObject shotgun = player.Bullet_list[2];
        foreach (var it in PoolManager.dictionary[shotgun].queue)
        {
            var item = it.GetComponent<PlayerProjectile>();
            item.Grade++;
            Grade_Shotgun = item.Grade;
        }

    }
    void missileUpdate()

    {
        GameObject shotgun = player.Bullet_list[4];
        foreach (var it in PoolManager.dictionary[shotgun].queue)
        {
            var item = it.GetComponent<PlayerProjectile>();
            item.Grade++;
            Grade_Missile = item.Grade;
        }
    }
    void TrackUpdate()

    {
        GameObject shotgun = player.Bullet_list[3];
        foreach (var it in PoolManager.dictionary[shotgun].queue)
        {
            var item = it.GetComponent<PlayerProjectile>();
            item.Grade++;
            Grade_Track = item.Grade;
        }
    }

    void DefaultUpdate()
    {
        GameObject shotgun = player.Bullet_list[0];
        foreach (var it in PoolManager.dictionary[shotgun].queue)
        {
            var item = it.GetComponent<PlayerProjectile>();
            item.Grade++;
            Grade_Default = item.Grade;
        }
    }



}
