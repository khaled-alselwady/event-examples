using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LightSwitchChangedEventArgs : EventArgs
{
    public bool IsLight { get; }

    public LightSwitchChangedEventArgs(bool IsLight)
    {
        this.IsLight = IsLight;
    }
}

public class LightSwitch
{
    public event EventHandler<LightSwitchChangedEventArgs> LightSwitchChanged;

    private bool _IsLight;

    public void TurnOn()
    {
        if (!_IsLight)
        {
            _IsLight = true;

            OnLightSwitchChanged(_IsLight);
        }
    }

    public void TurnOff()
    {
        if (_IsLight)
        {
            _IsLight = false;

            OnLightSwitchChanged(_IsLight);
        }
    }

    private void OnLightSwitchChanged(bool IsLight)
    {
        OnLightSwitchChanged(new LightSwitchChangedEventArgs(IsLight));
    }

    protected void OnLightSwitchChanged(LightSwitchChangedEventArgs e)
    {
        LightSwitchChanged?.Invoke(this, e);
    }

}

public class Display
{
    public void Subscribe(LightSwitch lightSwitch)
    {
        lightSwitch.LightSwitchChanged += HandleLightSwitchChanged;
    }

    private void HandleLightSwitchChanged(object sender, LightSwitchChangedEventArgs e)
    {
        Console.WriteLine("\n\nLogs:");

        string Status = e.IsLight ? "ON" : "OFF";

        Console.WriteLine($"The light is {Status} at {DateTime.Now}");
    }
}

public class clsLightSwitchController
{
    static void Main()
    {
        LightSwitch lightSwitch = new LightSwitch();
        Display display = new Display();

       display.Subscribe(lightSwitch);

        lightSwitch.TurnOn();
        lightSwitch.TurnOn();
        lightSwitch.TurnOn();
        lightSwitch.TurnOff();
        lightSwitch.TurnOn();

        Console.ReadKey();
    }
}

