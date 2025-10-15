using UnityEngine;

[CreateAssetMenu(fileName = "NewFishData", menuName = "Scriptable Objects/FishData")]
public class FishData : ScriptableObject
{
    [Header("Visual")]
    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite => sprite;

    [Header("Movement")]
    [SerializeField]
    private float swimSpeed;
    public float SwimSpeed => swimSpeed;
    [Tooltip("How fast the fish can change the direction")]
    [SerializeField]
    private float turnSpeed;
    public float TurnSpeed => turnSpeed;
}
