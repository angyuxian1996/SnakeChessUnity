﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour {

    void Update() {
        transform.rotation = Camera.main.transform.rotation;
    }
}