using Domain.Abstractions;

namespace Application.Common;

public class Either<TLeft, TRight> : IEither<TLeft, TRight>
{
    private readonly TLeft _left = default!;
    private readonly TRight _right = default!;
    private readonly bool _isLeft;

    public Either(TLeft left)
    {
        _left = left;
        _isLeft = true;
    }

    public Either(TRight right)
    {
        _right = right;
        _isLeft = false;
    }

    public T Match<T>(Func<TLeft, T> left, Func<TRight, T> right)
    {
        return _isLeft ? left(_left) : right(_right);
    }
}
