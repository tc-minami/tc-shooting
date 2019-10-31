using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public enum BulletType
    {
        Default,
        Rapid,
        Grendate,
        WideA,
        WideB,
    }

    [SerializeField]
    private BaseBullet defaultBulletPrefab;
    [SerializeField]
    private BaseBullet rapidBulletPrefab;
    [SerializeField]
    private BaseBullet grenadeBulletPrefab;

    [SerializeField]
    private float bulletSpeed = 1.0f;
    private float bulletSpawnPeriod = 0.1f;
    private Timer bulletSpawnTimer;
    private List<BaseBullet> bulletList;

    private GenericPool bulletPool;
    private BulletType bulletType;


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBulletType(BulletType _bulletType)
    {
        bulletType = _bulletType;
        SetBulletPrefab(_bulletType);
    }

    private void SetBulletPrefab(BulletType _bulletType)
    {
        switch (_bulletType)
        {
            case BulletType.Default:
            case BulletType.WideA:
            case BulletType.WideB:
                bulletPool.SetPoolableObject(defaultBulletPrefab, "Bullet");
                Debug.Log("Set Def Bullet");
                break;

            case BulletType.Grendate:
                bulletPool.SetPoolableObject(grenadeBulletPrefab, "GrenadeBullet");
                Debug.Log("Set Grenade Bullet");
                break;

            case BulletType.Rapid:
                bulletPool.SetPoolableObject(rapidBulletPrefab, "RapidBullet");
                Debug.Log("Set Rapid Bullet");
                break;

        }
    }


    private void Initialize()
    {
        bulletType = BulletType.Default;
        bulletPool = GenericPool.CreateNewPool(this.transform, "BulletPool");
        SetBulletPrefab(BulletType.Default);

        bulletSpawnTimer = Timer.InstantiateTimer(this.transform, "BulletTimer");
        bulletSpawnTimer.SetOnCompleteCallback(ShootBullet);
        bulletSpawnTimer.RunTimer(bulletSpawnPeriod, true);
    }

    private void ShootBullet()
    {
        BaseBullet bullet = bulletPool.GetOrCreate<BaseBullet>();
        bullet.InitializeBullet();
        bullet.SetBulletPos(this.transform.position);
    }
}
