using System.Drawing;

namespace RootKit.Drawings.BasicFilters
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public interface IFilter
    {
        Image ExecuteFilter(Image inputImage);
    }
}
