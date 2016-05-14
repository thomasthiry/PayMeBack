using System.Collections.Generic;
using Xunit;

namespace PayMeBack.Utils.Tests
{
    public class AssertHelperTests
    {
        private const string AssertFailMissingMessage = "The assert helper should have failed.";

        [Fact]
        public void AssertAreAllFieldsEqual_SimpleIdenticalObjects_True()
        {
            var obj1 = new { Prop1 = "prop1", Prop2 = "prop2" };
            var obj2 = new { Prop1 = "prop1", Prop2 = "prop2" };

            AssertHelper.AssertAreAllFieldsEqual(obj2, obj1);
        }

        [Fact]
        public void AssertAreAllFieldsEqual_SimpleDifferentObjects_False()
        {
            var obj1 = new { Prop1 = "prop1", Prop2 = "prop2" };
            var obj2 = new { Prop1 = "prop99", Prop2 = "prop2" };

            try
            {
                AssertHelper.AssertAreAllFieldsEqual(obj2, obj1);
                Assert.True(false, AssertFailMissingMessage);
            }
            catch (Xunit.Sdk.TrueException ex) when (!ex.Message.Contains(AssertFailMissingMessage))
            {
                // The assert helper has thrown an assert fail, as expected. Note that we don't want to catch the Assert.Fail() that we throw ourselves.
            }
        }

        [Fact]
        public void AssertAreAllFieldsEqual_Compare2Null_True()
        {
            string obj1 = null;
            string obj2 = null;

            AssertHelper.AssertAreAllFieldsEqual(obj2, obj1);
        }

        [Fact]
        public void AssertAreAllFieldsEqual_OneStringOnlyIsNull_False()
        {
            string obj1 = null;
            string obj2 = "hello";

            try
            {
                AssertHelper.AssertAreAllFieldsEqual(obj2, obj1);
                Assert.True(false, AssertFailMissingMessage);
            }
            catch (Xunit.Sdk.TrueException ex) when (ex.Message.Contains(obj2))
            {
                // The assert helper has thrown an assert fail, as expected. Note that we don't want to catch the Assert.Fail() that we throw ourselves.
            }
        }

        [Fact]
        public void AssertAreAllFieldsEqual_OneObjectOnlyIsNull_False()
        {
            GenericObject obj1 = null;
            GenericObject obj2 = new GenericObject();

            try
            {
                AssertHelper.AssertAreAllFieldsEqual(obj2, obj1);
                Assert.True(false, AssertFailMissingMessage);
            }
            catch (Xunit.Sdk.TrueException ex) when (ex.Message.Contains("GenericObject"))
            {
                // The assert helper has thrown an assert fail, as expected. Note that we don't want to catch the Assert.Fail() that we throw ourselves.
            }
        }

        [Fact]
        public void AssertAreAllFieldsEqual_IdenticalObjectsWithIntProperty_True()
        {
            var obj1 = new GenericObjectWithInt { Prop1 = 5 };
            var obj2 = new GenericObjectWithInt { Prop1 = 5 };

            AssertHelper.AssertAreAllFieldsEqual(obj2, obj1);
        }

        [Fact]
        public void AssertAreAllFieldsEqual_DifferentObjectsWithIntProperty_False()
        {
            var obj1 = new GenericObjectWithInt { Prop1 = 5 };
            var obj2 = new GenericObjectWithInt { Prop1 = 614893564 };

            try
            {
                AssertHelper.AssertAreAllFieldsEqual(obj2, obj1);
                Assert.True(false, AssertFailMissingMessage);
            }
            catch (Xunit.Sdk.TrueException ex) when (ex.Message.Contains(obj2.Prop1.ToString()))
            {
                // The assert helper has thrown an assert fail, as expected. Note that we don't want to catch the Assert.Fail() that we throw ourselves.
            }
        }

        [Fact]
        public void AssertAreAllFieldsEqual_IdenticalObjectsWithNull_True()
        {
            var obj1 = new GenericObject { Prop1 = null, Prop2 = null };
            var obj2 = new GenericObject { Prop1 = null, Prop2 = null };

            AssertHelper.AssertAreAllFieldsEqual(obj2, obj1);
        }

        [Fact]
        public void AssertAreAllFieldsEqual_DifferentObjectsWithNull_False()
        {
            var obj1 = new GenericObject { Prop1 = null, Prop2 = "ItShouldNotBeEqual" };
            var obj2 = new GenericObject { Prop1 = null, Prop2 = null };

            try
            {
                AssertHelper.AssertAreAllFieldsEqual(obj2, obj1);
                Assert.True(false, AssertFailMissingMessage);
            }
            catch (Xunit.Sdk.TrueException ex) when (ex.Message.Contains(obj1.Prop2.ToString()))
            {
                // The assert helper has thrown an assert fail, as expected. Note that we don't want to catch the Assert.Fail() that we throw ourselves.
            }
        }

        [Fact]
        public void AssertAreAllFieldsEqual_OneDifferenceExcludedObjects_True()
        {
            var obj1 = new { Prop1 = "prop1", Prop2 = "prop2" };
            var obj2 = new { Prop1 = "prop99", Prop2 = "prop2" };

            AssertHelper.AssertAreAllFieldsEqual(obj2, obj1, new[] { "Prop1" });
        }

        [Fact]
        public void AssertAreAllFieldsEqual_NestedIdenticalObjects_True()
        {
            var mainObj1 = new NestingGenericObject
            {
                Nested1 = new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                Nested2 = new GenericObject { Prop1 = "prop3", Prop2 = "prop4" }
            };
            var mainObj2 = new NestingGenericObject
            {
                Nested1 = new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                Nested2 = new GenericObject { Prop1 = "prop3", Prop2 = "prop4" }
            };

            AssertHelper.AssertAreAllFieldsEqual(mainObj2, mainObj1);
        }

