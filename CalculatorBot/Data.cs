using System.Globalization;

namespace CalculatorBot;

internal static class Data {
    private static readonly char[] SUPERSCRIPT = ['⁰', '¹', '²', '³', '⁴', '⁵', '⁶', '⁷', '⁸', '⁹'];
    
    // ⁰¹²³⁴⁵⁶⁷⁸⁹
    public static string ToSuperscript(this double num) => string.Join("",
        num.ToString(CultureInfo.InvariantCulture)
            .Replace(',', '.')
            .Select(c => c is >= '0' and <= '9' ? SUPERSCRIPT[c - '0'] : '∙'));
}