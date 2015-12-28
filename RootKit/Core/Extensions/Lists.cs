using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RootKit.Core
{
    //public partial class Extensions 
    public static partial class Extensions
    {
        /// <summary>
        /// transform a list of Ators into a list of strings
        /// </summary>
        /// <typeparam name="Actor">Type of the enumerable containet objet</typeparam>
        /// <param name="enumerable">The enum</param>
        /// <param name="separator">The separator for the Join method</param>
        /// <returns>a string that concatenate the actors name</returns>
        public static string Join<Actor>(this IEnumerable<Actor> enumerable, string separator)
        {
            // nécessite l'override de la méthode ToString dans l'objet Actors
            return string.Join(separator, enumerable);
        } 

    }
}
