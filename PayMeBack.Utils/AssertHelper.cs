using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Xunit;

namespace PayMeBack.Utils
{
    public static class AssertHelper
    {
        public static void AssertAreAllFieldsEqual<T>(T expected, T actual, ICollection<string> excludeFields = null) where T : class
        {
            if (excludeFields == null)
            {
                excludeFields = new List<string>();
            }

            if (expected == null && actual == null)
            {
                return;
            }
            else if (expected == null || actual == null)
            {
                Assert.True(false, $"Parameter 'expected'({ expected }) and 'actual' ({ actual }) should be identical.");
            }

            Type type = typeof(T);

            if (type.GetProperties().Length < 1)
            {
                Assert.True(false, $"No property can be compared for '{type.Name}'.");
            }

            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                if (propertyInfo.CanRead && !(excludeFields.Contains(propertyInfo.Name)))
                {
                    object expectedValue = propertyInfo.GetValue(expected);
                    object actualValue = propertyInfo.GetValue(actual);
                    if (propertyInfo.PropertyType.GetTypeInfo().IsClass && propertyInfo.PropertyType != typeof(string))
                    {
                        MethodInfo assertAreAllFieldsEqualMethod = typeof(AssertHelper).GetMethod(nameof(AssertAreAllFieldsEqual));
                        MethodInfo assertAreAllFieldsEqualMethodWithType = assertAreAllFieldsEqualMethod.MakeGenericMethod(new Type[] { propertyInfo.PropertyType });
                        try
                        {
                            assertAreAllFieldsEqualMethodWithType.Invoke(null, new object[] { expectedValue, actualValue, excludeFields });
                        }
                        catch (TargetInvocationException ex)
                        {
                            throw ex.InnerException;
                        }
                    }
                    else
                    {
                        Assert.True(object.Equals(expectedValue, actualValue), $"The values of the property '{propertyInfo.Name}' ('{ expectedValue }' and '{ actualValue }') should be equal.");
                    }
                }
            }
        }

        public static void AssertAreAllFieldsEqualInList<T>(IList<T> expecteds, IList<T> actuals) where T : class
        {
            AssertAreAllFieldsEqualInList(expecteds, actuals, new Collection<string>());
        }

        public static void AssertAreAllFieldsEqualInList<T>(IList<T> expecteds, IList<T> actuals, ICollection<string> excludeFields) where T : class
        {
            if (expecteds == null)
            {
                Assert.True(false, "Parameter 'expecteds' should not be null.");
            }
            if (actuals == null)
            {
                Assert.True(false, "Parameter 'actuals' should not be null.");
            }

            int expectedsCount = expecteds.Count;
            int actualsCount = actuals.Count;
            Assert.True(expectedsCount == actualsCount, $"The count of actual ({actualsCount}) should be the same as the count of the expected ({expectedsCount}).");

            for (var i = 0; i < expecteds.Count; i++)
            {
                AssertAreAllFieldsEqual(expecteds[i], actuals[i], excludeFields);
            }
        }
    }
}