namespace Common.DataAccess;

public class NotFoundException : Exception
{
    public NotFoundException(string objectName) : base($"Not found ({objectName}).")
    {
    }
}
