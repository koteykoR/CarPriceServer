
namespace MiddlewareLibrary
{
    public sealed class Either<TR, TE> where TE : Error
    {
        public TR Result { get; private set; }

        public TE Error { get; private set; }

        public bool HasError => Error is not null;

        public Either(TR result, TE error) => (Result, Error) = (result, error);
    }
}
