using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterStat stat;
    public float moveSpeed;
    public float attackSpeed;
    public Transform target;

    public virtual void Init()
    {
        stat = GetComponent<CharacterStat>();
    }
}
