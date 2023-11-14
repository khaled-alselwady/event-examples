using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CounterWithThresholdEventArgs : EventArgs
{
    public int CurrentNumber { get; }

    public CounterWithThresholdEventArgs(int currentNumber)
    {
        this.CurrentNumber = currentNumber;
    }
}

public class Counter
{
    public event EventHandler<CounterWithThresholdEventArgs> CounterChanged;

    private int _Counter;
    private int _Threshold;

    public Counter(int threshold)
    {
        this._Threshold = threshold;
    }

    public void Increment()
    {
        _Counter++;

        if (_Counter % _Threshold == 0)
        {
            OnCounterChanged(_Counter);
        }
    }

    private void OnCounterChanged(int CurrentNumber)
    {
        OnCounterChanged(new CounterWithThresholdEventArgs(CurrentNumber));
    }

    protected void OnCounterChanged(CounterWithThresholdEventArgs e)
    {
        CounterChanged?.Invoke(this, e);
    }

}

public class Display
{
    public void Subscribe(Counter counter)
    {
        counter.CounterChanged += HandleCounterChanged;
    }

    private void HandleCounterChanged(object sender, CounterWithThresholdEventArgs e)
    {
        Console.WriteLine($"\n\nThe counter reached to the Threshold and the current number is {e.CurrentNumber}");
    }
}

public class clsCounterWithThreshold
{
    static void Main(string[] args)
    {
        Counter counter = new Counter(threshold: 2);
        Display display = new Display();

        display.Subscribe(counter);

        counter.Increment();
        counter.Increment();
        counter.Increment();
        counter.Increment();
        

        Console.ReadKey();
    }
}


