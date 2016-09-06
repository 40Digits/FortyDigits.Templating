using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FortyDigits.Templating.Tests
{
    [TestClass]
    public class TokenListTests
    {
        protected readonly Dictionary<string, string> Tokens;
        protected readonly TokenListParser TemplateParser;

        public TokenListTests()
        {
            Tokens = new Dictionary<string, string>
            {
                { "quick", "slow" },
                { "fox", "cow" },
                { "lazy dog", "moon" }
            };

            TemplateParser = new TokenListParser(Tokens.Keys);
        }

        [TestMethod]
        public void TestOutput()
        {
            var testString = "The quick brown fox jumps over the lazy dog.";
            var template = TemplateParser.GetTemplate(testString);
            var result = template.Render(Tokens);

            Assert.AreEqual("The slow brown cow jumps over the moon.", result);
        }

        [TestMethod]
        public void TestStartOfStringTokenOutput()
        {
            var testString = "quick brown";
            var template = TemplateParser.GetTemplate(testString);
            var result = template.Render(Tokens);

            Assert.AreEqual("slow brown", result);
        }

        [TestMethod]
        public void TestEndOfStringTokenOutput()
        {
            var testString = "brown fox";
            var template = TemplateParser.GetTemplate(testString);
            var result = template.Render(Tokens);

            Assert.AreEqual("brown cow", result);
        }

        [TestMethod]
        public void TestStartAndEndOfStringTokenOutput()
        {
            var testString = "quick brown fox";
            var template = TemplateParser.GetTemplate(testString);
            var result = template.Render(Tokens);

            Assert.AreEqual("slow brown cow", result);
        }

        [TestMethod]
        public void TestOnlyTokenOutput()
        {
            var testString = "quick";
            var template = TemplateParser.GetTemplate(testString);
            var result = template.Render(Tokens);

            Assert.AreEqual("slow", result);
        }

        [TestMethod]
        public void TestNoTokensFound()
        {
            var testString = "The cow jumped over the moon.";
            var template = TemplateParser.GetTemplate(testString);

            Assert.AreEqual(testString, template.Render(Tokens));
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void TestNoTokensSupplied()
        {
            var noTokens = new string[0];
            var templateParser = new TokenListParser(noTokens);
        }

        [TestMethod]
        [ExpectedException(typeof (TokenOverlapException))]
        public void TestTokenInception()
        {
            var testString = "You mean, a token within a token";
            var tokens = new Dictionary<string, string>
            {
                { "token within", "dream within" },
                { "within a token", "within a dream" }
            };
            var templateParser = new TokenListParser(tokens.Keys);
            var template = templateParser.GetTemplate(testString);
        }

        [TestMethod]
        public void TestAdjoiningTokens()
        {
            var testString = "Welcome, {{Salutation}}{{LastName}}";
            var tokens = new Dictionary<string,string>
            {
                { "{{Salutation}}", "Mr." },
                { "{{LastName}}", "Fox" }
            };
            var templateParser = new TokenListParser(tokens.Keys);
            var template = templateParser.GetTemplate(testString);

            Assert.AreEqual("Welcome, Mr.Fox", template.Render(tokens));
        }

        [TestMethod]
        public void SpeedTest()
        {
            var templateString = Properties.Resources.LoremIpsum;
            var tokens = new[] {"{{FirstName}}", "{{LastName}}", "{{Email}}", "{{Color}}", "{{Music}}", "{{Sport}}"};

            var templateParser = new TokenListParser(tokens);
            var template = templateParser.GetTemplate(templateString);

            using (var context = new PeopleDataDataContext())
            {
                var people = context.Peoples;

                var elapsedTime = ExecutionTime(() =>
                {
                    foreach (var person in people)
                    {
                        var values = new Dictionary<string, string>
                        {
                            {"{{FirstName}}", person.FirstName},
                            {"{{LastName}}", person.LastName},
                            {"{{Email}}", person.Email},
                            {"{{Color}}", person.Color},
                            {"{{Music}}", person.Music},
                            {"{{Sport}}", person.Sport}
                        };

                        var result = template.Render(values);

                    }
                });

                Console.WriteLine("Executed {0} template replacements in {1} seconds", people.Count(), elapsedTime.TotalSeconds);
                Assert.IsTrue(elapsedTime < TimeSpan.FromSeconds(15));
            }
        }

        private TimeSpan ExecutionTime(Action toTime)
        {
            var timer = Stopwatch.StartNew();
            toTime();
            timer.Stop();
            return timer.Elapsed;
        }
    }
}
