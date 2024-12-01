namespace CalculatorBot;

internal static class FibonacciUtils {
    public static ulong[] FibArray(ulong n) {
        ulong[] f = new ulong[n];
        if (n > 1) f[0] = 0;
        if (n > 2) f[1] = 1;
        for (ulong i = 2; i < n; i++) {
            f[i] = f[i - 1] + f[i - 2];
        }

        return f;
    }
}