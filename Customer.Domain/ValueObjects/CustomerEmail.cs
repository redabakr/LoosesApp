using System.Globalization;
using System.Text.RegularExpressions;
using Customer.Domain.Exceptions;

namespace Customer.Domain.ValueObjects;

public record CustomerEmail
{
    public string Value { get; }
    public CustomerEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !IsValidEmail(value))
        {
            throw new InvalidCustomerEmailException(value);
        }
        Value = value;
    }
    
    public static implicit operator string(CustomerEmail email) => email.Value;
    public static implicit operator CustomerEmail(string value) => new(value);

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();
                // Pull out and process domain name (throws ArgumentException on invalid)
                var domainName = idn.GetAscii(match.Groups[2].Value);
                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            return false;
        }
        catch (ArgumentException e)
        {
            return false;
        }
        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }  
    }
}