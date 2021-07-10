using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLashLightController : MonoBehaviour
{

    Vector3 flashRight = new Vector3(1.92f, 0.27f);
    Vector3 flashLeft = new Vector3(-1.92f, 0.27f);
    Vector3 flashUp = new Vector3(0f, 2.02f);
    Vector3 flashDown = new Vector3(0f, -1.57f);
    Quaternion flashRightRotation = Quaternion.Euler(0f, 180f, 0f);
    Quaternion flashLeftRotation = Quaternion.Euler(0f, 0f, 0f);
    Quaternion flashUpRotation = Quaternion.Euler(0f, 0f, -90f);
    Quaternion flashDownRotation = Quaternion.Euler(0f, 0f, 90f);


    public void FlashRight() {
        transform.localPosition = flashRight;
        transform.rotation = flashRightRotation;
    }

    public void FlashLeft() {
        transform.localPosition = flashLeft;
        transform.rotation = flashLeftRotation;
    }

    public void FlashDown() {
        transform.localPosition = flashDown;
        transform.rotation = flashDownRotation;
    }
    public void FlashUp() {
        transform.localPosition = flashUp;
        transform.rotation = flashUpRotation;
    }
}