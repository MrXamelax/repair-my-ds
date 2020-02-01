using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions {

    public static Vector3 Round(this Vector3 vec) {
        vec.x = Mathf.Round(vec.x);
        vec.z = Mathf.Round(vec.z);

        return vec;
    }

}
