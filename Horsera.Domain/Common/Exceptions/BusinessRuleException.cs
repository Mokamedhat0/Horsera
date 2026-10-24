namespace Horsera.Domain.Common.Exceptions;

public sealed class BusinessRuleException(string message) : Exception(message)
{
}
