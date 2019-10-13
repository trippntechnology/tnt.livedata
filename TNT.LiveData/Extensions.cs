using System;

namespace TNT.LiveData
{

	/// <summary>
	/// Extension functions exposed by <see cref="TNT.LiveData"/>
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Transforms the value in <paramref name="source"/> using <paramref name="func"/>
		/// and returns the new value as <see cref="LiveData{R}"/>
		/// </summary>
		/// <typeparam name="T">Type of object managed by <paramref name="source"/></typeparam>
		/// <typeparam name="R">Type of object returned by <paramref name="func"/></typeparam>
		/// <param name="source"><see cref="LiveData{T}"/> being transformed</param>
		/// <param name="func"><see cref="Func{T, TResult}"/> that transforms the value in <paramref name="source"/>
		/// to a value of <typeparamref name="R"/></param>
		/// <returns><see cref="LiveData{R}"/> that represents the transformed data</returns>
		public static LiveData<R> Map<T, R>(this LiveData<T> source, Func<T, R> func) => Transformations.Map<T, R>(source, func);

		/// <summary>
		/// Transforms the value in <paramref name="source"/> into <see cref="LiveData{R}"/> using 
		/// <paramref name="func"/> and returns the new <see cref="LiveData{R}"/>
		/// </summary>
		/// <typeparam name="T">Type of object managed by <paramref name="source"/></typeparam>
		/// <typeparam name="R">Type of object returned by <paramref name="func"/></typeparam>
		/// <param name="source"><see cref="LiveData{T}"/> being transformed</param>
		/// <param name="func"><see cref="Func{T, TResult}"/> that transforms the value in <paramref name="source"/>
		/// to a value of <typeparamref name="R"/></param>
		/// <returns><see cref="LiveData{R}"/> that represents the transformed data</returns>
		public static LiveData<R> SwitchMap<T, R>(this LiveData<T> source, Func<T, LiveData<R>> func) => Transformations.SwitchMap<T, R>(source, func);
	}
}