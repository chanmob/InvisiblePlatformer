using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    private List<GameObject> bulletLists = new List<GameObject>();
    public GameObject bulletPrefab;
 
    private void CreateBulletObject(int _count)
    {
        for(int i = 0; i < _count; i++)
        {
            var b = Instantiate(bulletPrefab);
            bulletLists.Add(b);
            b.SetActive(false);
        }
    }

    public GameObject GetBullet()
    {
        GameObject returnBullet = null;

        if(bulletLists.Count > 0)
        {
            returnBullet = bulletLists[0];
            bulletLists.RemoveAt(0);
        }
        else
        {
            returnBullet = Instantiate(bulletPrefab);
        }

        return returnBullet;
    }

    public void WaitBullet(GameObject _bullet)
    {
        _bullet.SetActive(false);
        bulletLists.Add(_bullet);
    }
}
