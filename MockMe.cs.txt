using Rhino.Mocks;

namespace ShouldBe
{
    /// <summary>
    /// 
    /// </summary>
    public static class MockMe
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Stub<T>() where T : class
        {
            return MockRepository.GenerateStub<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="constructorArgs"></param>
        /// <returns></returns>
        public static T Partial<T>(params object[] constructorArgs) where T : class
        {
            return new MockRepository().PartialMock<T>(constructorArgs);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Mock<T>() where T : class
        {
            return MockRepository.GenerateMock<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DynamicMockWithRemoting<T>() where T : class
        {
            return MockRepository.GenerateDynamicMockWithRemoting<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T StrictMock<T>() where T : class
        {
            return MockRepository.GenerateStrictMock<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T StrictMockWithRemoting<T>() where T : class
        {
            return MockRepository.GenerateStrictMockWithRemoting<T>();
        }
    }
}