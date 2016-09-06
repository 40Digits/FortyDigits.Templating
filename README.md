# FortyDigits.Templating

## Overview

FortyDigits.Templating is a client library targeting .NET 4.5 and above that aims to provide extremely fast string replacement 
in string templates that will be reused many times. When the template instance is created, it will generated a DynamicMethod to 
replace the tokens that were passed to the parser. The cost of generating a template with a render method that produces IL code 
at runtime is expensive, but when you reuse this same instance many times throughout your application it will quickly pay off.

The performance benefits of this library will be most noticeable when you are reusing the template many, many times. Some 
potential use cases are:

- Generating a large number of emails with tokens for your user data
- Anywhere you apply a large list of data to a string template
- Anywhere where the string template does not change frequently and new data will continually be rendered to that same template

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
