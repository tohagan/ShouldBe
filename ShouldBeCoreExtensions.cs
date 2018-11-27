using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace ShouldBe
{
    internal static class ShouldBeCoreExtensions
    {
        internal static T2 AssertAwesomely<T, T2>(
            this T actual, 
            IConstraint specifiedConstraint, 
            T2 originalActual, 
            T2 originalExpected)
        {
            var constraint = specifiedConstraint.Resolve();
            var result = constraint.ApplyTo(actual);

            if (!result.IsSuccess)
            {
                // TODO: We should probably be using NUnit's constraint.WriteMessageTo() to prepare the fail message 
                // to better integrate with NUnit and leverage all the useful messages it generates.

                ShouldBeMessage.Fail(originalActual, originalExpected);
            }

            return originalActual;
        }
    }
}
