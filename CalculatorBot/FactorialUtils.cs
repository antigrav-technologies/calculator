namespace CalculatorBot;

internal static class FactorialUtils {
    public static ulong Factorial(ulong n) {
        ulong m = 1;
        for (ulong i = 2; i <= n; i++) m *= i;
        return m;
    }
}