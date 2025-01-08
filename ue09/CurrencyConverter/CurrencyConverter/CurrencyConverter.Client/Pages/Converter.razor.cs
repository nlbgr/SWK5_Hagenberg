using CurrencyConverter.Blazor.Client.Services.Generated;
using Microsoft.AspNetCore.Components;

namespace CurrencyConverter.Client.Pages;

public partial class Converter
{
	const int DECIMALS = 5;
	enum Direction { SOURCE_TO_TARGET, TARGET_TO_SOURCE };

	private IEnumerable<(string, string)>? currencyData;

	private string sourceSymbol = "EUR";
	private string targetSymbol = "USD";

	private decimal sourceValue;
	private decimal targetValue;
	private decimal exchangeRate;

	[Inject] private IConverterService ConverterService { get; set; } = null!;

	protected override async Task OnInitializedAsync()
	{
		if (currencyData is null)
		{
			var currencyList = (await ConverterService.GetAllAsync()).Result;
			currencyData = currencyList.Select(cd => (cd.Symbol, $"{cd.Name} ({cd.Country})"));
			exchangeRate = (await ConverterService.RateOfExchangeAsync(sourceSymbol, targetSymbol)).Result;
		}
	}

	private void HandleSourceInput(ChangeEventArgs e)
	{
        if (decimal.TryParse(e.Value?.ToString(), out decimal newValue))
        {
            sourceValue = newValue;
			Convert(Direction.SOURCE_TO_TARGET);
        }
	}
	private void HandleTargetInput(ChangeEventArgs e)
	{
        if (decimal.TryParse(e.Value?.ToString(), out decimal newValue))
        {
            targetValue = newValue;
            Convert(Direction.TARGET_TO_SOURCE);
        }
    }

	private async Task HandleSourceCurrencyChanged(string newValue)
    {
        sourceSymbol = newValue;
        await RefreshExchangeRate();
		Convert(Direction.SOURCE_TO_TARGET);
    }

	private async Task HandleTargetCurrencyChanged(string newValue)
	{
        sourceSymbol = newValue;
        await RefreshExchangeRate();
        Convert(Direction.TARGET_TO_SOURCE);
    }

	private async Task RefreshExchangeRate()
	{
        exchangeRate = (await ConverterService.RateOfExchangeAsync(sourceSymbol, targetSymbol)).Result;
    }

	private void Convert(Direction direction)
	{
		if (direction == Direction.SOURCE_TO_TARGET)
		{
			targetValue = Math.Round(sourceValue * exchangeRate, DECIMALS);
		}
		else if (direction == Direction.TARGET_TO_SOURCE)
		{
			sourceValue = Math.Round(targetValue / exchangeRate, DECIMALS);
		}
	}
}
