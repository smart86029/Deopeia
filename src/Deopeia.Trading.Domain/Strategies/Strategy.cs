using System.Data;
using System.Text.RegularExpressions;
using Deopeia.Common.Extensions;
using Deopeia.Trading.Domain.Orders;

namespace Deopeia.Trading.Domain.Strategies;

public partial class Strategy : AggregateRoot<StrategyId>, ILocalizable<StrategyLocale, StrategyId>
{
    private readonly EntityLocaleCollection<StrategyLocale, StrategyId> _locales = [];
    private readonly List<StrategyLeg> _legs = [];

    private Strategy() { }

    public Strategy(
        string name,
        string? description,
        bool isEnabled,
        string openExpression,
        string closeExpression
    )
    {
        ValidateExpression(openExpression);
        ValidateExpression(closeExpression);

        _locales.Default.UpdateName(name);
        _locales.Default.UpdateDescription(description);
        IsEnabled = isEnabled;
        OpenExpression = openExpression;
        CloseExpression = closeExpression;
    }

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Description => _locales[CultureInfo.CurrentCulture]?.Description;

    public bool IsEnabled { get; private set; }

    public string OpenExpression { get; private set; } = string.Empty;

    public string CloseExpression { get; private set; } = string.Empty;

    public IReadOnlyCollection<StrategyLocale> Locales => _locales.AsReadOnly();

    public IReadOnlyCollection<StrategyLeg> Legs => _legs.AsReadOnly();

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }

    public void UpdateDescription(string? description, CultureInfo culture)
    {
        _locales[culture].UpdateDescription(description);
    }

    public void Enable()
    {
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = false;
    }

    public void RemoveLocales(IEnumerable<StrategyLocale> locales)
    {
        _locales.Remove(locales);
    }

    public void AddLeg(OrderSide side, decimal ticks, decimal volume)
    {
        _legs.Add(new StrategyLeg(Id, _legs.Count + 1, side, ticks, volume));
    }

    private void ValidateExpression(string expression)
    {
        expression.MustNotBeNullOrWhiteSpace();

        var match = InequalityRegex().Match(expression);
        if (!match.Success)
        {
            throw new DomainException("Strategy.Expression");
        }
    }

    private void AA(string expression)
    {
        // 定義變數及其對應的值
        var parameters = new Dictionary<string, double>() { { "{1}", 4 }, { "{2}", 5 } };

        // 替換不等式中的變數
        foreach (var parameter in parameters)
        {
            expression = expression.Replace(parameter.Key, parameter.Value.ToString());
        }

        var match = InequalityRegex().Match(expression);
        if (!match.Success)
        {
            throw new Exception("B");
        }

        var leftValue = Evaluate(match.Groups[1].Value);
        var operatorSymbol = match.Groups[2].Value;
        var rightValue = match.Groups[3].Value.ToDecimal();
        var result = operatorSymbol switch
        {
            ">" => leftValue > rightValue,
            "<" => leftValue < rightValue,
            ">=" => leftValue >= rightValue,
            "<=" => leftValue <= rightValue,
            _ => throw new Exception("C"),
        };

        static decimal Evaluate(string expression)
        {
            return new DataTable().Compute(expression, string.Empty).ToDecimal();
        }
    }

    [GeneratedRegex(@"^([{\d}\.\s+-/*\(\)]+)([<>]=?)\s*(\d+)$")]
    public static partial Regex InequalityRegex();
}
