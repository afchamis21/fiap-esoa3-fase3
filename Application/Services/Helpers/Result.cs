namespace Fiap.Agnello.CLI.Application.Services.Helpers
{

    internal class Result
    {
        public readonly BussinessError? Error;
        public bool Success => Error == null;

        private Result(BussinessError? error)
        {
            Error = error;
        }

        public Result IfSuccess(Action action)
        {
            if (!Success)
            {
                return this;
            }

            action();

            return this;
        }

        public Result IfError(Action<BussinessError> action)
        {
            if (Success)
            {
                return this;
            }

            if (Error is null)
            {
                throw new NullReferenceException(nameof(Error));
            }

            action(Error);

            return this;
        }

        public static Result Ok() => new(null);
        public static Result Fail(BussinessError error) => new(error);
    }
    internal class Result<T>
    {
        public readonly T? Value;
        public readonly BussinessError? Error;
        public bool Success => Error == null;

        private Result(T? value, BussinessError? error)
        {
            Value = value;
            Error = error;
        }

        public Result<T> IfSuccess(Action<T> action)
        {
            if (!Success)
            {
                return this;
            }

            if (Value is null)
            {
                throw new NullReferenceException(nameof(Value));
            }

            action(Value);

            return this;
        }

        public Result<T> IfError(Action<BussinessError> action)
        {
            if (Success)
            {
                return this;
            }

            if (Error is null)
            {
                throw new NullReferenceException(nameof(Error));
            }

            action(Error);

            return this;
        }

        public T GetOrThrow()
        {
            if (Value == null)
            {
                throw new NullReferenceException(nameof(Value));
            }

            return Value;
        }

        public T GetOrElse(T defaultValue)
        {
            if (Value == null)
            {
                return defaultValue;
            }

            return Value;
        }


        public static Result<T> Ok(T value) => new(value, null);
        public static Result<T> Fail(BussinessError error) => new(default, error);
    }
}
