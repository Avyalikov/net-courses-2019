//-----------------------------------------------------------------------
// <copyright file="TestClass.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RoslynTestingApp
{
    /// <summary>
    /// test test
    /// </summary>
    public class TestClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestClass" /> class
        /// </summary>
        public TestClass()
        {
        }
         
        /// <summary>
        /// This method does something
        /// </summary>
        /// <returns>sample string</returns>
        public string GetProperty()
        {
            return "A";
        }

        /// <summary>
        /// This internal method does another things
        /// </summary>
        /// <returns>sample string 2</returns>
        private string GetPropertyInternal()
        {
            return "A";
        }
    }
}
