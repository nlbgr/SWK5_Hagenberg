using CurrencyConverter.Domain;
using CurrencyConverter.Logic;
using Microsoft.AspNetCore.Components;

namespace CurrencyConverter.Components.Pages;

public partial class CurrencyList
{
	[Inject] private ICurrencyCalculator Logic { get; set; } = null!;

	private ICollection<CurrencyData>? currencies;

	protected override async Task OnInitializedAsync()
	{
		await Task.Delay(1000);
		currencies = [];
		foreach (var symbol in await Logic.GetCurrenciesAsync())
		{
			await Task.Delay(200);
			_ = InvokeAsync(() => StateHasChanged());
			currencies.Add(await Logic.GetCurrencyDataAsync(symbol));
		}
	}
}