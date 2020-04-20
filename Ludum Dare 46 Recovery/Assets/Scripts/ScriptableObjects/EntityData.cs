using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "EntityDataObject", menuName = "Entity/Data", order = 1)]
public class EntityData : ScriptableObject
{
    public int ID = 0;
    public EntityType eType;
    public new string name;

    public int health;
    public int maxHealth = 10;
    public float speed;

    public Vector3 position;
    public Quaternion rotation;

}
public enum EntityType
{
    Flicker,
    AzulSaliva,
    RojoRuth,
    VerdeGerard
}