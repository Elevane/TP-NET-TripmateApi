using TripmateApi.Common;

namespace TripmateApi.Common
{
    /// <summary>
    public class Envelope<T>
    {
        public bool IsSuccess => String.IsNullOrEmpty(ErrorMessage);
        public Envelope() { }
        public T Result { get; set; }
        public string ErrorMessage { get; set; }


        protected internal Envelope(string errorMessage)
        {
            ErrorMessage = errorMessage;

        }

        protected internal Envelope(T result)
        {
            Result = result;
        }
    }
}

public class Envelope : Envelope<string>
{
    public Envelope(string result) : base(result)
    {
    }

    public static Envelope<T> Error<T>(string errorMessage)
    {
        return new Envelope<T>(errorMessage);
    }

    public static Envelope Error(string errorMessage)
    {
        return new Envelope(errorMessage);
    }

    public static Envelope<T> Ok<T>(T result)
    {
        return new Envelope<T>(result);
    }


    }

