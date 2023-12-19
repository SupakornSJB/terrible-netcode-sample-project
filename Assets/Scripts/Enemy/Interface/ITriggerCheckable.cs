public interface ITriggerCheckable 
{
    bool isAggroed { get; set; }
    bool isWithinStrikingDistance { get; set; }
    void SetAggroedState(bool isAggroed);
    void SetStrikingDistancBool(bool isWithinStrikingDistance);
}
