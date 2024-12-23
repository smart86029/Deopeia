namespace Deopeia.Trading.Domain.Contracts;

public readonly record struct VolumeRestriction(decimal Min, decimal Max, decimal Step) { }
