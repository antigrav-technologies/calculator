namespace CalculatorBot;

internal static class XorShift64 {
    private static ulong _state;

    static XorShift64() {
        _state = (ulong)new Random().Next(int.MaxValue) * int.MaxValue + (ulong)new Random().Next(int.MaxValue);
    }

    public static ulong Next() {
        _state ^= _state << 7;
        _state ^= _state >> 9;
        return _state;
    }

    public static int NextInt32() {
        return (int)Next();
    }

    public static int NextInt32(int a, int b) {
        return a + NextInt32() % (b - a);
    }
}