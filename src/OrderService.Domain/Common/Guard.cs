namespace OrderService.Domain.Common;

public class Guard
{
    public static void Assert<TException>(bool isRuleViolated) where TException : Exception, new()
    {
        if (isRuleViolated) throw new TException();
    }

    public static void Assert<TException>(bool isRuleViolated, TException exception) where TException : Exception, new()
    {
        if (isRuleViolated) throw exception;
    }
}