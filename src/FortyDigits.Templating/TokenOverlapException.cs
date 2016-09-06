using System;
using System.Runtime.Serialization;

namespace FortyDigits.Templating
{
    [Serializable]
    public class TokenOverlapException : Exception
    {
        private const string DefaultMessage = "Tokens may not overlap each other within the string.";

        public TokenOverlapException() : this(DefaultMessage)
        {
        }

        public TokenOverlapException(string message) : base(message)
        {
        }

        public TokenOverlapException(int firstTokenIndex, string firstTokenValue, int secondTokenIndex, string secondTokenValue) : 
            base(string.Format(
                "{0} Token \"{1}\" at position {2} is overlapping token \"{3}\" at position {4}.",
                DefaultMessage,
                secondTokenValue,
                secondTokenIndex,
                firstTokenValue,
                firstTokenIndex))

        {
        }
    }
}