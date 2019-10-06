using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraStates
{
    Player,
    ObjectSelected,
    Move
}

public interface ICameraState
{
    void entry(CameraControl camControl);
    ICameraState input(CameraControl camControl);
    void update(CameraControl camControl);
}
