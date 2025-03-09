namespace TCP_Server.Base_classes;

public abstract class TextBasedArgumentProtocol : Protocol
{
    public byte ExpectedArgsCount { get; protected set; }

    protected int[] FindArgumentSeperationPositions(string data)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));
        var argPoses = new int[ExpectedArgsCount];
        argPoses[0] = 0;
        int argCounter = 1;
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] == ' ')
            {
                try
                {
                    argPoses[argCounter++] = i + 1;
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new ArgumentException($"Expected {ExpectedArgsCount} arguments. " +
                        $"{argCounter} or more were given.", nameof(data), e);
                }
            }
        }
        if (argCounter != ExpectedArgsCount)
        {
            throw new ArgumentException($"Expected {ExpectedArgsCount} arguments. " +
                $"Only {argCounter} were given.", nameof(data));
        }
        return argPoses;
    }

    protected string[] SeperateArgumentsIntoArray(string data)
    {
        var args = new string[ExpectedArgsCount];
        var argPoses = FindArgumentSeperationPositions(data);
        for (int i = 0; i < ExpectedArgsCount; i++)
        {
            if (ExpectedArgsCount - 1 != i)
            {
                args[i] = data.Substring(argPoses[i], argPoses[i + 1] - 1);
            }
            else
            {
                args[i] = data.Substring(argPoses[i], data.Length - argPoses[i]);
            }
        }
        return args;
    }
}
