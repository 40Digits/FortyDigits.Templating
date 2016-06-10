# FortyDigits.Templating

## Overview

FortyDigits.Templating is a client library targeting .NET 4.5 and above that provides a token string replacement that is 
wicked fast using DynamicMethods. The cost of generating a template with a dynamic render method is expensive, but when you
reuse this same instance throughout your application it will quickly pay off. The SpeedTest UnitTest is a test that pulls 
in randomly generated data for 1M people into a ~14KB text template. Initial tests have shown this completing in about 8-10 
seconds.

## Usage examples

Create a template parser instance for the allowed string tokens.

```c#
var listOfTokens = new[] {"{{FirstName}}", "{{LastName}}", "{{Age}}" };
var templateParser = new TokenListParser(listOfTokens);
```

Get an instance of the template for rendering purposes.

```c#
var template = templateParser.GetTemplate("Hello, my name is {{FirstName}} {{LastName}} and I am {{Age}} years old.");
```

Pass a dictionary of token values to the template's render method to get back your rendered data. 
Reuse the same template instance over and over again for quick token replacement.

```c#
var tokenValues = new Dictionary<string, string>
{
    {"{{FirstName}}", "John"},
    {"{{LastName}}", "Doe"},
    {"{{Age}}", "29"}
};
var result = template.Render(tokenValues);
```

## Copyright and License

Copyright 2016 40Digits, LLC.

This plugin is released under the [MIT License](http://www.opensource.org/licenses/MIT).
