using System.Text.RegularExpressions;

namespace TesteTecnicoIdealSoft.WPF.Extensions;
public static class StringExtensions
{
    public static string CleanCharacters(this string value) 
        => Regex.Replace(value, @"[^\d]", "");
}
