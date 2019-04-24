using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Validation
{
    public static class Ensure
    {
        //IsNull();
        public static void IsNull<T>(this T subject, string? message = null)
            where T : class
        {
            IsNull<ArgumentNullException>(subject, message);
        }

        public static void IsNull<TException>(this object subject, string? message = null)
            where TException : Exception
        {
            subject.If(e => e != null, typeof(TException), message);
        }

        //IsNotNull();
        public static void IsNotNull<T>(this T subject, string? message = null)
            where T : class
        {
            IsNotNull<ArgumentException>(subject, message);
        }

        public static void IsNotNull<TException>(this object subject, string? message = null)
            where TException : Exception
        {
            subject.If(e => e is null, typeof(TException), message);
        }

        //Equal(object);
        public static void Equal<T>(this T subject, T @object, string? message = null)
            where T : class
        {
            Equal<ArgumentException>(subject, @object, message);
        }

        public static void Equal<TException>(this object subject, object @object, string? message = null)
            where TException : Exception
        {
            subject.If(e => e != @object, typeof(TException), message);
        }

        //NotEqual(object);
        public static void NotEqual<T>(this T subject, T @object, string? message = null)
            where T : class
        {
            NotEqual<ArgumentException>(subject, @object, message);
        }

        public static void NotEqual<TException>(this object subject, object @object, string? message = null)
            where TException : Exception
        {
            subject.If(e => e == @object, typeof(TException), message);
        }

        //Is(Func<bool>);
        public static void Is<T>(this T subject, Func<T, bool> condition, string? message = null)
            where T : class
        {
            Is<ArgumentException>(subject, e => condition((T)e), message);
        }

        public static void Is<TException>(this object subject, Func<object, bool> accept, string? message = null)
            where TException : Exception
        {
            subject.If(e => !accept.Invoke(e), typeof(TException), message);
        }

        //IsEmpty();
        public static void IsEmpty(this string subject, string? message = null)
        {
            IsEmpty<ArgumentException>(subject, message);
        }

        public static void IsEmpty<TException>(this string subject, string? message = null)
            where TException : Exception
        {
            subject.If(e => e != string.Empty, typeof(TException), message);
        }

        //IsNotEmpty();
        public static void IsNotEmpty(this string subject, string? message = null)
        {
            IsNotEmpty<ArgumentException>(subject, message);
        }

        public static void IsNotEmpty<TException>(this string subject, string? message = null)
            where TException : Exception
        {
            subject.If(e => e == string.Empty, typeof(TException), message);
        }

        //IsNullOrEmpty();
        public static void IsNullOrEmpty(this string subject, string? message = null)
        {
            IsNullOrEmpty<ArgumentException>(subject, message);
        }

        public static void IsNullOrEmpty<TException>(this string subject, string? message = null)
            where TException : Exception
        {
            subject.If(e => !string.IsNullOrEmpty(e), typeof(TException), message);
        }

        //IsNotNullOrEmpty();
        public static void IsNotNullOrEmpty(this string subject, string? message = null)
        {
            IsNotNullOrEmpty<ArgumentException>(subject, message);
        }

        public static void IsNotNullOrEmpty<TException>(this string subject, string? message = null)
            where TException : Exception
        {
            subject.If(e => string.IsNullOrEmpty(e), typeof(TException), message);
        }

        //IsNullOrWhiteSpace();
        public static void IsNullOrWhiteSpace(this string subject, string? message = null)
        {
            IsNullOrWhiteSpace<ArgumentException>(subject, message);
        }

        public static void IsNullOrWhiteSpace<TException>(this string subject, string? message = null)
            where TException : Exception
        {
            subject.If(e => !string.IsNullOrWhiteSpace(e), typeof(TException), message);
        }

        //IsNotNullOrWhiteSpace();
        public static void IsNotNullOrWhiteSpace(this string subject, string? message = null)
        {
            IsNotNullOrWhiteSpace<ArgumentException>(subject, message);
        }

        public static void IsNotNullOrWhiteSpace<TException>(this string subject, string? message = null)
            where TException : Exception
        {
            subject.If(e => string.IsNullOrWhiteSpace(e), typeof(TException), message);
        }

        //IsZeroOrAbove();
        public static void IsZeroOrAbove(this int subject, string? message = null)
        {
            IsZeroOrAbove<ArgumentException>(subject, message);
        }

        public static void IsZeroOrAbove<TException>(this int subject, string? message = null)
            where TException : Exception
        {
            IsZeroOrAbove<TException>((double)subject, message);
        }

        public static void IsZeroOrAbove(this long subject, string? message = null)
        {
            IsZeroOrAbove<ArgumentException>(subject, message);
        }

        public static void IsZeroOrAbove<TException>(this long subject, string? message = null)
            where TException : Exception
        {
            IsZeroOrAbove<TException>((double)subject, message);
        }

        public static void IsZeroOrAbove(this float subject, string? message = null)
        {
            IsZeroOrAbove<ArgumentException>(subject, message);
        }

        public static void IsZeroOrAbove<TException>(this float subject, string? message = null)
            where TException : Exception
        {
            IsZeroOrAbove<TException>((double)subject, message);
        }

        public static void IsZeroOrAbove(this double subject, string? message = null)
        {
            IsZeroOrAbove<ArgumentException>(subject, message);
        }

        public static void IsZeroOrAbove<TException>(this double subject, string? message = null)
            where TException : Exception
        {
            subject.If(e => subject < 0, typeof(TException), message);
        }

        //IsZeroOrBelow();
        public static void IsZeroOrBelow(this double subject, string? message = null)
        {
            IsZeroOrBelow<ArgumentException>(subject, message);
        }

        public static void IsZeroOrBelow<TException>(this double subject, string? message = null)
            where TException : Exception
        {
            subject.If(e => subject > 0, typeof(TException), message);
        }

        public static void IsZeroOrBelow(this float subject, string? message = null)
        {
            IsZeroOrBelow<ArgumentException>(subject, message);
        }

        public static void IsZeroOrBelow<TException>(this float subject, string? message = null)
            where TException : Exception
        {
            IsZeroOrBelow<TException>((double)subject, message);
        }

        public static void IsZeroOrBelow(this long subject, string? message = null)
        {
            IsZeroOrBelow<ArgumentException>(subject, message);
        }

        public static void IsZeroOrBelow<TException>(this long subject, string? message = null)
            where TException : Exception
        {
            IsZeroOrBelow<TException>((double)subject, message);
        }

        public static void IsZeroOrBelow(this int subject, string? message = null)
        {
            IsZeroOrBelow<ArgumentException>(subject, message);
        }

        public static void IsZeroOrBelow<TException>(this int subject, string? message = null)
            where TException : Exception
        {
            subject.If(e => subject > 0, typeof(TException), message);
        }

        //IsAboveZero();
        public static void IsAboveZero(this double subject, string? message = null)
        {
            IsAboveZero<ArgumentException>(subject, message);
        }

        public static void IsAboveZero<TException>(this double subject, string? message = null)
            where TException : Exception
        {
            subject.If(e => subject <= 0, typeof(TException), message);
        }

        public static void IsAboveZero(this int subject, string? message = null)
        {
            IsAboveZero<ArgumentException>(subject, message);
        }

        public static void IsAboveZero<TException>(this int subject, string? message = null)
            where TException : Exception
        {
            IsAboveZero<TException>((double)subject, message);
        }

        public static void IsAboveZero(this float subject, string? message = null)
        {
            IsAboveZero<ArgumentException>(subject, message);
        }

        public static void IsAboveZero<TException>(this float subject, string? message = null)
            where TException : Exception
        {
            IsAboveZero<TException>((double)subject, message);
        }

        public static void IsAboveZero(this long subject, string? message = null)
        {
            IsAboveZero<ArgumentException>(subject, message);
        }

        public static void IsAboveZero<TException>(this long subject, string? message = null)
            where TException : Exception
        {
            IsAboveZero<TException>((double)subject, message);
        }

        //IsBelowZero();
        public static void IsBelowZero(this int subject, string? message = null)
        {
            IsBelowZero<ArgumentException>(subject, message);
        }

        public static void IsBelowZero<TException>(this int subject, string? message = null)
            where TException : Exception
        {
            IsBelowZero<ArgumentException>((double)subject, message);
        }

        public static void IsBelowZero(this long subject, string? message = null)
        {
            IsBelowZero<ArgumentException>(subject, message);
        }

        public static void IsBelowZero<TException>(this long subject, string? message = null)
            where TException : Exception
        {
            IsBelowZero<ArgumentException>((double)subject, message);
        }

        public static void IsBelowZero(this float subject, string? message = null)
        {
            IsBelowZero<ArgumentException>(subject, message);
        }

        public static void IsBelowZero<TException>(this float subject, string? message = null)
            where TException : Exception
        {
            IsBelowZero<ArgumentException>((double)subject, message);
        }

        public static void IsBelowZero(this double subject, string? message = null)
        {
            IsBelowZero<ArgumentException>(subject, message);
        }

        public static void IsBelowZero<TException>(this double subject, string? message = null)
            where TException : Exception
        {
            subject.If(e => subject >= 0, typeof(TException), message);
        }

        // Assisting methods
        private static void If<TResult>(
            this TResult subject,
            Func<TResult, bool> rejectCondition,
            Type type,
            string? message)
        {
            if (rejectCondition(subject))
            {
                if (message is null)
                {
                    throw ExceptionFrom(type);
                }
                else
                {
                    throw ExceptionWithMessageFrom(type, message);
                }
            }
        }

        private static Exception ExceptionFrom(Type type)
        {
            return (Exception)Activator.CreateInstance(type);
        }

        private static Exception ExceptionWithMessageFrom(Type type, string message)
        {
            return (Exception)Activator.CreateInstance(type, new string[] { message });
        }
    }
}
