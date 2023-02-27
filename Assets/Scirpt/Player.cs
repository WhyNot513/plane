using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public enum PlaneType
{
    DefaultPanel,
    AttackPanel,
    AddbloodPlane,
    TimeSlowPlane,
    addDamagePlane
}
public enum ProjectileType
{
    Basebullets, //基础子弹
    Laserbullets,//激光
    Trackbullets, //追踪
    Missilebullets,//导弹
    Shotguns//散弹

}


public class Player : MonoBehaviour
{
    public static bool CanMove { get; set; }//判断玩家是否可以进行移动

    public PlaneType plane;
    public ProjectileType ProjectileType;
    public bool CanFire { get; set; }//判断玩家是否可以开火
                                     //  [SerializeField] float IntervalTime = 1f;
                                     // WaitForSeconds IntervalTimeBullet;
    public List<GameObject> Bullet_list = new List<GameObject>();//玩家子弹总类
    public List<GameObject> BranchPos_list = new List<GameObject>();//玩家子弹发射位置
    [SerializeField, Range(0, 10)] int weaponPower = 0;                                                       // Start is called before the first frame update
    Coroutine Onfire_Cor;
    //.....能量爆发
    public bool isOverDriving;//判断是否开启能量爆发状态/
    //public float OverDrivingSpeed;//能量爆发速度
    //WaitForSeconds OverDrivingTimeBullet;

    public float LastTime;
    public float Firetime;
    public GameObject Laser; //激光武器

    public static Shields playshields;
    GameObject Projectile = null;
    private void Awake()
    {
        playshields = GetComponent<Player_Shields>();
    }
    void Start()
    {

        CanMove = true;
        //  Onfire_Cor = StartCoroutine(OnFirePlayer(IntervalTime));

    }
    private void OnEnable()
    {

        SelectBullet(ProjectileType, ref Projectile);

        if ((UpdateManager.Instance.Grade_Track >= 2) && Projectile == Bullet_list[3]) //追踪子弹升级到三级后
        {
            Firetime = 0.4f;
        }
        else
        {
            Firetime = 1f;
        }
        playerOverdrive.on += OverDriveOn;
        playerOverdrive.off += OverDriveOff;
    }

    private void OnDisable()
    {
        playerOverdrive.on -= OverDriveOn;
        playerOverdrive.off -= OverDriveOff;
    }
    // Update is called once per frame


    public float c;
    void Update()
    {
        //if (playshields.enabled == false && EnemyManager.Instance.EnemyDestory && (UpdateManager.Instance.Grade_Shotgun >= 2) && Projectile == Bullet_list[2])
        //{

        //    playshields.enabled = true;
        //}
        if (playshields.enabled && (UpdateManager.Instance.Grade_Shotgun >= 3) && Projectile == Bullet_list[2])
        {
            Firetime = 0.5f;
        }


        //   Debug.LogWarning(isOverDriving + "---------" + Time.timeScale);
        if (isOverDriving == false)
        {
            OverDrive();

        }
        LastTime = Time.unscaledTime - c;

        if (gameObject.activeSelf && LastTime > Firetime)
        {

            c = Time.unscaledTime;


            SelectBullet(ProjectileType, ref Projectile);
            if (Projectile != null)
            {
                Laser.SetActive(false);
                switch (weaponPower)
                {

                    case 0:
                        PoolManager.Release(Projectile, BranchPos_list[2].transform.position);
                        break;

                    case 1:
                        PoolManager.Release(Projectile, BranchPos_list[0].transform.position, BranchPos_list[0].transform.rotation);
                        PoolManager.Release(Projectile, BranchPos_list[1].transform.position, BranchPos_list[1].transform.rotation);

                        break;
                    case 2:
                        PoolManager.Release(Projectile, BranchPos_list[0].transform.position, BranchPos_list[0].transform.rotation);
                        PoolManager.Release(Projectile, BranchPos_list[1].transform.position, BranchPos_list[1].transform.rotation);
                        PoolManager.Release(Projectile, BranchPos_list[2].transform.position, BranchPos_list[2].transform.rotation);

                        break;

                    case 7:
                        for (int i = 0; i < 8; i++)
                        {
                            var a = new Quaternion(0, 0, -0.4f + 0.1f * i, 1.0f);
                            PoolManager.Release(Projectile, BranchPos_list[2].transform.position, a);

                        }

                        break;

                    default:
                        break;
                }
            }



        }

    }

