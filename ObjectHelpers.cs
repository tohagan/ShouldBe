namespace ShouldBe
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectHelpers
    {
        /// <summary>
        /// Type cast helper
        /// </summary>
        /// <param name="o"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T As<T>(this object o)
        {
            return (T)o;
        }
    }
}