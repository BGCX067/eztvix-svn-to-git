using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace EzTvix.Provider
{
    public class Movies : IDictionary<int, Movie>
    {


        private Dictionary<int, Movie> dico = new Dictionary<int, Movie>();

        #region *** iDictionnary Members ***
        // ...
        // Summary:
        //     Gets an System.Collections.Generic.ICollection<T> containing the keys of
        //     the System.Collections.Generic.IDictionary<TKey,TValue>.
        //
        // Returns:
        //     An System.Collections.Generic.ICollection<T> containing the keys of the object
        //     that implements System.Collections.Generic.IDictionary<TKey,TValue>.
        public ICollection<int> Keys { get { return dico.Keys; } }
        //
        // Summary:
        //     Gets an System.Collections.Generic.ICollection<T> containing the values in
        //     the System.Collections.Generic.IDictionary<TKey,TValue>.
        //
        // Returns:
        //     An System.Collections.Generic.ICollection<T> containing the values in the
        //     object that implements System.Collections.Generic.IDictionary<TKey,TValue>.
        public ICollection<Movie> Values { get { return dico.Values; } }

        // Summary:
        //     Gets or sets the element with the specified key.
        //
        // Parameters:
        //   key:
        //     The key of the element to get or set.
        //
        // Returns:
        //     The element with the specified key.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     key is null.
        //
        //   System.Collections.Generic.KeyNotFoundException:
        //     The property is retrieved and key is not found.
        //
        //   System.NotSupportedException:
        //     The property is set and the System.Collections.Generic.IDictionary<TKey,TValue>
        //     is read-only.
        public Movie this[int key]
        {
            get { return dico[key]; }
            set { dico[key] = value; }
        }
        // Summary:
        //     Adds an element with the provided key and value to the System.Collections.Generic.IDictionary<TKey,TValue>.
        //
        // Parameters:
        //   key:
        //     The object to use as the key of the element to add.
        //
        //   value:
        //     The object to use as the value of the element to add.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     key is null.
        //
        //   System.ArgumentException:
        //     An element with the same key already exists in the System.Collections.Generic.IDictionary<TKey,TValue>.
        //
        //   System.NotSupportedException:
        //     The System.Collections.Generic.IDictionary<TKey,TValue> is read-only.
        public void Add(int key, Movie value)
        {
            dico.Add(key, value);
        }
        //
        // Summary:
        //     Determines whether the System.Collections.Generic.IDictionary<TKey,TValue>
        //     contains an element with the specified key.
        //
        // Parameters:
        //   key:
        //     The key to locate in the System.Collections.Generic.IDictionary<TKey,TValue>.
        //
        // Returns:
        //     true if the System.Collections.Generic.IDictionary<TKey,TValue> contains
        //     an element with the key; otherwise, false.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     key is null.
        public bool ContainsKey(int key)
        {
            return dico.ContainsKey(key);
        }
        //
        // Summary:
        //     Removes the element with the specified key from the System.Collections.Generic.IDictionary<TKey,TValue>.
        //
        // Parameters:
        //   key:
        //     The key of the element to remove.
        //
        // Returns:
        //     true if the element is successfully removed; otherwise, false. This method
        //     also returns false if key was not found in the original System.Collections.Generic.IDictionary<TKey,TValue>.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     key is null.
        //
        //   System.NotSupportedException:
        //     The System.Collections.Generic.IDictionary<TKey,TValue> is read-only.
        public bool Remove(int key)
        {
            return dico.Remove(key);
        }
        //
        // Summary:
        //     Gets the value associated with the specified key.
        //
        // Parameters:
        //   key:
        //     The key whose value to get.
        //
        //   value:
        //     When this method returns, the value associated with the specified key, if
        //     the key is found; otherwise, the default value for the type of the value
        //     parameter. This parameter is passed uninitialized.
        //
        // Returns:
        //     true if the object that implements System.Collections.Generic.IDictionary<TKey,TValue>
        //     contains an element with the specified key; otherwise, false.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     key is null.
        public bool TryGetValue(int key, out Movie value)
        {
            return dico.TryGetValue(key, out value);
        }
        #endregion
        #region *** iCollection ***
        // Summary:
        //     Gets the number of elements contained in the System.Collections.Generic.ICollection<T>.
        //
        // Returns:
        //     The number of elements contained in the System.Collections.Generic.ICollection<T>.
        public int Count { get { return dico.Count(); } }
        //
        // Summary:
        //     Gets a value indicating whether the System.Collections.Generic.ICollection<T>
        //     is read-only.
        //
        // Returns:
        //     true if the System.Collections.Generic.ICollection<T> is read-only; otherwise,
        //     false.
        public bool IsReadOnly { get { return false; } }

        // Summary:
        //     Adds an item to the System.Collections.Generic.ICollection<T>.
        //
        // Parameters:
        //   item:
        //     The object to add to the System.Collections.Generic.ICollection<T>.
        //
        // Exceptions:
        //   System.NotSupportedException:
        //     The System.Collections.Generic.ICollection<T> is read-only.
        public void Add(KeyValuePair<int, Movie> item)
        {
            //ICollection<KeyValuePair<TKey, TValue>>
            dico.Add(item.Key, item.Value);
        }
        //
        // Summary:
        //     Removes all items from the System.Collections.Generic.ICollection<T>.
        //
        // Exceptions:
        //   System.NotSupportedException:
        //     The System.Collections.Generic.ICollection<T> is read-only.
        public void Clear()
        {
            dico.Clear();
        }
        //
        // Summary:
        //     Determines whether the System.Collections.Generic.ICollection<T> contains
        //     a specific value.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.ICollection<T>.
        //
        // Returns:
        //     true if item is found in the System.Collections.Generic.ICollection<T>; otherwise,
        //     false.
        public bool Contains(KeyValuePair<int, Movie> item)
        {
            //ICollection<KeyValuePair<TKey, TValue>>
            return dico.ContainsKey(item.Key);
        }
        //
        // Summary:
        //     Copies the elements of the System.Collections.Generic.ICollection<T> to an
        //     System.Array, starting at a particular System.Array index.
        //
        // Parameters:
        //   array:
        //     The one-dimensional System.Array that is the destination of the elements
        //     copied from System.Collections.Generic.ICollection<T>. The System.Array must
        //     have zero-based indexing.
        //
        //   arrayIndex:
        //     The zero-based index in array at which copying begins.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     array is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     arrayIndex is less than 0.
        //
        //   System.ArgumentException:
        //     array is multidimensional.-or-The number of elements in the source System.Collections.Generic.ICollection<T>
        //     is greater than the available space from arrayIndex to the end of the destination
        //     array.-or-Type T cannot be cast automatically to the type of the destination
        //     array.
        public void CopyTo(KeyValuePair<int, Movie>[] array, int arrayIndex)
        {
            //
        }
        //
        // Summary:
        //     Removes the first occurrence of a specific object from the System.Collections.Generic.ICollection<T>.
        //
        // Parameters:
        //   item:
        //     The object to remove from the System.Collections.Generic.ICollection<T>.
        //
        // Returns:
        //     true if item was successfully removed from the System.Collections.Generic.ICollection<T>;
        //     otherwise, false. This method also returns false if item is not found in
        //     the original System.Collections.Generic.ICollection<T>.
        //
        // Exceptions:
        //   System.NotSupportedException:
        //     The System.Collections.Generic.ICollection<T> is read-only.
        public bool Remove(KeyValuePair<int, Movie> item)
        {
            return dico.Remove(item.Key);
        }

        #endregion
        #region *** IEnumerable<T> Members ***
        public IEnumerator<KeyValuePair<int, Movie>> GetEnumerator()
        {
            return dico.GetEnumerator();
        }
        #endregion
        #region *** IEnumerable Members ***
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return dico.GetEnumerator();
        }
        #endregion

        //public ImageList imageList
        //{
        //    get
        //    {
        //        ImageList imgList = new ImageList();
        //        imgList.ImageSize = new Size(70, 100);
        //        imgList.ColorDepth = ColorDepth.Depth32Bit;

        //        foreach (KeyValuePair<int, Movie> kvp in dico)
        //        {
        //            try
        //            {
        //                //imgList.Images.Add(kvp.Value.Moviepictures[0].Thumb);
        //                imgList.Images.Add(kvp.Value.ThumbCover);
        //            }
        //            catch (Exception ex)
        //            {
        //                imgList.Images.Add(global::RootKit.Drawings.Images.Alpha);
        //            }
        //        }

        //        return imgList;
        //    }
        //}

        public ListViewItem[] getListView()
        {
            //List<ListViewItem> itemList = new List<ListViewItem> ();
            string Description;
            int i = 0;
            ListViewItem itemToAdd;
            ListViewItem[] itemList = new ListViewItem[this.Count];

            foreach (KeyValuePair<int, Movie> kvp in dico)
            {
                Description = kvp.Value.Title + " - (" + kvp.Value.Title + ") - " + kvp.Value.Year
                    + "\r\nRéalisateur : " + kvp.Value.Directors[0].ToString();

                itemToAdd = new ListViewItem(kvp.Value.ID.ToString());//newMovieList[i].OriginalTitle);

                itemToAdd.ImageIndex = i;

                itemToAdd.SubItems.Add(Description);

                itemList[i] = itemToAdd;
                i++;
            }
            i = 0;


            return itemList;

        }

        public bool Contains(Movie item)
        {
            //ICollection<KeyValuePair<TKey, TValue>>
            return dico.ContainsKey(item.ID);
        }
        public void Add(Movie value)
        {
            dico.Add(value.ID, value);
        }
        public bool Remove(Movie value)
        {
            if (this.Contains(value))
                return dico.Remove(value.ID);
            return true;
        }

    }
}
