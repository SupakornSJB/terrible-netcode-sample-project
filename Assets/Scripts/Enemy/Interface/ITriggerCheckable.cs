public interface ITriggerCheckable 
{
    bool isAggroed { get; set; }
    bool isWithinStrikingDistance { get; set; }
    void setAggroedState(bool isAggroed);
    void setStrikingDistancBool(bool isWithinStrikingDistance);
}
