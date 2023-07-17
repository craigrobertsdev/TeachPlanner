namespace Domain.Abstractions;

public interface IEither<TLeft, TRight>
{
    T Match<T>(Func<TLeft, T> left, Func<TRight, T> right);
}
