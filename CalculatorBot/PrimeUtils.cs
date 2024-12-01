namespace CalculatorBot;

internal static class PrimeUtils {
    public static bool IsPrime(ulong n) {
        if (n < 2) return false;
        for (ulong x = 2; x * x <= n; x++) {
            if (n % x == 0) return false;
        }
        return true;
    }
    
    public static List<ulong> GeneratePrimes(int n) {
        List<ulong> f = [];
        ulong i = 2;
        while (f.Count != n) {
            if (IsPrime(i)) f.Add(i);
            i++;
        }
        return f;
    }
    
    public static List<ulong> Factors(ulong n) {
        List<ulong> f = [];
        for (ulong x = 2; x * x <= n; x++) {
            while (n % x == 0) {
                f.Add(x);
                n /= x;
            }
        }
        if (n > 1) f.Add(n);
        return f;
    }
    
    public static List<ulong> Divisors(ulong n) {
        List<ulong> f = [];
        for (ulong x = 1; x <= n; x++) {
            if (n % x == 0) {
                f.Add(x);
            }
        }
        return f;
    }
    
    public static ulong SigmaFunction(ulong n) => Divisors(n).Aggregate<ulong, ulong>(0, (current, x) => current + x);

    public static ulong NthPrime(uint n) {
        int count = 0;
        ulong num = 2;
        while (true) {
            if (IsPrime(num) && ++count == n) return num;
            num++;
        }
    }
    
    public static bool IsPseudoprime(ulong n) {
        return Factors(n).Count == 2;
    }
}
/*
static Random random = new();
        private static string RandomValue() {
            int value = random.Next(1, 14);
            switch (value) {
                case 1: return "Ace";
                case 11: return "Jack";
                case 12: return "Queen";
                case 13: return "King";
                default: return value.ToString();
            }
        }

        private static string RandomSuit() {
            switch (random.Next(1, 5)) {
                case 1: return "Spades";
                case 2: return "Hearts";
                case 3: return "Clubs";
                default: return "Diamonds";
            }
        }

        public static string RandomCard() {
            return RandomValue() + " of " + RandomSuit();
        }
    }*/
