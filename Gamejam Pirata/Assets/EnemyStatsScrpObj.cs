using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsScrpObj", menuName = "Scriptable Objects/EnemyStatsScrpObj")]
public class EnemyStatsScrpObj : ScriptableObject
{
    public int life;
    public float speed;
    public float howManyWillWalk;
}
