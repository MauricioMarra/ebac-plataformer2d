using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerConfig")]
public class SOPlayerConfig : ScriptableObject
{
    [Header("Movement")]
    public float speed;
    public float frictionSpeed;
    public float runningSpeedFactor;

    [Header("Jump")]
    public float jumpForce;
    public float jumpSpeed;
}
