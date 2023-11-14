using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum enTrafficSignal { Red, Yellow, Green };

public class TrafficSignalEventArgs : EventArgs
{
    public enTrafficSignal CurrentState { get; }

    public TrafficSignalEventArgs(enTrafficSignal currentState)
    {
        this.CurrentState = currentState;
    }
}

public class TrafficSignal
{
    public event EventHandler<TrafficSignalEventArgs> StateChanged;

    private enTrafficSignal _CurrentState;

    public void ChangeState(enTrafficSignal NewState)
    {
        if (NewState != _CurrentState)
        {
            _CurrentState = NewState;

            OnStateChanged(NewState);
        }
    }

    private void OnStateChanged(enTrafficSignal NewState)
    {
        OnStateChanged(new TrafficSignalEventArgs(NewState));
    }

    protected void OnStateChanged(TrafficSignalEventArgs e)
    {
        StateChanged?.Invoke(this, e);
    }

}

public class Display
{
    public void Subscribe(TrafficSignal trafficSignal)
    {
        trafficSignal.StateChanged += HandleStateChanged;
    }

    private void HandleStateChanged(object sender, TrafficSignalEventArgs e)
    {
        Console.WriteLine($"\n\nThe Current State is {e.CurrentState}");
    }
}

public class clsTrafficSignalController
{
    static void Main(string[] args)
    {
        TrafficSignal trafficSignal = new TrafficSignal();
        Display display = new Display();

        display.Subscribe(trafficSignal);

        trafficSignal.ChangeState(enTrafficSignal.Red);
        trafficSignal.ChangeState(enTrafficSignal.Yellow);
        trafficSignal.ChangeState(enTrafficSignal.Yellow);
        trafficSignal.ChangeState(enTrafficSignal.Green);

        Console.ReadKey();
    }
}

