using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace ShouldBe
{
    internal static class ShouldBeCoreExtensions
    {
        internal static T2 AssertAwesomely<T, T2>(
            this T actual, 
            IResolveConstraint specifiedConstraint, 
            T2 originalActual, 
            T2 originalExpected)
        {
            IConstraint constraint = specifiedConstraint.Resolve();
            if (!constraint.ApplyTo(actual).IsSuccess)
            {
                // TODO: We should probably be using NUnit's constraint.Description() to prepare the fail message 
                // to better integrate with NUnit and leaverage all the useful messages it generates.
                
                ShouldBeMessage.Fail(originalActual, originalExpected);
            }

            return originalActual;
        }
    }
}
