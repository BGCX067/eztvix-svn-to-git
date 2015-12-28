using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace RootKit.API
{
    //public class MovieList : IList<OldMovie>
    //{
    //    private List<OldMovie> _innerList = new List<OldMovie>();
    //    private IEnumerable<OldMovie> _lazyLoader;

    //    #region Interface Members
    //    #region IList<T> Members

    //    private void ensureList()
    //    {
    //        //if (_innerList == null)
    //        //    _innerList = new List<Movie>(_lazyLoader);
    //    }
        
    //    public Movie this[int index]
    //    {
    //        get
    //        {
    //            return _innerList[index];
    //        }
    //        set
    //        {
    //            _innerList[index] = value;
    //        }
    //    }

    //    // Summary:
    //    //     Determines the index of a specific item in the System.Collections.Generic.IList<T>.
    //    //
    //    // Parameters:
    //    //   item:
    //    //     The object to locate in the System.Collections.Generic.IList<T>.
    //    //
    //    // Returns:
    //    //     The index of item if found in the list; otherwise, -1.
    //    public int IndexOf(Movie item)
    //    {
    //        ensureList();
    //        return _innerList.IndexOf(item);
    //    }
    //    //
    //    // Summary:
    //    //     Inserts an item to the System.Collections.Generic.IList<T> at the specified
    //    //     index.
    //    //
    //    // Parameters:
    //    //   index:
    //    //     The zero-based index at which item should be inserted.
    //    //
    //    //   item:
    //    //     The object to insert into the System.Collections.Generic.IList<T>.
    //    //
    //    // Exceptions:
    //    //   System.ArgumentOutOfRangeException:
    //    //     index is not a valid index in the System.Collections.Generic.IList<T>.
    //    //
    //    //   System.NotSupportedException:
    //    //     The System.Collections.Generic.IList<T> is read-only.
    //    public void Insert(int index, Movie item)
    //    {
    //        ensureList();
    //        _innerList.Insert(index, item);
    //    }
    //    //
    //    // Summary:
    //    //     Removes the System.Collections.Generic.IList<T> item at the specified index.
    //    //
    //    // Parameters:
    //    //   index:
    //    //     The zero-based index of the item to remove.
    //    //
    //    // Exceptions:
    //    //   System.ArgumentOutOfRangeException:
    //    //     index is not a valid index in the System.Collections.Generic.IList<T>.
    //    //
    //    //   System.NotSupportedException:
    //    //     The System.Collections.Generic.IList<T> is read-only.
    //    public void RemoveAt(int index)
    //    {
    //        ensureList();
    //        _innerList.RemoveAt(index);
    //    }
    //    #endregion

    //    #region IEnumerable<T> Members
    //    public IEnumerator<Movie> GetEnumerator() 
    //    {
    //        ensureList();
    //        return _innerList.GetEnumerator();
    //    }
    //    #endregion

    //    #region IEnumerable Members
    //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    //    {
    //        ensureList();
    //        return _innerList.GetEnumerator();
    //    }
    //    #endregion

    //    #region ICollection<T> Members
    //    // Summary:
    //    //     Gets the number of elements contained in the System.Collections.Generic.ICollection<T>.
    //    //
    //    // Returns:
    //    //     The number of elements contained in the System.Collections.Generic.ICollection<T>.
    //    public int Count { get { ensureList(); return _innerList.Count; } }
    //    //
    //    // Summary:
    //    //     Gets a value indicating whether the System.Collections.Generic.ICollection<T>
    //    //     is read-only.
    //    //
    //    // Returns:
    //    //     true if the System.Collections.Generic.ICollection<T> is read-only; otherwise,
    //    //     false.
    //    public bool IsReadOnly { get { return false; } }

    //    // Summary:
    //    //     Adds an item to the System.Collections.Generic.ICollection<T>.
    //    //
    //    // Parameters:
    //    //   item:
    //    //     The object to add to the System.Collections.Generic.ICollection<T>.
    //    //
    //    // Exceptions:
    //    //   System.NotSupportedException:
    //    //     The System.Collections.Generic.ICollection<T> is read-only.
    //    public void Add(Movie item)
    //    {
    //        ensureList();
    //        _innerList.Add(item);
    //    }
    //    //
    //    // Summary:
    //    //     Removes all items from the System.Collections.Generic.ICollection<T>.
    //    //
    //    // Exceptions:
    //    //   System.NotSupportedException:
    //    //     The System.Collections.Generic.ICollection<T> is read-only.
    //    public void Clear()
    //    {
    //        ensureList();
    //        _innerList.Clear();
    //    }
    //    //
    //    // Summary:
    //    //     Determines whether the System.Collections.Generic.ICollection<T> contains
    //    //     a specific value.
    //    //
    //    // Parameters:
    //    //   item:
    //    //     The object to locate in the System.Collections.Generic.ICollection<T>.
    //    //
    //    // Returns:
    //    //     true if item is found in the System.Collections.Generic.ICollection<T>; otherwise,
    //    //     false.
    //    public bool Contains(Movie item)
    //    {
    //        ensureList();
    //        return _innerList.Contains(item);
    //    }
    //    //
    //    // Summary:
    //    //     Copies the elements of the System.Collections.Generic.ICollection<T> to an
    //    //     System.Array, starting at a particular System.Array index.
    //    //
    //    // Parameters:
    //    //   array:
    //    //     The one-dimensional System.Array that is the destination of the elements
    //    //     copied from System.Collections.Generic.ICollection<T>. The System.Array must
    //    //     have zero-based indexing.
    //    //
    //    //   arrayIndex:
    //    //     The zero-based index in array at which copying begins.
    //    //
    //    // Exceptions:
    //    //   System.ArgumentNullException:
    //    //     array is null.
    //    //
    //    //   System.ArgumentOutOfRangeException:
    //    //     arrayIndex is less than 0.
    //    //
    //    //   System.ArgumentException:
    //    //     array is multidimensional.-or-The number of elements in the source System.Collections.Generic.ICollection<T>
    //    //     is greater than the available space from arrayIndex to the end of the destination
    //    //     array.-or-Type T cannot be cast automatically to the type of the destination
    //    //     array.
    //    public void CopyTo(Movie[] array, int arrayIndex)
    //    {
    //        ensureList();
    //        _innerList.CopyTo(array, arrayIndex);
    //    }
    //    //
    //    // Summary:
    //    //     Removes the first occurrence of a specific object from the System.Collections.Generic.ICollection<T>.
    //    //
    //    // Parameters:
    //    //   item:
    //    //     The object to remove from the System.Collections.Generic.ICollection<T>.
    //    //
    //    // Returns:
    //    //     true if item was successfully removed from the System.Collections.Generic.ICollection<T>;
    //    //     otherwise, false. This method also returns false if item is not found in
    //    //     the original System.Collections.Generic.ICollection<T>.
    //    //
    //    // Exceptions:
    //    //   System.NotSupportedException:
    //    //     The System.Collections.Generic.ICollection<T> is read-only.
    //    public bool Remove(Movie item)
    //    { 
    //                ensureList();
    //        return _innerList.Remove(item);

    //    }
    //    #endregion
    //    #endregion

    //    public ImageList imageList
    //    {
    //        get
    //        {
    //            ImageList toto = new ImageList();
    //            toto.ImageSize = new Size(70, 100);

    //            foreach (OldMovie mov in _innerList)
    //            {
    //                try
    //                {
    //                    toto.Images.Add(mov.Moviepictures[0].Thumb);
    //                }
    //                catch (Exception ex)
    //                {
    //                    toto.Images.Add(global::RootKit.Drawings.Images.Alpha);
    //                }
    //            }

    //            return toto;
    //        }
    //    }
    //}

}
