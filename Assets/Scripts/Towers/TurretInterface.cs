using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TurretInterface
{
    // Called through animation events on `.anim` files to make the turret fire.
    void Shoot();
}
