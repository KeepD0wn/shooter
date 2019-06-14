using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AllWeapon : MonoBehaviour
{
    public abstract int CurrenPatrons { get; set; }

    public abstract bool IsDropped { get; set; }

    /// <summary>
    /// возвращает отдачу в центр прицела
    /// </summary>
    public abstract void ReturnRay();

    /// <summary>
    /// Чек на кд между выстрелами
    /// </summary>
    public abstract bool CanShootCoolDown();

    /// <summary>
    /// рандомим смещение raycastа по x и y
    /// </summary>
    public abstract void RandomOffset();

    /// <summary>
    /// перезарядка со звуком, сброс отдачи, без времени
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerator Reload();

    /// <summary>
    /// тряска камеры при выстреле
    /// </summary>
    /// <returns>секунды задержки должны быть меньше кд между выстрелами </returns>
    public abstract IEnumerator ShakeCamera();

    /// <summary>
    /// звук выстрела из оружия
    /// </summary>
    /// <returns></returns>
   public abstract IEnumerator SoundShoot();

    /// <summary>
    /// кидает рейкаст, создаёт декаль, обновляет кд
    /// </summary>
    public abstract void Shoot();

    /// <summary>
    /// толкает предмет в который стреляешь
    /// </summary>
    /// <param name="hit">передаёт рейкаст</param>
    public abstract void ForceHit(RaycastHit hit);

    /// <summary>
    /// оставляет декаль после выстрела
    /// </summary>
    /// <param name="hit">передаёт рейкаст</param>
    public abstract void SetDecal(RaycastHit hit);

    /// <summary>
    /// скинуть оружие G
    /// </summary>
    public abstract void DropGun();
}
