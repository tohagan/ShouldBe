using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;
using ShouldBe.DifferenceHighlighting;

namespace ShouldBe
{
    /// <summary/>
    public class AssertionStackFrameInfo
    {
        /// <summary/>
        public bool CanReadSourceCode { get; set; }
        /// <summary/>
        public string ShouldMethod { get; set; }
        /// <summary/>
        public string FileName { get; set; }
        /// <summary/>
        public int LineNumber { get; set; }
        /// <summary/>
        public int ColumnNumber { get; set; }
    }

    /// <summary>
    /// Helper class for creating assertion failure messages.
    /// </summary>
    /// <remarks>
    /// Fails using NUnit's <see cref="Assert.Fail(string)"/> method 
    /// to ensure that ShouldBe works with tools like ReSharper 
    /// that are designed to integrate with NUnit.
    /// </remarks>
    internal static class ShouldBeMessage
    {
        // replaced when testing using TestHelper
        internal static Action<string> OnFail = Assert.Fail;

        internal static bool TestHelper
        {
            get => OnFail == Assert.Fail;
            set => OnFail = value ? ThrowAssertException : (Action<string>)Assert.Fail;
        }

        private static void ThrowAssertException(string msg)
        {
            throw new AssertionException(msg);
        }

        public static void FailActualIfNull<T>(T actual)
        {
            if (actual == null)
            {
                OnFail(GetMessageActualIsNull());
            }
        }

        public static void FailActual<T>(T actual)
        {
            OnFail(GetMessageActual(actual));
        }

        public static void FailExpecting<T>(T expected)
        {
            OnFail(GetMessageExpecting(expected));
        }

        public static void FailExpectingElement<T>(T expectedElement)
        {
            OnFail(GetMessageExpectingElement(expectedElement));
        }

        public static void FailExpectingFormatted(string expected)
        {
            OnFail(GetMessageExpectingFormatted(expected));
        }

        public static void Fail<T>(T actual, T expected, string context = null)
        {
            OnFail(GetMessage(actual, expected, context));
        }

        public static void Fail<T>(IEnumerable<T> actual, T expected, T tolerance)
        {
            OnFail(GetMessageWithTolerance(actual, expected, tolerance));
        }

        public static void Fail<T>(IEnumerable<T> actual, T expected, string context = null)
        {
            OnFail(GetMessage(actual, expected, context));
        }

        public static string GetMessage<T1, T2>(T1 actual, T2 expected, string context = null)
        {
            string message = string.Format("{0}\n    {1}\n        but was\n    {2}",
                context ?? GetContext(), expected.Inspect(), actual.Inspect());

            if (actual.CanGenerateDifferencesBetween(expected))
            {
                message += string.Format("\n        difference\n    {0}",
                actual.HighlightDifferencesBetween(expected));
            }

            return message;
        }

        public static string GetMessageWithTolerance<T>(IEnumerable<T> actual, T expected, T tolerance)
        {
            return string.Format("{0}\n    {1} (+/- {2})\n        but was\n    {3}",
                GetContext(), expected, tolerance, actual.Inspect());
        }
        
        public static string GetMessageActual<T>(T actual)
        {
            string context = GetContext();
            return string.Format("{0} but was\n    {1}", context, actual.Inspect());
        }

        public static string GetMessageActualIsNull()
        {
            string context = GetContext(true);
            return string.Format("{0} should not be null", context);
        }

        public static string GetMessageExpecting<T>(T expected)
        {
            string context = GetContext();
            return string.Format("{0}\n    {1}", context, expected.Inspect());
        }

        public static string GetMessageExpectingFormatted(string expected)
        {
            string context = GetContext();
            return string.Format("{0}\n    {1}", context, expected);
        }

        public static string GetMessageExpectingElement<T>(T expectedElement)
        {
            string context = GetContext();
            return string.Format("{0} an element satisfying the condition\n    {1}",
                context, expectedElement.Inspect());
        }

        public static string GetContext()
        {
            return GetContext(false);
        }

