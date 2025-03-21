using SpacetimeDB.Types;
using Unity.VisualScripting;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    const float LERP_DURATION_SEC = 0.1f;
    private static readonly int ShaderColorProperty = Shader.PropertyToID("_Color");
    [DoNotSerialize] public uint EntityId;
    protected float LerpTime;
    protected Vector3 LerpStartPosition;
    protected Vector3 LerpTargetPosition;
    protected Vector3 TargetScale;

    protected virtual void Spawn(uint entityId)
    {
        EntityId = entityId;
        
        var entity = GameManager.Conn.Db.Entity.EntityId.Find(EntityId);
        LerpStartPosition = LerpTargetPosition = transform.position = (Vector2)entity.Position;
        transform.localScale = Vector3.one;
        TargetScale = MassToScale(entity.Mass);
    }

    public void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().material.SetColor(ShaderColorProperty, color);
    }

    public virtual void OnEntityUpdated(Entity newVal)
    {
        LerpTime = 0.0f;
        LerpStartPosition = transform.position;
        LerpTargetPosition = (Vector2)newVal.Position;
        TargetScale = MassToScale(newVal.Mass);
    }

    public virtual void OnDelete(EventContext ctx)
    {
        Destroy(gameObject);
    }

    public virtual void Update()
    {
        LerpTime = Mathf.Min(LerpTime, LerpTime + Time.deltaTime);
        transform.position = Vector3.Lerp(LerpStartPosition, LerpTargetPosition, LerpTime / LERP_DURATION_SEC);
        transform.localScale = Vector3.Lerp(transform.localScale, TargetScale, Time.deltaTime * 8);
    }

    public static Vector3 MassToScale(uint mass)
    {
        var diameter = MassToDiameter(mass);
        return new Vector3(diameter, diameter, 1);
    }

    public static float MassToRadius(uint mass) => Mathf.Sqrt(mass);
    public static float MassToDiameter(uint mass) => MassToRadius(mass) * 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


}
