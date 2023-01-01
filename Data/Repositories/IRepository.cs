using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;

namespace BikeSparesInventorySystem.Data.Repositories;

internal interface IRepository<TSource> where TSource : IModel
{
	//
	// Summary:
	//     Adds an item to the System.Collections.Generic.ICollection`1.
	//
	// Parameters:
	//   item:
	//     The object to add to the System.Collections.Generic.ICollection`1.
	//
	// Exceptions:
	//   T:System.NotSupportedException:
	//     The System.Collections.Generic.ICollection`1 is read-only.
	void Add(TSource item);

	TSource Get<TKey>(Func<TSource, TKey> keySelector, TKey byValue);

	ICollection<TSource> GetAll();

	ICollection<TSource> GetAllSorted<TKey>(Func<TSource, TKey> keySelector, SortDirection direction);

	//
	// Summary:
	//     Removes the first occurrence of a specific object from the System.Collections.Generic.ICollection`1.
	//
	// Parameters:
	//   item:
	//     The object to remove from the System.Collections.Generic.ICollection`1.
	//
	// Returns:
	//     true if item was successfully removed from the System.Collections.Generic.ICollection`1;
	//     otherwise, false. This method also returns false if item is not found in the
	//     original System.Collections.Generic.ICollection`1.
	//
	// Exceptions:
	//   T:System.NotSupportedException:
	//     The System.Collections.Generic.ICollection`1 is read-only.
	bool Remove(TSource item);
	//
	// Summary:
	//     Gets the number of elements contained in the System.Collections.Generic.ICollection`1.
	//
	// Returns:
	//     The number of elements contained in the System.Collections.Generic.ICollection`1.
	int Count { get; }
	//
	// Summary:
	//     Removes all items from the System.Collections.Generic.ICollection`1.
	//
	// Exceptions:
	//   T:System.NotSupportedException:
	//     The System.Collections.Generic.ICollection`1 is read-only.
	void Clear();
	//
	// Summary:
	//     Determines whether the System.Collections.Generic.ICollection`1 contains a specific
	//     value.
	//
	// Parameters:
	//   item:
	//     The object to locate in the System.Collections.Generic.ICollection`1.
	//
	// Returns:
	//     true if item is found in the System.Collections.Generic.ICollection`1; otherwise,
	//     false.
	bool Contains(TSource item);
}