        private static string GetContext(bool codeOnly)
        {
            AssertionStackFrameInfo assertionStackFrame = GetStackFrameForOriginatingTestMethod();
            var codePart = "The provided expression";

            // Extract "actual" expression from source code using stackframeInfo
            if (assertionStackFrame.CanReadSourceCode)
            {
                var possibleCodeLines = File.ReadAllLines(assertionStackFrame.FileName)
                                            .Skip(assertionStackFrame.LineNumber).ToArray();
                var codeLines = possibleCodeLines.DelimitWith("\n");

                var shouldMethodIndex = codeLines.IndexOf(assertionStackFrame.ShouldMethod);
                codePart = shouldMethodIndex > -1 ?
                    codeLines.Substring(0, shouldMethodIndex - 1).Trim() :
                    possibleCodeLines[0];

                if (codePart.StartsWith("() => ")) codePart = codePart.Substring(6);
            }

            if (codeOnly)
            {
                return codePart;
            }
            else
            {
                string assertDesc = assertionStackFrame.ShouldMethod.Replace("Async", "").CamelCasedToSpaced();
                return string.Format("{0}\n  {1}", codePart, assertDesc);
            }
        }

        //private static TestEnvironment GetStackFrameForOriginatingTestMethod1()
        //{
        //    var stackTrace = new StackTrace(true);
        //    StackFrame[] frames = stackTrace.GetFrames();           
        //    if (frames == null) throw new Exception("Unable to find test method");

        //    // Filter out asyc, system or other library stack frames.
        //    frames = frames.Where(frm => frm.GetFileName() != null).ToArray();
        //    if (frames.Length == 0) throw new Exception("Unable to find test method. No stack frames available. Run unit tests with a debug build.");

        //    var i = 0;
        //    var shouldbeFrame = frames[i];
        //    if (shouldbeFrame == null) throw new Exception("Unable to find test method");

        //    while (!shouldbeFrame.GetMethod().DeclaringType.GetCustomAttributes(typeof(ShouldBeMethodsAttribute), true).Any())
        //    {
        //        shouldbeFrame = frames[++i];
        //    }

        //    var originatingFrame = frames[i + 1];

        //    string sourceFile = originatingFrame.GetFileName();

        //    return new TestEnvironment
        //        {
        //            CanReadSourceCode = sourceFile != null && File.Exists(sourceFile),
        //            ShouldMethod = shouldbeFrame.GetMethod().Name,
        //            FileName = sourceFile,
        //            LineNumber = originatingFrame.GetFileLineNumber() - 1
        //        };
        //}
    
        private static AssertionStackFrameInfo GetStackFrameForOriginatingTestMethod()
        {
            var stackTrace = new StackTrace(true);

            StackFrame[] frames = stackTrace.GetFrames();
            if (frames == null) throw new Exception("Unable to find test method. No stack frames available. Run unit tests with a debug build.");

            // Filter out asyc, system or other library stack frames.
            //frames = frames.Where(frm => frm.GetFileName() != null).ToArray();

            if (frames.Length == 0) throw new Exception("Unable to find test method. No stack frames available. Run unit tests with a debug build.");

            StackFrame frame;
            MethodBase method;
            string assertionMethodName = null;
            Type declType;
            int i;

            for (i = 0;
                    i < frames.Length 
                    && (frame = frames[i]) != null 
                    && (method = frame.GetMethod()) != null
                    && (declType = method.DeclaringType) != null; 
                i++)
            {
                // Find at least one StackFrame having [ShouldBeMethodsAttribute] on the method class.
                if (declType.GetCustomAttributes(typeof (ShouldBeMethodsAttribute), true).Any())
                {
                    assertionMethodName = frame.GetMethod().Name;
                }
                // THEN next StackFrame after the method class.  
                else if (assertionMethodName != null)
                {
                    // Use NUnit test fixture method as the source code for the assertion failure message
                    string sourceFile = frame.GetFileName();

                    return new AssertionStackFrameInfo
                    {
                        CanReadSourceCode = sourceFile != null && File.Exists(sourceFile),
                        ShouldMethod = assertionMethodName,
                        FileName = sourceFile,
                        LineNumber = frame.GetFileLineNumber() - 1,
                        ColumnNumber = frame.GetFileColumnNumber()
                    };
                }
            }

            throw new Exception("Unable to find test method source code. No stack available or using async calls.");
        }
    }
}