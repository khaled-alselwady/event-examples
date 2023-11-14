using System;

public class TemperatureChangedEventArgs : EventArgs
{
    public double OldTemperature { get; }
    public double NewTemperature { get; }
    public double Difference { get; }

    public TemperatureChangedEventArgs(double oldTemperature, double newTemperature)
    {
        this.OldTemperature = oldTemperature;
        this.NewTemperature = newTemperature;
        this.Difference = newTemperature - oldTemperature;
    }
}

public class Thermostat
{
    public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged;

    private double _OldTemperature;
    private double _CurrentTemperature;

    public void SetTemperature(double NewTemperature)
    {
        if (_CurrentTemperature != NewTemperature)
        {
            _OldTemperature = _CurrentTemperature;
            _CurrentTemperature = NewTemperature;
            OnTemperatureChanged(_OldTemperature, _CurrentTemperature);
        }
    }

    private void OnTemperatureChanged(double OldTemperature, double NewTemperature)
    {
        OnTemperatureChanged(new TemperatureChangedEventArgs(OldTemperature, NewTemperature));
    }

    protected void OnTemperatureChanged(TemperatureChangedEventArgs e)
    {
        TemperatureChanged?.Invoke(this, e);
    }

}

public class Display
{
    public void Subscribe(Thermostat thermostat)
    {
        thermostat.TemperatureChanged += HandleTemperatureChanged;
    }

    private void HandleTemperatureChanged(object sender, TemperatureChangedEventArgs e)
    {
        Console.WriteLine("\n\nTemperature changed:");
        Console.WriteLine($"Temperature changed from {e.OldTemperature}°C");
        Console.WriteLine($"Temperature changed to {e.NewTemperature}°C");
        Console.WriteLine($"Temperature Difference to {e.Difference}°C");
    }
}

public class clsTemperatureChanged
{
    static void Main()
    {
        Thermostat thermostat = new Thermostat();
        Display display = new Display();

        display.Subscribe(thermostat);

        thermostat.SetTemperature(25);
        thermostat.SetTemperature(30);
        thermostat.SetTemperature(30);
        thermostat.SetTemperature(30);
        thermostat.SetTemperature(30);
        thermostat.SetTemperature(30);
        thermostat.SetTemperature(30);

        Console.ReadLine();

    }
}
