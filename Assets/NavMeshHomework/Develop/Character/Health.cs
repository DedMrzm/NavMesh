public class Health
{
    private float _maxValue;

    private float _value;

    public Health(float maxHealth)
    {
        _maxValue = maxHealth;
        _value = maxHealth;
    }

    public float Value
    {
        get
        {
            return _value;  
        }
        set
        {
            if (value < 0)
                _value = 0;
            else
                _value = value;
        }
    }
}
