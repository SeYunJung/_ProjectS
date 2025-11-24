public class Hero : CombatController
{
    public float level {  get; private set; }

    public override void Init()
    {
        base.Init();
    }

    public void Init(float speed)
    {
        base.Init();
        attackSpeed += speed;
        level = 1.0f;
    }

    public void SetLevel(float level)
    {
        this.level += level;
    }
}