        [Fact]
        public void AssertAreAllFieldsEqual_NestedDifferentObjects_False()
        {
            var mainObj1 = new NestingGenericObject
            {
                Nested1 = new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                Nested2 = new GenericObject { Prop1 = "prop3", Prop2 = "prop4" }
            };
            var mainObj2 = new NestingGenericObject
            {
                Nested1 = new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                Nested2 = new GenericObject { Prop1 = "prop999", Prop2 = "prop4" }
            };

            try
            {
                AssertHelper.AssertAreAllFieldsEqual(mainObj1, mainObj2);
                Assert.True(false, AssertFailMissingMessage);
            }
            catch (Xunit.Sdk.TrueException ex) when (!ex.Message.Contains(AssertFailMissingMessage))
            {
                // The assert helper has thrown an assert fail, as expected. Note that we don't want to catch the Assert.Fail() that we throw ourselves.
            }
        }

        [Fact]
        public void AssertAreAllFieldsEqualInList_SimpleIdenticalLists_True()
        {
            var list1 = new List<GenericObject> {
                new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
            };
            var list2 = new List<GenericObject> {
                new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
            };

            AssertHelper.AssertAreAllFieldsEqualInList(list1, list2);
        }

        [Fact]
        public void AssertAreAllFieldsEqualInList_SimpleDifferentLists_False()
        {
            var list1 = new List<GenericObject> {
                new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                new GenericObject { Prop1 = "prop9999", Prop2 = "prop2" },
            };
            var list2 = new List<GenericObject> {
                new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
            };

            try
            {
                AssertHelper.AssertAreAllFieldsEqualInList(list1, list2);
                Assert.True(false, AssertFailMissingMessage);
            }
            catch (Xunit.Sdk.TrueException ex) when (!ex.Message.Contains(AssertFailMissingMessage))
            {
                // The assert helper has thrown an assert fail, as expected. Note that we don't want to catch the Assert.Fail() that we throw ourselves.
            }
        }

        [Fact]
        public void AssertAreAllFieldsEqualInList_OneExcludedDifferenceInLists_True()
        {
            var list1 = new List<GenericObject> {
                new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                new GenericObject { Prop1 = "prop99", Prop2 = "prop2" },
            };
            var list2 = new List<GenericObject> {
                new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
            };

            AssertHelper.AssertAreAllFieldsEqualInList(list1, list2, excludeFields: new[] { "Prop1" });
        }

        [Fact]
        public void AssertAreAllFieldsEqualInList_NestedIdenticalObjects_True()
        {
            var list1 = new List<NestingGenericObject> {
                new NestingGenericObject
                {
                    Nested1 = new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                    Nested2 = new GenericObject { Prop1 = "prop3", Prop2 = "prop4" }
                },
                new NestingGenericObject
                {
                    Nested1 = new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                    Nested2 = new GenericObject { Prop1 = "prop3", Prop2 = "prop4" }
                }
            };
            var list2 = new List<NestingGenericObject> {
                new NestingGenericObject
                {
                    Nested1 = new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                    Nested2 = new GenericObject { Prop1 = "prop3", Prop2 = "prop4" }
                },
                new NestingGenericObject
                {
                    Nested1 = new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                    Nested2 = new GenericObject { Prop1 = "prop3", Prop2 = "prop4" }
                }
            };

            AssertHelper.AssertAreAllFieldsEqualInList(list1, list2);
        }

        [Fact]
        public void AssertAreAllFieldsEqualInList_NestedDifferentObjects_False()
        {
            var list1 = new List<NestingGenericObject> {
                new NestingGenericObject
                {
                    Nested1 = new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                    Nested2 = new GenericObject { Prop1 = "prop3", Prop2 = "prop4" }
                },
                new NestingGenericObject
                {
                    Nested1 = new GenericObject { Prop1 = "prop1", Prop2 = "prop999" },
                    Nested2 = new GenericObject { Prop1 = "prop3", Prop2 = "prop4" }
                }
            };
            var list2 = new List<NestingGenericObject> {
                new NestingGenericObject
                {
                    Nested1 = new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                    Nested2 = new GenericObject { Prop1 = "prop3", Prop2 = "prop4" }
                },
                new NestingGenericObject
                {
                    Nested1 = new GenericObject { Prop1 = "prop1", Prop2 = "prop2" },
                    Nested2 = new GenericObject { Prop1 = "prop3", Prop2 = "prop4" }
                }
            };

            try
            {
                AssertHelper.AssertAreAllFieldsEqualInList(list1, list2);
                Assert.True(false, AssertFailMissingMessage);
            }
            catch (Xunit.Sdk.TrueException ex) when (!ex.Message.Contains(AssertFailMissingMessage))
            {
                // The assert helper has thrown an assert fail, as expected. Note that we don't want to catch the Assert.Fail() that we throw ourselves.
            }
        }

        internal class GenericObject
        {
            public string Prop1 { get; set; }
            public string Prop2 { get; set; }
        }

        internal class GenericObjectWithInt
        {
            public int Prop1 { get; set; }
        }

        internal class NestingGenericObject
        {
            public GenericObject Nested1 { get; set; }
            public GenericObject Nested2 { get; set; }
        }

        internal class NestingListAndGenericObject
        {
            public IEnumerable<GenericObject> List1 { get; set; }
            public GenericObject Nested1 { get; set; }
        }
    }
}
