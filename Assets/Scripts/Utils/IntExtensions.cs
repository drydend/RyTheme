public static class IntExtensions
{
    public static string ToStringWithZeros(this int value, int minNumberOfZeros)
    {
        var stringNumber = value.ToString();

        if (stringNumber.Length < minNumberOfZeros)
        {
            string zerosSequence = "";

            for (int i = 0; i < minNumberOfZeros - stringNumber.Length; i++)
            {
                zerosSequence += "0";
            }

            stringNumber = zerosSequence + stringNumber;
        }

        return stringNumber;
    }
}
