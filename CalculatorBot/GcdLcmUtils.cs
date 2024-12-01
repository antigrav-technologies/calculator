namespace CalculatorBot;

internal static class GcdLcmUtils {
    public static ulong Gcd(ulong a, ulong b) {
        while (a != b) {
            if (a > b) a = a - b;
            else b = b - a;
        }
        return a;
    }

    public static ulong Lcm(ulong a, ulong b) => a / Gcd(a, b) * b;
}