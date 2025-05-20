namespace exception.Utils;

public class IsNullEmptyOrWhiteSpace
{
    public static bool Check(string val)
    {
        if (string.IsNullOrEmpty(val.Trim())) return true;
        return false;
    }
}