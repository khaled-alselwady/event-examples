using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StockPriceChangedEventArgs : EventArgs
{
    public double OldPrice { get; }
    public double NewPrice { get; }
    public double Difference { get; }

    public StockPriceChangedEventArgs(double oldPrice, double newPrice)
    {
        this.OldPrice = oldPrice;
        this.NewPrice = newPrice;
        this.Difference = newPrice - oldPrice;
    }
}

public class Stocks
{
    public event EventHandler<StockPriceChangedEventArgs> StockPriceChanged;

    private double _OldPrice;
    private double _CurrentPrice;

    public void SetStockPrice(double NewPrice)
    {
        if (_CurrentPrice != NewPrice)
        {
            _OldPrice = _CurrentPrice;
            _CurrentPrice = NewPrice;

            OnStockPriceChanged(_OldPrice, _CurrentPrice);
        }
    }

    private void OnStockPriceChanged(double OldPrice, double NewPrice)
    {
        OnStockPriceChanged(new StockPriceChangedEventArgs(OldPrice, NewPrice));
    }

    protected void OnStockPriceChanged(StockPriceChangedEventArgs e)
    {
        StockPriceChanged?.Invoke(this, e);
    }
}

public class Display
{
    public void Subscribe(Stocks stocks)
    {
        stocks.StockPriceChanged += HandleStockPriceChanged;
    }

    private void HandleStockPriceChanged(object sender, StockPriceChangedEventArgs e)
    {
        Console.WriteLine("\n\nStock Price changed:");
        Console.WriteLine($"Price changed from {e.OldPrice}$");
        Console.WriteLine($"Price changed from  {e.NewPrice}$");
        Console.WriteLine($"Price Difference to {e.Difference}$");
    }
}

public class clsStockPriceTracker
{
    static void Main()
    {
        Stocks stocks = new Stocks();
        Display display = new Display();

        display.Subscribe(stocks);

        stocks.SetStockPrice(100);
        stocks.SetStockPrice(120);
        stocks.SetStockPrice(120);

        Console.ReadKey();
    }
}

