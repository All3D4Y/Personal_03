using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    //BulletPool bullet;
    protected override void OnInitialize()
    {
        // 풀 초기화
        //bullet = GetComponentInChildren<BulletPool>();
        //if (bullet != null)
        //    bullet.Initialize();
    }

    // 풀에서 오브젝트 가져오는 함수들 ------------------------------------------------------------------
    //public Bullet GetBullet(Vector3? position, float angle = 0.0f)
    //{
    //    //Vector3.forward * angle
    //    return bullet.GetObject(position, new Vector3(0, 0, angle));
    //}
}
