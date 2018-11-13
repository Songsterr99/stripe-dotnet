namespace StripeTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Stripe;
    using Xunit;

#if NETCOREAPP
    public class AllStripeObjectClassesPresentInDictionary
    {
        protected static readonly Type[] AllowedExceptions =
        {
            // This is a generic type that is handled separately
            typeof(StripeList<>),

            // TODO: replace RecipientActiveAccount with BankAccount in the next major release
            typeof(RecipientActiveAccount),
        };

        // Checks that all Stripe object classes (i.e. model classes that implement IHasObject)
        // have an entry in the Stripe.Util.ObjectsToTypes dictionary.
        [Fact]
        public void Check()
        {
            List<string> results = new List<string>();

            // Get all classes that implement IHasObject
            var type = typeof(IHasObject);
            var assembly = type.GetTypeInfo().Assembly;
            var modelClasses = assembly.DefinedTypes
                .Where(t => t.IsClass && t.ImplementedInterfaces.Contains(type))
                .Select(t => t.AsType());

            foreach (Type modelClass in modelClasses)
            {
                // Skip types present in dictionary
                if (Stripe.Infrastructure.Util.ObjectsToTypes.Any(kv => kv.Value == modelClass))
                {
                    continue;
                }

                // Skip whitelisted types
                if (AllowedExceptions.Contains(modelClass))
                {
                    continue;
                }

                results.Add(modelClass.Name);
            }

            if (results.Any())
            {
                // Sort results alphabetically
                results = results.OrderBy(i => i).ToList();

                // Display our own error message (because Assert.Empty truncates the results)
                Console.WriteLine("Found IHasObject classes not present in ObjectsToTypes dictionary:");
                foreach (string item in results)
                {
                    Console.WriteLine($"- {item}");
                }

                // Actually fail test
                Assert.True(false, "Found at least one model class missing in ObjectsToTypes dictionary");
            }
        }
    }
#endif
}