    IEnumerator OnFirePlayer(float IntervalTime)
    {
        //  GameObject OverDriveProjectile = Bullet_list[3];
        //  IntervalTimeBullet = new WaitForSeconds(IntervalTime);
        while (true)
        {
            //if (gameObject.activeSelf)
            //{
            //    switch (weaponPower)
            //    {
            //        case 0:
            //            PoolManager.Release(isOverDriving ? OverDriveProjectile : Bullet_list[0], BranchPos_list[2].transform.position);

            //            break;

            //        case 1:
            //            PoolManager.Release(isOverDriving ? OverDriveProjectile : Bullet_list[0], BranchPos_list[0].transform.position);
            //            PoolManager.Release(isOverDriving ? OverDriveProjectile : Bullet_list[0], BranchPos_list[1].transform.position);

            //            break;
            //        case 2:
            //            PoolManager.Release(isOverDriving ? OverDriveProjectile : Bullet_list[2], BranchPos_list[0].transform.position);
            //            PoolManager.Release(isOverDriving ? OverDriveProjectile : Bullet_list[1], BranchPos_list[1].transform.position);
            //            PoolManager.Release(isOverDriving ? OverDriveProjectile : Bullet_list[0], BranchPos_list[2].transform.position);

            //            break;

            //        default:
            //            break;
            //    }


            //}
            yield return null;

            //yield return isOverDriving ? OverDrivingTimeBullet : IntervalTimeBullet;
            //if (isOverDriving)
            //{
            //    switch (plane)
            //    {
            //        case PlaneType.DefaultPanel:
            //            //   OverDriveProjectile = Bullet_list[3];
            //            break;
            //        case PlaneType.AttackPanel:
            //            //  OverDriveProjectile = Bullet_list[4];
            //            weaponPower = 0;
            //            break;
            //    }
            //}

        }
    }
    #region OverDrive 能量爆发
    void OverDrive()
    {
        //Debug.Log(PlayerEnergy.Instance.energy);
        if (!PlayerEnergy.Instance.isEnough(PlayerEnergy.MAX)) return;
        //  Debug.Log(PlayerEnergy.Instance.energy + "--------------");
        isOverDriving = true;
        playerOverdrive.on();


    }
    void OverDriveOn()
    {

        // isOverDriving = true;
        // Debug.Log("1666666666666666666666");
        if (plane == PlaneType.TimeSlowPlane)
        {
            Timecontroller.Instance.BulletTime(1f, 0.3f);
        }

        MusrKillManager.Instance.OverDrving(plane);
    }
    void OverDriveOff()
    {
        // Debug.Log("11111111111111111111111");
        isOverDriving = false;

    }
    #endregion

    #region 选择子弹
    void SelectBullet(ProjectileType projectileType, ref GameObject Projectile)
    {
        switch (projectileType)
        {
            case ProjectileType.Basebullets:
                Projectile = Bullet_list[0];
                break;
            case ProjectileType.Laserbullets:
                Projectile = null;
                Laser.SetActive(true);
                break;
            case ProjectileType.Trackbullets:
                Projectile = Bullet_list[3];
                break;
            case ProjectileType.Missilebullets:
                Projectile = Bullet_list[4];
                break;
            case ProjectileType.Shotguns:
                Projectile = Bullet_list[2];
                break;
            default:
                break;
        }
    }
    #endregion




}
