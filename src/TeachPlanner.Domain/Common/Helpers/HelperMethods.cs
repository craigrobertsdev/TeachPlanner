namespace TeachPlanner.Domain.Common.Helpers;
public static class HelperMethods
{
    public static bool ListsContainSameElements<T>(List<T> list1, List<T> list2) where T : IEquatable<T>
    {
        if (list1.Count != list2.Count)
        {
            return false;
        }

        list1.Sort();
        list2.Sort();

        return list1.SequenceEqual(list2);
    }
}
